using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour, IDeadly
{
    //Public vars
    public int DamageCause = 40;
    public GameObject MedKitPrefab;
    
    //COMPONENTs
    Transform player;
    NavMeshAgent agent;
    Status myStatus;
    AnimationCharacter myAnimation;
    MovementCharacter myMovement;

    private void Start()
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER).transform;
        agent = GetComponent<NavMeshAgent>();
        myStatus = GetComponent<Status>();
        agent.speed = myStatus.Speed;
        myAnimation = GetComponent<AnimationCharacter>();
        myMovement = GetComponent<MovementCharacter>();
    }

    private void Update()
    {
        Vector3 playerPosition = player.position;
        agent.SetDestination(playerPosition);
        myAnimation.Walk(agent.velocity.magnitude);

        if (agent.hasPath)
        {
            bool playerClose = agent.remainingDistance <= agent.stoppingDistance;
            myAnimation.Attack(playerClose);
            
            Vector3 direction = playerPosition - transform.position;
            myMovement.Rotation(direction);
        }
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
        agent.enabled = false;

        Instantiate(MedKitPrefab, transform.position, Quaternion.identity);
    }
}