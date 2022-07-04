using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public static GameManager ins;
    private void Awake()
    {
        ins = this;
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
