using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    //Components
    Rigidbody myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction, float speedMovement)
    {
        //Way to move the character using physics
        myRigidbody.velocity = direction.normalized * speedMovement;
    }

    public void Rotation(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        myRigidbody.MoveRotation(rotation);
    }

    public void Die()
    {
        myRigidbody.constraints = RigidbodyConstraints.None;
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }

}
