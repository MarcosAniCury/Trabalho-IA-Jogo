using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vertex {
    public Vector3 position { get; set; }
    public float heuristca { get; set; }
    public float distance { get; set; }
    public Vertex parent { get; set; }

    public Vertex(Vector3 vertexPos, Vector3 targetPos)
    {
        this.parent = null;
        this.distance = 0;
        this.position = vertexPos;
        this.heuristca = Vector3.Distance(vertexPos, targetPos);
    }

    public Vertex(Vertex parent, Vector3 vertexPos, Vector3 targetPos)
    {
        this.parent = parent;
        this.position = vertexPos;
        this.distance = parent.distance + Vector3.Distance(parent.position, vertexPos);
        this.heuristca = Vector3.Distance(vertexPos, targetPos);
    }
}

public class BossController : MonoBehaviour, IDeadly
{
    //Public vars
    public int DamageCause = 40;
    public GameObject MedKitPrefab;
    
    //COMPONENTs
    Transform player;
    Status myStatus;
    AnimationCharacter myAnimation;
    MovementCharacter myMovement;

    private Queue<Vector3> direction;

    private void Start()
    {
        direction = new Queue<Vector3>();
        player = GameObject.FindWithTag(Constants.TAG_PLAYER).transform;
        myStatus = GetComponent<Status>();
        myAnimation = GetComponent<AnimationCharacter>();
        myMovement = GetComponent<MovementCharacter>();
    }

    private void FixedUpdate()
    {
        if (direction.Count > 0)
        {
            if (Vector3.Distance(direction.Peek(), transform.position) == 0)
                direction.Dequeue();
            transform.Translate(direction.Peek() * Time.deltaTime);
        } else {
            FindPath();
        }
    }

    void FindPath()
    {
        //0 - definir vertice limite como posicao do boss
        bool stop = false;
        Vertex tmpVertex;
        Vertex selected = new Vertex(transform.position, player.position);
        List<Vertex> limit = new List<Vertex>();
        limit.Add(selected);
        Vector3[] variant = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        //1 - plotar vertices ao redor dos vertices limite

        while (!stop /*&& repeat < 10*/)
        {

            for (int i = 0; i < variant.Length; i++)
            {
                tmpVertex = new Vertex(selected, selected.position + (variant[i] * 3), player.position);
                if (limit.FindIndex(e => Vector3.Distance(e.position, tmpVertex.position) == 0) == -1 && Physics.OverlapSphere(tmpVertex.position + (Vector3.up * 3), 1).Length == 0)
                    limit.Add(tmpVertex);
            }

            //2 - calcular heuristica de cada vertice

            limit.Remove(selected);

            //3 - escolher vertice com menor (distancia do boss + heuristica)
            //4 - "mover" para o vertice escolhido

            if (limit.Count > 0)
            {
                selected = limit[0];
                limit.ForEach(curr => {
                    if ((curr.heuristca + curr.distance) < (selected.heuristca + selected.distance))
                        selected = curr;
                });
            }

            //5 - verificar se estar do lado do jogador
            //  Se sim movemos para o vertice escolhido
            //  Se não retornarmos ao passo 1
            if (Vector3.Distance(selected.position, player.position) <= 4.5)
            {
                stop = true;
                direction.Clear();
                SetPath(selected);
            }
        }
    }

    void SetPath(Vertex path)
    {
        if (path.parent != null)
        {
            SetPath(path.parent);
        }
        direction.Enqueue(path.position);
    }

    void AttackPlayer()
    {
        player.GetComponent<PlayerController>().TakeDamage(DamageCause);
    }

    public void TakeDamage(int damage)
    {
        myStatus.Life -= damage;
        if (myStatus.Life <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(gameObject, 2);
        
        myAnimation.Die();
        myMovement.Die();
        this.enabled = false;

        Instantiate(MedKitPrefab, transform.position, Quaternion.identity);
    }
}