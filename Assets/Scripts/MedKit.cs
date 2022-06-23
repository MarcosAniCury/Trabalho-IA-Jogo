using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, Constants.MEDKIT_TIME_SELF_DESTROY);
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.tag == Constants.TAG_PLAYER) {
            otherCollider.GetComponent<PlayerController>().Healing(Constants.MEDKIT_AMOUNT_HEAL);
            Destroy(gameObject);
        }
    }
}
