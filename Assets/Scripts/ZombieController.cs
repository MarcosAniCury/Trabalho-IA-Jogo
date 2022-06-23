using UnityEngine;

public class ZombieController : MonoBehaviour, IDeadly
{
    //Public vars
    public int DamageCaused = 20;
    public AudioClip ZombieDieSound;
    public GameObject MedKitPrefab;
    
    [HideInInspector]
    public EnemyGenerate MyGenerate;

    //Private vars
    GameObject player;
    float contWander;
    Vector3 randomPositionWander;
    Vector3 direction;

    //Components
    PlayerController playerController;
    MovementCharacter myMovement;
    AnimationCharacter myAnimation;
    Status myStatus;
    UIController gameInterface;

    void Start() 
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER);
        playerController = player.GetComponent<PlayerController>(); 
        myMovement = GetComponent<MovementCharacter>(); 
        myAnimation = GetComponent<AnimationCharacter>();
        myStatus = GetComponent<Status>();
        gameInterface = GameObject.FindObjectOfType(typeof(UIController)) as UIController;

        SetZombieRandom();
    }

    void FixedUpdate()
    {
        float distanceBetweenZombiAndPlayer = Vector3.Distance(
            transform.position, player.transform.position
        );

        bool attacking = true;
        if (distanceBetweenZombiAndPlayer > Constants.ZOMBIE_DISTANCE_TO_WANDER) {
            Wander();
            attacking = false;
        } else if (distanceBetweenZombiAndPlayer > Constants.ZOMBIE_DISTANCE_TO_CHASE) {
            direction = player.transform.position - transform.position;
            myMovement.Rotation(direction);
            myMovement.Movement(direction.normalized, myStatus.Speed);
            attacking = false;
        } 

        myAnimation.Walk(direction.magnitude);
        myAnimation.Attack(attacking);
    }

    void Wander()
    {
        contWander -= Time.deltaTime;

        if (contWander <= 0) {
            randomPositionWander = GenerateRandomPosition();
            float randomTimeToSpawn = Random.Range(-1f, 1f);
            contWander += Constants.ZOMBIE_TIME_BETWEEN_WANDER_AGAIN + randomTimeToSpawn;
        }

        float speedMovement = 0;

        bool isProxDistance = 
            Vector3.Distance(transform.position, randomPositionWander) <=
            Constants.ZOMBIE_ERROR_RATE_DISTANCE;

        if (!isProxDistance) {
            direction = randomPositionWander - transform.position;
            myMovement.Rotation(direction);
            speedMovement = myStatus.Speed;
        }

        myMovement.Movement(direction, speedMovement);
    }

    Vector3 GenerateRandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * Constants.ZOMBIE_RADIO_TO_GENERATE_RANDOM_POSITION;
        position += transform.position;
        position.y = transform.position.y;

        return position;
    }

    void AttackPlayer() 
    {
        playerController.TakeDamage(DamageCaused);
    }

    void SetZombieRandom()
    {
        int generateTypeZombie = Random.Range(1, transform.childCount);
        transform.GetChild(generateTypeZombie).gameObject.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        myStatus.Life -= damage;
        if (myStatus.Life <= 0) {
            Dead();
        }
    }

    public void Dead()
    {
        int timeToZombieDestroy = 2;
        Destroy(gameObject, timeToZombieDestroy);

        myAnimation.Die();
        myMovement.Die();
        
        GenerateMedKit();

        gameInterface.AddOneZombieDead();
        
        MyGenerate.DecreseNumberOfZombieAlive();
        
        SoundController.instance.PlayOneShot(ZombieDieSound);

        this.enabled = false;
    }

    void GenerateMedKit()
    {   
        if(Random.value <= Constants.ZOMBIE_CHANCE_TO_GENERATE_MED_KIT) {
            Instantiate(MedKitPrefab, transform.position, Quaternion.identity);
        }
    }
}
