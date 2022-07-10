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
public class UIManager_Skin : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("SharedScene", LoadSceneMode.Additive);
    }





    public List<GameObject> characters;
    public GameObject contents;
    public string curCharString;
    public GameObject buyBtn, currentBtn;
    public Text priceText;


    public void OnClickSelectCharacter(bool isSetup = false)
    {
        currentBtn = gameObject;
        if (isSetup == false)
        {
            currentBtn = EventSystem.current.currentSelectedGameObject.gameObject;
            curCharString = currentBtn.name;

        }
        else
        {

            curCharString = PlayerPrefs.GetString("curChar");
            if (curCharString == "") return;
            currentBtn = contents.transform.Find(curCharString).gameObject;
        }


        foreach (Transform child in contents.transform)
        {
            child.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        currentBtn.transform.GetChild(0).GetComponent<Image>().color = Color.green;

        characters.ForEach(go =>
        {

            go.SetActive(false);

            if (go == currentBtn.GetComponent<UIManager_Skin_skinObj>().myObjRef)
            {
                go.SetActive(true);
                UserData.SetCurChar(go.name);

            }

        });
        buyBtn.gameObject.SetActive(!UserData.IsUnlockedChar(curCharString));
        priceText.text = "" + currentBtn.gameObject.GetComponent<UIManager_Skin_skinObj>().myPrice;
    }








    public void OnClickBack()
    {
        SceneManager.LoadScene("Play");
    }



    public GameObject characterTab, itemTab, previewChar, previewItem, charBtn, itemBtn;


    public void SwitchTab(string name)
    {
        characterTab.gameObject.SetActive(false);
        itemTab.gameObject.SetActive(false);
        previewChar.gameObject.SetActive(false);
        previewItem.gameObject.SetActive(false);
        charBtn.GetComponent<Image>().color = Color.gray;
        itemBtn.GetComponent<Image>().color = Color.gray;


        if (name == "characterTab")
        {
            characterTab.gameObject.SetActive(true);
            previewChar.gameObject.SetActive(true);
            charBtn.GetComponent<Image>().color = Color.white;



        }
        else if (name == "itemTab")
        {
            itemTab.gameObject.SetActive(true);
            previewItem.gameObject.SetActive(true);
            itemBtn.GetComponent<Image>().color = Color.white;





        }

    }



    private void Start()
    {
        SharedScene.ins.InactiveKeyGR();
        OnClickSelectCharacter(true);
    }


    public string curCharSelecting;
    public Text price;
    public void OnClickBuyBtn()
    {
        var curGold = UserData.GetGold();
        var price = Int32.Parse(this.price.text);
        if (price > curGold)
        {
            Debug.LogError("NOT ENOUGH GOLD");
            return;
        }
        else
        {
            UserData.SetCurChar(curCharString);
            UserData.MinusGold(price);
            UserData.SetUnlockChar(curCharString);
            currentBtn.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            buyBtn.gameObject.SetActive(false);
            Debug.LogError("BOUGHT CHAR " + curCharString + "price__" + price);

        }
    }






}
