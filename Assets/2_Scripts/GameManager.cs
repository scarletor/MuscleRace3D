using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
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
public class GameManager : MonoBehaviour
{
    public static GameObject curChar;
    void Start()
    {
        UserData.SetGoldMore(3000);
        RefreshCurChar();
    }

    [Button]
    public void OnClickBack()
    {
        SceneManager.LoadScene("Play");
    }



    public static GameManager ins;
    private void Awake()
    {
        ins = this;
    }



    public GameObject charsParent;
    public void RefreshCurChar()
    {
        var charName = PlayerPrefs.GetString("curChar");
        if (charName == "")
        {
            PlayerPrefs.SetString("curChar", "Stickman");
            charName = PlayerPrefs.GetString("curChar");
        }


        foreach (Transform child in charsParent.transform)
        {
            if (child.name == charName)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }




    }


    public bool isStopGame;
    public void BotWin(GameObject bot)
    {
        if (isStopGame == true) return;
        CameraTo(bot);
        isStopGame = true;
        UIManager.ins.OnPlayerLose_UI();
    }


    public CinemachineVirtualCamera cam;

    public void CameraTo(GameObject target)
    {
        cam.Follow = target.transform;
        cam.gameObject.transform.rotation = Quaternion.Euler(30, 0, 0);
    }



    public void ResetGame()
    {
        isStopGame = false;
    }

    public void PlayerWin()
    {
        Utils.ins.DelayCall(2, () =>
        {
            UIManager.ins.DisplayWinUI();

        });
    }
    public List<GroundBase> grounds;
    public GameObject inkGr;


    public GameObject posBlueUnlock;
    public GameObject posGreenUnlock;
    public GameObject posPinkUnlock;
    public GameObject botMovePos;


    public GameObject posBlueUnlock_Ground2;
    public GameObject posGreenUnlock_Ground2;
    public GameObject posPinkUnlock_Ground2;
    public GameObject botMovePos_Ground2;


    public GameObject posBlueUnlock_Ground3;
    public GameObject posGreenUnlock_Ground3;
    public GameObject posPinkUnlock_Ground3;
    public GameObject botMovePos_Ground3;



}
