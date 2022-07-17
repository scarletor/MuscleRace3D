using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using TMPro;
using Cinemachine;
public class CharacterControl : MonoBehaviour
{
    public int currentGroundIndex;
    public GroundBase currentGround;
    public GameObject inkDropParticle, buildBridgeInkDropPos;

    public GameObject inkPosList;
    public void SetupOtherStick()
    {

    }
    public static CharacterControl charPink, charGreen, charBlue, charPlayer;

    private void Awake()
    {
        if (colorType == myIDEnum.Blue) charBlue = this;
        if (colorType == myIDEnum.Pink) charPink = this;
        if (colorType == myIDEnum.Green) charGreen = this;
        if (colorType == myIDEnum.Red) charPlayer = this;
    }

    private void Start()
    {

    }
    //
    //
    // Start is called before the first frame update
    public void Setup()
    {
        SetupCurGround();
        canMove = true;
        MoveByBot();
    }


    public int inkToTake;
    public bool isBot;
    public myIDEnum colorType;
    public float speedMove;
    public VariableJoystick _joystick;
    public Vector3 dirDisplay;
    public float WTF;
    public Rigidbody _rigid;
    void Update()
    {
        Paint(); //ground 1
        BuildBridge(); //ground 2
        LosePaint();  // ground 3
    }
    private void FixedUpdate()
    {
        MoveByJoyStick();
    }

    public bool canMove;
    public void MoveByJoyStick()
    {
        if (isBot) return;


        if (GameManager.ins.isStopGame) return;
        if (canMove == false) return;
        if (pauseTween) return;
        if (killTween) return;
        if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Fall") return;




        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;


        Vector3 dir = _joystick.Direction;
        if (dir != Vector3.zero)
        {
            _anim.SetBool("isRunning", true);
            dir.z = dir.y;
            dir.y = 0;
            dir = dir * speedMove * Time.fixedDeltaTime;
            dirDisplay = dir;
            gameObject.transform.position += dir;
            WTF = dir.z + dir.x;

            gameObject.transform.rotation = Quaternion.LookRotation(dir);

            if (inkCount > 0)
            {
                _anim.SetBool("isRunWith", true);
            }
            else
            {
                _anim.SetBool("isRunWith", false);
            }
        }
        else
        {
            _anim.SetBool("isRunning", false);
        }
    }

    Tweener moveTween;
    bool pauseTween;
    [Button]
    public void KillTween()
    {
        var lastpos = transform.position;
        DOTween.Kill(gameObject.transform);
        gameObject.transform.position = lastpos;
        killTween = true;
        canMove = false;
        pauseTween = true;

    }
    bool killTween;

    [Button]
    public void ResumeTweenPlayer()
    {
        _anim.SetBool("isRunning", true);
        pauseTween = false;
        killTween = false;
        canMove = true;
    }





