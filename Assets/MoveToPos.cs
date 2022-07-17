using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class MoveToPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (posToMoveBackPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToMoveBackPlayer.transform.position, Time.deltaTime * 30);
            if (Vector3.Distance(transform.position, posToMoveBackPlayer.transform.position) < 0.03f)
            {
                gameObject.transform.SetParent(posToMoveBackPlayer.transform);
                transform.localEulerAngles = Vector3.zero;
                enabled = false;
                posToMoveBackPlayer = null;
            }
        }



        if (posToMovePaint != null)
        {
            gameObject.transform.parent = null;
            var pos = posToMovePaint;
            transform.position = Vector3.MoveTowards(transform.position, pos.transform.position, Time.deltaTime * 30);


            if (Vector3.Distance(transform.position, pos.transform.position) < 0.03f)
            {
                gameObject.SetActive(false);
                pos.gameObject.SetActive(false);
            }

        }






    }

    public GameObject posToMoveBackPlayer, posToMovePaint;

    public colorTypeEnum myColorType;

}

public enum colorTypeEnum
{
    none,
    red,
    green, pink, blue,


}

