using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refer : MonoBehaviour
{





    public static Refer ins;

    private void Awake()
    {
        ins = this;        
    }




    public GameObject redInk, blueInk, greenInk, pinkInk;
}