    [Button]
    public void ResumeTween()
    {
        _anim.SetBool("isRunning", true);
        Debug.LogError("SET RUNBING TRUE" + colorType);
        pauseTween = false;
        killTween = false;
        canMove = true;
        MoveByBot();
    }
    public bool isFirstMoveGround3;
    public float speedMoveBot;
    [Button]
    public void MoveByBot()
    {
        if (isBot == false) return;
        if (canMove == false) return;
        if (pauseTween) return;
        if (killTween) return;
        if (GameManager.ins.isStopGame) return;







        var rdPos = gameObject.transform;
        var distance = 1f;
        var timeToMove = 1f;
        if (inkTook.Count >= inkToTake) // move to endPoint
        {
            if (colorType == myIDEnum.Green) rdPos = currentGround.posGreenUnlock.transform;
            if (colorType == myIDEnum.Blue) rdPos = currentGround.posBlueUnlock.transform;
            if (colorType == myIDEnum.Pink) rdPos = currentGround.posPinkUnlock.transform;


            distance = Vector3.Distance(rdPos.transform.position, transform.position);
            timeToMove = distance / speedMoveBot;

            moveTween = gameObject.transform.DOMove(rdPos.transform.position, timeToMove).SetEase(Ease.Linear).OnComplete(() =>
                   {
                       _anim.SetBool("isRunning", false);
                       Utils.ins.DelayCall(3, () =>
                       {
                           MoveByBot();
                       });
                   });
        }
        else // move to collect point
        {
            rdPos = currentGround.botMovePosGr.transform.GetChild(Random.Range(0, currentGround.botMovePosGr.transform.childCount - 1));

            distance = Vector3.Distance(rdPos.transform.position, transform.position);
            timeToMove = distance / speedMoveBot;

            moveTween = gameObject.transform.DOMove(rdPos.transform.position, timeToMove).SetEase(Ease.Linear).OnComplete(() =>
            {
                MoveByBot();
            });

        }

        SetDirToPos(rdPos);


        return;

        if (_groundState == GroundStateEnum.ground_1)
        {
            //init var
            Vector3 dir = rdPos.transform.position - transform.position;
            if (dir != Vector3.zero)
            {
                gameObject.transform.rotation = Quaternion.LookRotation(dir);
                if (inkCount > 0)
                {
                    _anim.SetBool("isRunning", true);
                    _anim.SetBool("isRunWith", true);
                }
                else
                {
                    _anim.SetBool("isRunWith", false);
                    _anim.SetBool("isRunning", true);

                }
            }
            else
            {
                _anim.SetBool("isRunning", false);
            }
        }






        //if (_groundState == GroundStateEnum.ground_2)
        //{
        //    //init var
        //    var rdPos = gameObject.transform;
        //    var distance = 1f;
        //    var timeToMove = 1f;

        //    if (inkTook.Count >= inkToTake) // move to endPoint
        //    {
        //        if (colorType == myIDEnum.Green) rdPos = GameManager.ins.posGreenUnlock_Ground2.transform;
        //        if (colorType == myIDEnum.Blue) rdPos = GameManager.ins.posBlueUnlock_Ground2.transform;
        //        if (colorType == myIDEnum.Pink) rdPos = GameManager.ins.posPinkUnlock_Ground2.transform;


        //        distance = Vector3.Distance(rdPos.transform.position, transform.position);
        //        timeToMove = distance / speedMoveBot;

        //        moveTween = gameObject.transform.DOMove(rdPos.transform.position, timeToMove).SetEase(Ease.Linear).OnComplete(() =>
        //        {
        //            _anim.SetBool("isRunning", false);
        //            Utils.ins.DelayCall(1.5f, () =>
        //            {
        //                MoveByBot();
        //            });
        //        });
        //    }
        //    else // move to collect point
        //    {
        //        rdPos = GameManager.ins.botMovePos_Ground2.transform.GetChild(Random.Range(0, GameManager.ins.botMovePos.transform.childCount - 1));

        //        distance = Vector3.Distance(rdPos.transform.position, transform.position);
        //        timeToMove = distance / speedMoveBot;

        //        moveTween = gameObject.transform.DOMove(rdPos.transform.position, timeToMove).SetEase(Ease.Linear).OnComplete(() =>
        //        {
        //            MoveByBot();
        //        });

        //    }

        //    Vector3 dir = rdPos.transform.position - transform.position;
        //    if (dir != Vector3.zero)
        //    {
        //        gameObject.transform.rotation = Quaternion.LookRotation(dir);
        //        if (inkCount > 0)
        //        {
        //            _anim.SetBool("isRunning", true);
        //            _anim.SetBool("isRunWith", true);
        //        }
        //        else
        //        {
        //            _anim.SetBool("isRunWith", false);
        //            _anim.SetBool("isRunning", true);

        //        }
        //    }
        //    else
        //    {
        //        _anim.SetBool("isRunning", false);
        //    }
        //}

        //if (_groundState == GroundStateEnum.ground_3)
        //{
        //    //init var
        //    inkToTake = 5;

        //    var rdPos = gameObject.transform;
        //    var distance = 1f;
        //    var timeToMove = 1f;

        //    if (inkTook.Count >= inkToTake) // move to endPoint
        //    {
        //        if (colorType == myIDEnum.Green) rdPos = GameManager.ins.posGreenUnlock_Ground3.transform;
        //        if (colorType == myIDEnum.Blue) rdPos = GameManager.ins.posBlueUnlock_Ground3.transform;
        //        if (colorType == myIDEnum.Pink) rdPos = GameManager.ins.posPinkUnlock_Ground3.transform;


        //        //rdPos.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        //        distance = Vector3.Distance(rdPos.transform.position, transform.position);
        //        timeToMove = distance / speedMoveBot;

        //        moveTween = gameObject.transform.DOMove(rdPos.transform.position, timeToMove).SetEase(Ease.Linear).OnComplete(() =>
        //        {
        //            _anim.SetBool("isRunning", false);// finish collect 25 ink then wait at brige


        //            Utils.ins.DelayCall(2, () =>
        //            {
        //                if (GameManager.ins.isStopGame) return;
        //                rdPos.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        //                distance = Vector3.Distance(rdPos.transform.position, transform.position);
        //                timeToMove = distance / speedMoveBot;
        //                moveTween = gameObject.transform.DOMove(rdPos.transform.position, timeToMove * 2.5f).SetEase(Ease.Linear).OnComplete(() =>
        //                {
        //                    gameObject.transform.rotation = Quaternion.LookRotation(Vector3.zero);
        //                    _anim.SetTrigger("dance");
        //                    GameManager.ins.BotWin(gameObject);
        //                    gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
        //                });
        //                SetDirToPos(rdPos);

        //            });
        //            // move to end
        //        });




        //    }
        //    else // move to collect point
        //    {
        //        rdPos = GameManager.ins.botMovePos_Ground3.transform.GetChild(Random.Range(0, GameManager.ins.botMovePos.transform.childCount - 1));

        //        distance = Vector3.Distance(rdPos.transform.position, transform.position);
        //        timeToMove = distance / speedMoveBot;

        //        moveTween = gameObject.transform.DOMove(rdPos.transform.position, timeToMove).SetEase(Ease.Linear).OnComplete(() =>
        //        {
        //            MoveByBot();
        //        });

        //    }

        //}

    }

