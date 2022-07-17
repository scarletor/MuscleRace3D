using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using System;
public class SharedScene : MonoBehaviour
{
    public static SharedScene ins;
    private void Awake()
    {
        ins = this;
    }

    public GameObject topPanel;
    public Text goldText, keyText;
    public GameObject keyGr, goldPos;
    public Image key1, key2, key3;
    void Start()
    {
        RefreshGoldText();
        RefreshKeyText();
    }

    public void RefreshGoldText()
    {
        goldText.text = "" + UserData.GetGold();
    }
    public void RefreshKeyText()
    {
        key1.color = Color.black;
        key2.color = Color.black;
        key3.color = Color.black;
        if (UserData.GetCurKey() == 1)
        {
            key1.color = Color.white;
        }
        if (UserData.GetCurKey() == 2)
        {
            key1.color = Color.white;
            key2.color = Color.white;
        }
        if (UserData.GetCurKey() >= 3)
        {
            key1.color = Color.white;
            key2.color = Color.white;
            key3.color = Color.white;
        }


    }

    [Button]
    public void SetGold(int gold)
    {
        UserData.SetGold(gold);
    }


    public void InactiveKeyGR()
    {
        keyGr.gameObject.SetActive(false);
    }



    public void ActiveKeyGr()
    {
        keyGr.gameObject.SetActive(true);
    }


}
