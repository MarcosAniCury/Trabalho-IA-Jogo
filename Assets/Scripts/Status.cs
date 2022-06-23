using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    //Public vars
    public int InitialLife = 100;
    [HideInInspector]
    public int Life;
    public float Speed = 5;

    void Start()
    {
        Life = InitialLife;
    }
}
