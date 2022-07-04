using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using TMPro;

public class GetInkCollider : MonoBehaviour
{









    public CharacterControl parent;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("#_1_Ink_" + parent.colorType))
        {
            if (parent.GetInk(other.gameObject) == false) return;

            other.gameObject.name = "#_2_InkTaken_" + parent.colorType;
        }

        if (other.gameObject.name.Contains("#_" + parent.colorType + "_Paint"))
        {
            parent.canPaint = true;
        }
        if (other.gameObject.name.Contains("#_" + parent.colorType + "_BridgeBuild"))
        {
            parent.canPaintBridge = true;
        }

        if (other.gameObject.name.Contains("#_" + parent.colorType + "_BridgeLose"))
        {
            parent.canLosePaint = true;
            parent.speedMove = 1f;
        }
        if (other.gameObject.name.Contains("#_Victory"))
        {
            parent.WinGame();
        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name.Contains("#_" + parent.colorType + "_Paint"))
        {
            parent.canPaint = false;
        }

        if (other.gameObject.name.Contains("#_" + parent.colorType + "_BridgeBuild"))
        {
            parent.canPaintBridge = false;
        }


        if (other.gameObject.name.Contains("#_" + parent.colorType + "_BridgeLose"))
        {
            parent.canLosePaint = false;
            parent.speedMove = 3;
        }
    }









}
public enum myIDEnum
{
    none,
    Red,
    Blue,
    Green,
    Pink,


}
