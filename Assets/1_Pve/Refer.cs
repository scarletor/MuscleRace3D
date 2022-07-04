using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;


public class Refer : MonoBehaviour
{




    public List<GameObject> bulletList;

    public GameObject impact1,minion;



    public static Refer ins;

    private void Start()
    {
        ins = this;
    }

}