    public void SetDirToPos(Transform pos)
    {
        Vector3 dir = pos.transform.position - transform.position;
        if (dir != Vector3.zero)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(dir);
            if (inkCount > 0)
            {
                _anim.SetBool("isRunning", true);
                _anim.SetBool("isRunWith", true);
            }
            else
            {
                _anim.SetBool("isRunWith", false);
                _anim.SetBool("isRunning", true);

            }
        }
        else
        {
            _anim.SetBool("isRunning", false);
        }
    }




    public float timePaint;
    public void Paint()
    {
        if (canPaint == false) return;
        if (timePaint == 0) timePaint = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - timePaint >= .18f)
        {
            DropPaint();
            timePaint = Time.timeSinceLevelLoad;
        }


    }
    public void DropPaint()
    {
        if (inkTook.Count <= 0) return;
        foreach (Transform child in paintHideGr.transform.GetChild(0).transform)
        {
            if (child.gameObject.activeInHierarchy == true) paintHideLeft.Add(child.gameObject);
        }
        if (paintHideLeft[paintHideLeft.Count - 1] == null)
        {
            UnlockPaint();
            return;
        }

        var inkFlyEff = Instantiate(inkTook[inkTook.Count - 1], null, false);
        inkFlyEff.transform.position = inkTook[inkTook.Count - 1].transform.position;
        inkFlyEff.transform.rotation = inkTook[inkTook.Count - 1].transform.rotation;
        inkFlyEff.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        inkFlyEff.transform.DOMove(paintHideLeft[paintHideLeft.Count - 1].transform.position, 0.2f);
        Utils.ins.DelayCall(0.2f, () =>
        {
            var inkDropEff = Instantiate(inkDropParticle);
            inkDropEff.transform.position = inkFlyEff.transform.position;
            Destroy(inkFlyEff);
            Destroy(paintHideLeft[paintHideLeft.Count - 1].gameObject);
            if (paintHideLeft.Count == 0)
            {
                UnlockPaint();
            }
        });
        Destroy(inkTook[inkTook.Count - 1]);
        inkTook.Remove(inkTook[inkTook.Count - 1]);



        inkCount--;
    }


    public void SetupCurGround()
    {

        Debug.LogError(colorType);
        switch (currentGround.myGroundType)
        {
            case GroundTypeEnum.none:
                break;
            case GroundTypeEnum.reveal_Bridge:
                break;
            case GroundTypeEnum.dropInk_Bridge:
                break;
            case GroundTypeEnum.build_Bridge:
                if (colorType == myIDEnum.Red) myBuildBridge = currentGround.buildBridge_Red;
                if (colorType == myIDEnum.Blue) myBuildBridge = currentGround.buildBridge_Blue;
                if (colorType == myIDEnum.Green) myBuildBridge = currentGround.buildBridge_Green;
                if (colorType == myIDEnum.Pink) myBuildBridge = currentGround.buildBridge_Pink;

                if (colorType == myIDEnum.Red) myBuildBridgeInkPos = currentGround.buildBridge_RedInkPos;
                if (colorType == myIDEnum.Blue) myBuildBridgeInkPos = currentGround.buildBridge_BlueInkPos;
                if (colorType == myIDEnum.Green) myBuildBridgeInkPos = currentGround.buildBridge_GreenInkPos;
                if (colorType == myIDEnum.Pink) myBuildBridgeInkPos = currentGround.buildBridge_PinkInkPos;



                break;
            case GroundTypeEnum.final_Bridge:
                break;
        }
    }

    public bool canPaintBridge;
    [Button]
    public void BuildBridge()
    {
        if (myBuildBridge == null) return;

        if (myBuildBridge.transform.localScale.z >= 2.1f)  //unlock Bridge unlock ground 3
        {
            if (isBot == false)
                myBuildBridge.transform.GetChild(0).gameObject.SetActive(false);
            currentGroundIndex++;
            SpawnManager.ins.AddSpawn3(this);
            SpawnManager.ins.RemoveSpawn2(this);
            return;
        }
        if (canPaintBridge == false) return;


        if (timePaint == 0) timePaint = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - timePaint >= .25f)
        {
            DropInkBuildBridge();
            timePaint = Time.timeSinceLevelLoad;
        }

    }






    public GameObject myBuildBridge, myBuildBridgeInkPos;
    public void DropInkBuildBridge()
    {
        if (inkTook.Count <= 0) return;
        Debug.LogError("PAINT BRIDGE" + colorType);
        var inkFlyEff = Instantiate(inkTook[inkTook.Count - 1], null, false);
        inkFlyEff.transform.position = inkTook[inkTook.Count - 1].transform.position;
        inkFlyEff.transform.rotation = inkTook[inkTook.Count - 1].transform.rotation;
        inkFlyEff.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        inkFlyEff.transform.DOMove(myBuildBridgeInkPos.transform.position, 0.2f);
        Utils.ins.DelayCall(0.2f, () =>
        {
            var inkDropEff = Instantiate(inkDropParticle);
            inkDropEff.transform.position = inkFlyEff.transform.position;
            Destroy(inkFlyEff);
            var oldScale = myBuildBridge.transform.localScale;
            oldScale.z += 0.05f;
            myBuildBridge.transform.localScale = oldScale;
        });
        Destroy(inkTook[inkTook.Count - 1]);
        inkTook.Remove(inkTook[inkTook.Count - 1]);
        inkCount--;
    }








    public bool canLosePaint;
    public void LosePaint()
    {
        if (canLosePaint == false) return;
        if (timePaint == 0) timePaint = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - timePaint >= 0.3f)// fixlater 0.3
        {


            if (inkTook.Count <= 0) return;

            Destroy(inkTook[inkTook.Count - 1]);
            inkTook.Remove(inkTook[inkTook.Count - 1]);
            inkCount--;


            timePaint = Time.timeSinceLevelLoad;
        }
    }









    public Animator _anim;
    public GameObject inkContainer;


    public void CheckAnim(string animName)
    {
        _anim.Play(animName);
    }


    public List<GameObject> inkTook;
    public int inkCount;
    public bool GetInk(GameObject ink)
    {
        if (inkCount == inkContainer.transform.childCount) return false;
        if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Fall") return false;

        ink.GetComponent<MoveToPos>().posToMoveBackPlayer = inkContainer.transform.GetChild(inkCount).gameObject;
        inkTook.Add(ink);
        inkCount++;

        return true;



    }


    public List<GameObject> paintHideLeft;
    public GameObject paintHideGr;
    [Button]
    public void Paint(GameObject ink = null, GameObject paintHideGr11 = null)
    {
        Debug.LogError("paint");
        canPaint = true;
        inkTook.Reverse();

        StartCoroutine(Paint());

        IEnumerator Paint()
        {
            var index = 0;


            foreach (Transform child in paintHideGr.transform.GetChild(0).transform)
            {
                if (child.gameObject.activeInHierarchy == true) paintHideLeft.Add(child.gameObject);
            }

            var inkTookTemp = inkTook;
            for (int i = 0; i < inkTookTemp.Count; i++)
            {
                yield return new WaitForSeconds(.15f);
                if (canPaint == false) yield break;

                if (paintHideLeft.Count > index)
                {
                    inkTookTemp[i].GetComponent<MoveToPos>().posToMovePaint = paintHideLeft[index];
                    inkTookTemp[i].GetComponent<MoveToPos>().enabled = true;
                    Debug.LogError("remove " + i);
                    inkTook.Remove(inkTookTemp[i]);
                    index++;
                }

            }

            inkCount = 0;

            yield return new WaitForSeconds(2);

            foreach (Transform child in paintHideGr.transform)
            {
                if (child.gameObject.activeInHierarchy == true) paintHideLeft.Add(child.gameObject);
            }
            if (paintHideLeft.Count == 0)
            {
                UnlockPaint();
            }

        }
    }
    public bool canPaint;
    public void StopPaint()
    {
        paintHideLeft.Clear();
        Debug.LogError("stop paint");
        canPaint = false;
    }





    public GameObject paint;
    [Button]
    public void UnlockPaint()//unlock ground 2
    {
        paint.transform.DOMoveY(-3, 1);
        currentGroundIndex++;
        SpawnManager.ins.AddSpawn2(this);
        SpawnManager.ins.RemoveSpawn1(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "#_3_waterCollider")
        {
            Debug.LogError("ENTER water");
            speedMove = 1.2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "#_3_waterCollider")
        {
            Debug.LogError("EXIT water");

            speedMove = 3;
        }
    }


    [Button]

    public void TouchOtherStick(int enemyInkCount)
    {
        if (inkCount >= enemyInkCount) return;
        _anim.SetTrigger("fall");
        KillTween();
        InkFall();






    }
    public List<GameObject> inksRemove;
    public GameObject test;
    [Button]
    public void InkFall()
    {
        inkCount = 0;

        var inkToDrop = gameObject;


        if (colorType == myIDEnum.Red) inkToDrop = SpawnManager.ins.inkBase[0];
        if (colorType == myIDEnum.Blue) inkToDrop = SpawnManager.ins.inkBase[1];
        if (colorType == myIDEnum.Green) inkToDrop = SpawnManager.ins.inkBase[2];
        if (colorType == myIDEnum.Pink) inkToDrop = SpawnManager.ins.inkBase[3];

        var deleteList = new List<GameObject>();
        inkTook.ForEach(ink =>
        {

            var newInk = Instantiate(inkToDrop, SpawnManager.ins.gameObject.transform);
            newInk.GetComponent<Collider>().enabled = false;
            Utils.ins.DelayCall(3, () =>
            {
                newInk.GetComponent<Collider>().enabled = true;
            });

            newInk.transform.position = transform.position;
            ink.gameObject.SetActive(false);
            deleteList.Add(ink);
            var posEnd = transform.position;
            posEnd.x += Random.Range(-3f, 3);
            posEnd.z += Random.Range(-3f, 3);
            newInk.transform.DOJump(posEnd, 2, 1, 0.6f).SetEase(Ease.Linear);
        });


        foreach (GameObject go in inkTook)
        {
            Destroy(go);
        }
        inkTook.Clear();
    }


    public bool isUnlockGround1, isUnlockGround2, isUnlockGround3;
    public GroundStateEnum _groundState;


    public GameObject finalPaint;
    public GameObject posMoveEnd;
    public List<GameObject> finalPaintHide;
    [Button]
    public void WinGame()
    {
        canMove = false;
        GameManager.ins.CameraTo(finalPaint);

        charPink.gameObject.SetActive(false);//fixlater
        charGreen.gameObject.SetActive(false);
        charBlue.gameObject.SetActive(false);

        transform.DOMove(posMoveEnd.transform.position, 1).OnComplete(() =>
        {
            _anim.SetBool("isRunning", false);
            _rigid.velocity = Vector3.zero;
            _rigid.angularVelocity = Vector3.zero;
        });
        SetDirToPos(posMoveEnd.transform);



        if (inkTook.Count == 0)
        {
            GameManager.ins.PlayerWin();
            return;
        }

        List<GameObject> inkTemp = new List<GameObject>(inkTook);



        int i = 0;

        StartCoroutine(inkFly());
        IEnumerator inkFly()
        {

            foreach (GameObject child in inkTemp)
            {
                yield return new WaitForSeconds(0.5f);
                var tempi = i;

                if (finalPaintHide[finalPaintHide.Count - 1] == null)
                {
                    Debug.LogError("!!!");
                    GameManager.ins.PlayerWin();
                    yield break;
                }


                if (inkTook.Count == 0)
                {
                    GameManager.ins.PlayerWin();
                    yield break;
                }

                var inkFlyEff = Instantiate(inkTook[inkTook.Count - 1], null, false);


                inkFlyEff.transform.position = inkTook[inkTook.Count - 1].transform.position;
                inkFlyEff.transform.rotation = inkTook[inkTook.Count - 1].transform.rotation;
                inkFlyEff.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                inkFlyEff.transform.DOMove(finalPaintHide[tempi].transform.position, 0.2f).OnComplete(() =>
                {
                    var inkDropEff = Instantiate(inkDropParticle);
                    inkDropEff.transform.position = inkFlyEff.transform.position;
                    Destroy(inkFlyEff);
                    Destroy(finalPaintHide[tempi].gameObject);
                });
                Destroy(inkTook[inkTook.Count - 1]);
                inkTook.Remove(inkTook[inkTook.Count - 1]);
                i++;
            }

            GameManager.ins.PlayerWin();


        }

    }








}

public enum GroundStateEnum
{
    none,
    ground_1,
    ground_2,
    ground_3,

}