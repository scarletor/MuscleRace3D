using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonobehaviourSingleton : MonoBehaviour
{


    public static MonobehaviourSingleton instant;


    private void Awake()
    {
        if (instant == null)
        {
            instant = this;  // if the loaded scene already has this instance so it will not create new one
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

















}
