using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public vars
    public GameObject Player;
    public Camera cameraMain;
    public Camera cameraDebug;

    //private vars
    private Vector3 distanceBetweenPlayerAndCamera;

    void Start() {
        distanceBetweenPlayerAndCamera = cameraMain.transform.position - Player.transform.position;
    }

    void Update() {
        cameraMain.transform.position = Player.transform.position + distanceBetweenPlayerAndCamera;
    }

    public void tradeCamera(string toCamera)
    {
        if (toCamera == Constants.TAG_CAMERA_MAIN)
        {
            cameraMain.gameObject.SetActive(true);
            cameraDebug.gameObject.SetActive(false);
        } else if (toCamera == Constants.TAG_CAMERA_DEBUG)
        {
            cameraMain.gameObject.SetActive(false);
            cameraDebug.gameObject.SetActive(true);
        }
    }
}
