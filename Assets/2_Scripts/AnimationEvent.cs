using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{


    public CharacterControl parent;

    public void ContinueMoving()
    {
        parent.ResumeTween();
    }
    public void ContinueMovingPlayer()
    {
        if(parent.isBot==false)
        parent.ResumeTweenPlayer();
    }



}
