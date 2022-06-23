using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //public vars
    public GameObject Bullet;
    public GameObject BarrelGun;
    public AudioClip ShotSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Constants.INPUT_MOUSE_LEFT)) {
            Instantiate(Bullet, BarrelGun.transform.position, BarrelGun.transform.rotation);
            SoundController.instance.PlayOneShot(ShotSound);
        }
    }
}
