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
public class LevelManager : MonoBehaviour
{


    [TableList(ShowIndexLabels = true)]
    public List<Level> levelList;





    private void Awake()
    {
    }
    private void Start()
    {
        SpawnLevel();
    }

    [Button]
    public void SpawnLevel()
    {
        var curLevel = UserData.GetCurLevel();


        levelList[curLevel].groundData.ForEach(ground =>
        {
            var index = levelList[curLevel].groundData.IndexOf(ground);
            Debug.LogError(index);
            var newGroundRef = final_Bridge;
            var curGroundType = ground.groundType;
            switch (ground.groundType)
            {
                case GroundTypeEnum.none:
                    newGroundRef = null;
                    break;
                case GroundTypeEnum.reveal_Bridge:
                    newGroundRef = reveal_Bridge;
                    break;
                case GroundTypeEnum.dropInk_Bridge:
                    newGroundRef = dropInk_Bridge;
                    break;
                case GroundTypeEnum.build_Bridge:
                    newGroundRef = build_Bridge;
                    break;
                case GroundTypeEnum.final_Bridge:
                    newGroundRef = final_Bridge;
                    break;
            }


            var newGround = Instantiate(newGroundRef, groundGr.transform);
            var pos = Vector3.zero;
            pos.z = index * 30;
            newGround.transform.position = pos;
            newGround.GetComponent<GroundBase>().myGroundType = curGroundType;
            GameManager.ins.grounds.Add(newGround.GetComponent<GroundBase>());
            if (CharacterControl.charBlue.currentGround == null)
            {
                CharacterControl.charBlue.currentGround = newGround.GetComponent<GroundBase>();
                CharacterControl.charGreen.currentGround = newGround.GetComponent<GroundBase>();
                CharacterControl.charPink.currentGround = newGround.GetComponent<GroundBase>();
                CharacterControl.charPlayer.currentGround = newGround.GetComponent<GroundBase>();

            }






            if (ground.hasVideoLeft)
            {
                var vidGround = Instantiate(video_BridgeLeft, groundGr.transform);
                pos = vidGround.transform.position;
                pos.z = index * 30 + 15;
                vidGround.transform.position = pos;
            }

            if (ground.hasVideoRight)
            {
                var vidGround = Instantiate(video_BridgeRight, groundGr.transform);
                pos = vidGround.transform.position;
                pos.z = index * 30 + 15;
                vidGround.transform.position = pos;
            }

        });





    }

    public GameObject reveal_Bridge, dropInk_Bridge, build_Bridge, final_Bridge, video_BridgeLeft, video_BridgeRight, groundGr;
    public int currentLevel;

}
[Serializable]
public class Level
{
    [TableList(ShowIndexLabels = true)]
    public List<GroundLevel> groundData;

}



[Serializable]
public class GroundLevel
{
    public bool hasVideoLeft, hasVideoRight;

    public GroundTypeEnum groundType;
    public float hard;
}
[Serializable]
public enum GroundTypeEnum
{
    none,
    reveal_Bridge,
    dropInk_Bridge,
    build_Bridge,
    final_Bridge,


}