using UnityEngine;

public class PlayerController : MonoBehaviour, IDeadly, ICurable
{
    //Public vars
    public UIController UIController;
    public AudioClip DamageSound;
    public GameObject playerGun;
    
    //Private vars
    private Vector3 direction;

    //Components
    MovementCharacter myMovement;
    AnimationCharacter myAnimator;
    [HideInInspector]
    public Status myStatus;

    void Start() 
    {
        myMovement = GetComponent<MovementCharacter>();
        myAnimator = GetComponent<AnimationCharacter>();
        myStatus = GetComponent<Status>();
    }
    
    void Update()
    {
        //Use to walk with awsd
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisZ = Input.GetAxisRaw("Vertical");

        direction = new Vector3(axisX, 0, axisZ);

        myAnimator.Walk(direction.magnitude);
    }

    void FixedUpdate() 
    {
        myMovement.Movement(direction, myStatus.Speed);

        PlayerMovement();
    }

    void PlayerMovement()
    {
        Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, playerGun.transform.position);

        float colliderDistance;

        if (plane.Raycast(rayCamera, out colliderDistance))
        {
            Vector3 colliderLocal = rayCamera.GetPoint(colliderDistance);
            colliderLocal.y = 0;

            Vector3 localLook = colliderLocal - transform.position;
            myMovement.Rotation(localLook);
        }
    }

    public void TakeDamage(int damageTaked) 
    {
        myStatus.Life -= damageTaked;

        UIController.updatedLivePlayerSlider();

        SoundController.instance.PlayOneShot(DamageSound);
        
        if (myStatus.Life <= 0) {
            Dead();
        }
    }

    public void Healing(int amountHeal)
    {
        myStatus.Life += amountHeal;
        if (myStatus.Life > myStatus.InitialLife) {
            myStatus.Life = myStatus.InitialLife;
        }

        UIController.updatedLivePlayerSlider();
    }

    public void Dead()
    {
        UIController.GameOver();
    }
}
