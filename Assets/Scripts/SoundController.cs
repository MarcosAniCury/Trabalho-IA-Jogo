using UnityEngine;

public class SoundController : MonoBehaviour
{
    //Public vars
    public static AudioSource instance;

    //Components
    AudioSource myAudioSource;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        instance = myAudioSource;
    }
}
