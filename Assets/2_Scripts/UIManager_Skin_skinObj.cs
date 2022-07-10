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

public class UIManager_Skin_skinObj : MonoBehaviour
{


    public GameObject myObjRef;
    private void Awake()
    {
        name = myObjRef.name;
    }


    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        if (UserData.IsUnlockedChar(name))
        {
            transform.GetChild(1).GetComponent<Image>().color = Color.white;
            isUnlocked = true;
        }
    }

    public int myPrice;

    public bool isUnlocked;









}
