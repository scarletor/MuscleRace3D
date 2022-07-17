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
        UserData.SetCurLevelIncrease();
        UserData.SetGoldMore(250);
        GoldEff(250);
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
        UserData.SetGoldMore(100);
        GoldEff(100);
    }


    public void OnClickReplayBtn()
    {
        Utils.ins.ReloadCurrentScene();
    }



    [TitleGroup("StartGame")]

    public GameObject homeUI;
    public void OnClickStartGame()
    {
        CharacterControl.charGreen.Setup();//   
        CharacterControl.charBlue.Setup();
        CharacterControl.charPink.Setup();
        CharacterControl.charPlayer.Setup();
        GameManager.ins.grounds[0].CharacterArrive(CharacterControl.charGreen);
        GameManager.ins.grounds[0].CharacterArrive(CharacterControl.charBlue);
        GameManager.ins.grounds[0].CharacterArrive(CharacterControl.charPink);
        GameManager.ins.grounds[0].CharacterArrive(CharacterControl.charPlayer);
        homeUI.gameObject.SetActive(false);
    }




    public GameObject goldPref, startGoldPos, endGoldPos, canvas;
    [Button]
    public void GoldEff(int value)
    {
        Utils.ins.DelayCall(2, () =>
        {

            StartCoroutine(spawn());

            IEnumerator spawn()
            {
                var i = 0;
                while (i < 10)
                {
                    i++;
                    yield return new WaitForSeconds(0.1f);

                    var newGoldEff = Instantiate(goldPref, canvas.transform);
                    newGoldEff.transform.position = startGoldPos.transform.position;
                    newGoldEff.transform.DOMove(SharedScene.ins.goldPos.transform.position, 1);
                }
            }

            Utils.ins.DelayCall(1, () =>
            {
                Utils.ins.IncreaseNumEff(SharedScene.ins.goldText,value);
            });

        });
    }


}



public enum UIStateEnum
{
    none,
    home,

}
