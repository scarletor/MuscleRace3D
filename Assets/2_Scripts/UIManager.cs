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

public class UIManager : MonoBehaviour
{







    public static UIManager ins;

    private void Awake()
    {
        ins = this;
        SceneManager.LoadScene("SharedScene", LoadSceneMode.Additive);
    }



    public void OnClickSkinBtn()
    {
        SceneManager.LoadScene("Skin");
    }




    public void OnClickPlayBtn()
    {

    }




    public void OnLose()
    {

    }

    public Text levelText;
    public void Setup()
    {
        initPosYTopanel = SharedScene.ins.topPanel.transform.position.y;
        levelText.text = "Level " + UserData.GetCurLevel();
        SharedScene.ins.ActiveKeyGr();
    }

    private void Start()
    {
        Setup();
    }



    public GameObject winUI;
    [Button]
    public void DisplayWinUI()
    {
        winUI.SetActive(true);
        SharedScene.ins.topPanel.SetActive(true);
        var timeShow = 1;
        Utils.ins.FadeInUI(winUI);
        Utils.ins.DelayCall(timeShow, () =>
        {
            ProcessFill();
            PointerShake();
        });
    }





    [Button]
    public void TopPanelIn()
    {
        SharedScene.ins.topPanel.transform.DOMoveY(initPosYTopanel, 1);
    }

    [Button]
    public void TopPanelOut()
    {
        SharedScene.ins.topPanel.transform.DOMoveY(initPosYTopanel + 500, 1);
    }
    public float initPosYTopanel;

    public Text textFill;
    public Image spriteFill;
    public float fillAmount;
    public float filled;
    [Button]
    public void ProcessFill()
    {

        filled = 0;
        StartCoroutine(Fill());
        IEnumerator Fill()
        {
            while (filled <= fillAmount)
            {
                yield return new WaitForSeconds(Time.deltaTime * 0.1f);
                filled += 0.5f;
                if (filled % 1 == 0)
                {
                    textFill.text = "" + filled;
                }

                spriteFill.fillAmount = filled * 0.01f;
            }

        }

    }
    public int endRotation;
    public int countShake;
    public GameObject pointer;
    [Button]
    public void PointerShake()
    {
        if (stopStake) return;
        countShake++;
        endRotation = 0;
        if (countShake % 2 == 0)
        {
            endRotation = 89;
        }
        else
        {
            endRotation = -89;
        }
        var rdTime = Random.Range(.3f, 1f);
        pointer.transform.DORotate(new Vector3(0, 0, endRotation), rdTime, RotateMode.Fast).OnComplete(() => { PointerShake(); });
        Debug.LogError("SHAKE");
    }

    public bool stopStake = false;
    public void OnClickVideoAd()
    {
        stopStake = true;
        pointer.transform.DOPause();
        Debug.LogError("Click video ad");


        var pointerAt = 5;
        Debug.LogError(pointer.transform.localRotation.eulerAngles.z);
        if (pointer.transform.localRotation.eulerAngles.z >= 55)
        {
            pointerAt = 2;
        }
        if (pointer.transform.localRotation.eulerAngles.z >= 14)
        {
            pointerAt = 4;
        }
        if (pointer.transform.localRotation.eulerAngles.z <= -55)
        {
            pointerAt = 2;
        }
        if (pointer.transform.localRotation.eulerAngles.z <= -14)
        {
            pointerAt = 4;
        }


        Debug.LogError(pointerAt);
    }


    public void ResetWinUI()
    {
        stopStake = false;
    }






    [TitleGroup("Setting")]

    public GameObject settingUI;
    public void OnClickCloseBtn()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name.Contains("#_Setting")) homeUI.SetActive(true);
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }

    public void OnClickSettingBtn()
    {
        settingUI.gameObject.SetActive(true);
        homeUI.gameObject.SetActive(false);
    }





    [TitleGroup("Lose")]

    public GameObject loseUI;

    public void OnPlayerLose_UI()
    {
        Utils.ins.FadeInUI(loseUI);
    }


    public void OnClickReplayBtn()
    {
        Utils.ins.ReloadCurrentScene();
    }



    [TitleGroup("StartGame")]

    public GameObject homeUI;
    public void OnClickStartGame()
    {
        CharacterControl.charGreen.StartGame();//   
        CharacterControl.charBlue.StartGame();
        CharacterControl.charPink.StartGame();
        homeUI.gameObject.SetActive(false);
    }




}



public enum UIStateEnum
{
    none,
    home,

}
