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
        Paint_Update(); //ground 1
        BuildBridge_Update(); //ground 2
        LosePaintBridge_Update();  // ground 3
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
        if (inkTook.Count >= inkToTake) // move to unlock bridge point
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




    float timePaint;
    public void Paint_Update()
    {
        if (canPaintReavealBridge == false) return;

        if (timePaint == 0) timePaint = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - timePaint >= .18f)
        {
            DropPaintRevealBridge();
            timePaint = Time.timeSinceLevelLoad;
        }

        Debug.LogError(11);
    }
    public void DropPaintRevealBridge()
    {
        if (inkTook.Count <= 0) return;

        foreach (Transform child in revealPaintGr.transform.GetChild(0).transform)
        {
            if (child.gameObject.activeInHierarchy == true) revealInkLeft.Add(child.gameObject);
        }
        if (revealInkLeft[revealInkLeft.Count - 1] == null)
        {
            UnlockRevealBridge();
            return;
        }

        var inkFlyEff = Instantiate(inkTook[inkTook.Count - 1], null, false);
        inkFlyEff.transform.position = inkTook[inkTook.Count - 1].transform.position;
        inkFlyEff.transform.rotation = inkTook[inkTook.Count - 1].transform.rotation;
        inkFlyEff.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        inkFlyEff.transform.DOMove(revealInkLeft[revealInkLeft.Count - 1].transform.position, 0.2f);
        Utils.ins.DelayCall(0.2f, () =>
        {
            var inkDropEff = Instantiate(inkDropParticle);
            inkDropEff.transform.position = inkFlyEff.transform.position;
            Destroy(inkFlyEff);
            Destroy(revealInkLeft[revealInkLeft.Count - 1].gameObject);
            if (revealInkLeft.Count == 0)
            {
                UnlockRevealBridge();
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


                if (colorType == myIDEnum.Blue)
                {
                    revealPaintGr = currentGround.reavealBridge_Blue;
                    revealPaint = currentGround.reavealPaint_Blue;
                }
                if (colorType == myIDEnum.Pink)
                {
                    revealPaintGr = currentGround.reavealBridge_Pink;
                    revealPaint = currentGround.reavealPaint_Pink;

                }
                if (colorType == myIDEnum.Green)
                {
                    revealPaintGr = currentGround.reavealBridge_Green;
                    revealPaint = currentGround.reavealPaint_Green;

                }
                if (colorType == myIDEnum.Red)
                {
                    revealPaintGr = currentGround.reavealBridge_Red;
                    revealPaint = currentGround.reavealPaint_Red;

                }











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
    public void BuildBridge_Update()
    {
        if (myBuildBridge == null) return;

        if (myBuildBridge.transform.localScale.z >= 0.5f)  //unlock Bridge unlock ground 3 config bridge size
        {
            if (isBot == false)
                myBuildBridge.transform.GetChild(0).gameObject.SetActive(false);
            GoNextGround();
            return;
        }
        if (canPaintBridge == false) return;


        if (timePaint == 0) timePaint = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - timePaint >= 0.25f)
        {
            DropInkBuildBridge();
            timePaint = Time.timeSinceLevelLoad;
        }

    }






    public GameObject myBuildBridge, myBuildBridgeInkPos;
    public void DropInkBuildBridge()
    {
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
    public void LosePaintBridge_Update()
    {
        if (canLosePaint == false) return;
        if (timePaint == 0) timePaint = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - timePaint >= 0.3f)// fixlater 0.3
        {

            if (inkTook.Count <= 0) return;
            var ink = inkTook[inkTook.Count - 1];
            var inkDropEff = Instantiate(inkDropParticle);
            inkDropEff.transform.position = ink.transform.position;


            Destroy(ink);
            inkTook.Remove(ink);
            inkCount--;


            timePaint = Time.timeSinceLevelLoad;
        }
    }



    public void GoNextGround()
    {
        currentGroundIndex++;
        currentGround.isUnlocked = true;

        // reset old data ground
        myBuildBridge = null;
        myBuildBridgeInkPos = null;


        //setup new ground
        currentGround = GameManager.ins.grounds[currentGroundIndex];
        SetupCurGround();



        GameManager.ins.grounds[currentGroundIndex].CharacterArrive(this);

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

    public List<GameObject> revealInkLeft;
    public GameObject revealPaintGr;
    [Button]
    public void Paint(GameObject ink = null, GameObject paintHideGr11 = null)
    {
        Debug.LogError("paint");
        canPaintReavealBridge = true;
        inkTook.Reverse();

        StartCoroutine(Paint());

        IEnumerator Paint()
        {
            var index = 0;


            foreach (Transform child in revealPaintGr.transform.GetChild(0).transform)
            {
                if (child.gameObject.activeInHierarchy == true) revealInkLeft.Add(child.gameObject);
            }

            var inkTookTemp = inkTook;
            for (int i = 0; i < inkTookTemp.Count; i++)
            {
                yield return new WaitForSeconds(.15f);
                if (canPaintReavealBridge == false) yield break;

                if (revealInkLeft.Count > index)
                {
                    inkTookTemp[i].GetComponent<MoveToPos>().posToMovePaint = revealInkLeft[index];
                    inkTookTemp[i].GetComponent<MoveToPos>().enabled = true;
                    Debug.LogError("remove " + i);
                    inkTook.Remove(inkTookTemp[i]);
                    index++;
                }

            }

            inkCount = 0;

            yield return new WaitForSeconds(2);

            foreach (Transform child in revealPaintGr.transform)
            {
                if (child.gameObject.activeInHierarchy == true) revealInkLeft.Add(child.gameObject);
            }
            if (revealInkLeft.Count == 0 && currentGround.isUnlocked == false)
            {
                UnlockRevealBridge();
            }

        }
    }
    public bool canPaintReavealBridge;
    public void StopPaint()
    {
        revealInkLeft.Clear();
        Debug.LogError("stop paint");
        canPaintReavealBridge = false;
    }





    public GameObject revealPaint;
    [Button]
    public void UnlockRevealBridge()//unlock ground 2
    {
        if (revealPaintGr.transform.parent.name == "#UNLOCKED") return;
        revealPaintGr.transform.parent.name = "#UNLOCKED";
        revealPaint.transform.DOMoveY(-3, 1);
        GoNextGround();
        Debug.LogError("UNLOCK REAVEAL" + colorType);
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

        var inkToDrop = Refer.ins.whiteInk;




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

