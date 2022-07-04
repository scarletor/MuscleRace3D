using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchStickmanCollider : MonoBehaviour
{










    public CharacterControl parent;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("#_TouchStickmanCollider"))
        {
            parent.TouchOtherStick(other.gameObject.GetComponent<TouchStickmanCollider>().parent.inkCount);
        }
    }






}
