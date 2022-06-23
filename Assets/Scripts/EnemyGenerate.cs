using System.Collections;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    //Public vars
    public GameObject Zombie;
    public float GenerateEnemyTime = 1;
    public LayerMask LayerEnemys;

    //Private vars
    private float timeCount;
    private int enemysMaxAlive = 2;
    private int enemysAlive = 0;
    private float countTimeIncreseNumberOfMaxEnemys;

    //Components
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER);
        countTimeIncreseNumberOfMaxEnemys = Constants.ENEMY_GENERATE_TIME_TO_INCRESE_NUMBER_OF_MAX_ENEMYS;
        for (int i = 0; i < enemysMaxAlive; i++)
        {
            StartCoroutine(GenerateZombie());
        }
    }

    void Update()
    {
        bool canGenerateEnemy = (Vector3.Distance(
            transform.position,
            player.transform.position
        ) > Constants.ENEMY_GENERATE_DISTANCE_BETWEEN_PLAYER_AND_ENEMY_TO_SPAWN);

        if (canGenerateEnemy && enemysAlive < enemysMaxAlive)
        {
            timeCount += Time.deltaTime;

            if (timeCount > GenerateEnemyTime)
            {
                StartCoroutine(GenerateZombie());
                timeCount = 0;
            }
        }

        if (Time.timeSinceLevelLoad > countTimeIncreseNumberOfMaxEnemys)
        {
            enemysMaxAlive++;
            countTimeIncreseNumberOfMaxEnemys = 
                Time.timeSinceLevelLoad + Constants.ENEMY_GENERATE_TIME_TO_INCRESE_NUMBER_OF_MAX_ENEMYS;
        }
    }

    IEnumerator GenerateZombie()
    {
        Vector3 randomPosition;
        Collider[] colliders;
        do
        {
            randomPosition = GenerateRandomPosition();
            colliders = Physics.OverlapSphere(randomPosition, 1, LayerEnemys);
            yield return null;
        } while (colliders.Length > 0);

        ZombieController zombie = Instantiate(Zombie, randomPosition, transform.rotation)
            .GetComponent<ZombieController>();
        zombie.MyGenerate = this;
        enemysAlive++;
    }

    Vector3 GenerateRandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * Constants.ENEMY_GENERATE_RADIO_TO_GENERATE_RANDOM_POSITION;
        Vector3 myPosition = transform.position;
        position += myPosition;
        position.y = myPosition.y;

        return position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Constants.ENEMY_GENERATE_RADIO_TO_GENERATE_RANDOM_POSITION);
    }

    public void DecreseNumberOfZombieAlive()
    {
        enemysAlive--;
    }
}
