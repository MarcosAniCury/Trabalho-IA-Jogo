using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public vars
    public GameObject Player;

    //private vars
    private Vector3 distanceBetweenPlayerAndCamera;

    void Start() {
        distanceBetweenPlayerAndCamera = transform.position - Player.transform.position;
    }

    void Update() {
        transform.position = Player.transform.position + distanceBetweenPlayerAndCamera;
    }
}
