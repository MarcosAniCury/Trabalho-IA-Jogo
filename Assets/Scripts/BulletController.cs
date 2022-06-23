using UnityEngine;

public class BulletController : MonoBehaviour
{
    //public vars
    public float BulletSpeed = 20;

    //Components
    Rigidbody bulletRigidbody;

    void Start() 
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() 
    {
        bulletRigidbody.MovePosition(
            bulletRigidbody.position + (
                transform.forward *
                BulletSpeed *
                Time.deltaTime
            )
        );
    }

    void OnTriggerEnter(Collider colliderObject)
    {
        switch (colliderObject.tag) {
           case Constants.TAG_ENEMY:
               colliderObject.GetComponent<ZombieController>().TakeDamage(Constants.BULLET_DAMAGE_IN_ZOMBIE);
               break;
           case Constants.TAG_BOSS:
               colliderObject.GetComponent<BossController>().TakeDamage(Constants.BULLET_DAMAGE_IN_ZOMBIE);
               break;
        }

        Destroy(gameObject);
    }
}
