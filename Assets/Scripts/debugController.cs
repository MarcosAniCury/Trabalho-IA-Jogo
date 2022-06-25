using System;
using UnityEngine;
using UnityEngine.UI;

public class debugController : MonoBehaviour
{

    //public vars
    public Text debug;

    //private vars
    private bool debugControll;
    private CameraController cameraController;

    void Start()
    {
        debugControll = false;
        cameraController = FindObjectOfType(typeof(CameraController)) as CameraController;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            debugControll = !debugControll;

            debug.gameObject.SetActive(debugControll);
            if (debugControll)
            {
                cameraController.tradeCamera(Constants.TAG_CAMERA_DEBUG);
            }
            else
            {
                cameraController.GetComponent<CameraController>().tradeCamera(Constants.TAG_CAMERA_MAIN);
            }
        }
    }
}