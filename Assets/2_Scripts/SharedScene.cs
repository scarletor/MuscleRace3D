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
    public Text goldText;
    public GameObject keyGr;

    void Start()
    {
        RefreshGoldText();
    }

    public void RefreshGoldText()
    {
        goldText.text = "" + UserData.GetGold();
    }


    // Update is called once per frame
    void Update()
    {

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
