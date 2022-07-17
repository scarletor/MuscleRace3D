using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class GroundBase : MonoBehaviour
{
    public bool isUnlocked;

    public List<GameObject> inkPrefToSpawn;
    public List<CharacterControl> arrivedChar;
    public GroundTypeEnum myGroundType;

    public int spawnedRed, spawnedPink, spawnedGreen, spawnedBlue, spawned, stopSpawn, spawnLimitPerColor;

    public GameObject inkSpawnPosGr, botMovePosGr, posGreenUnlock, posBlueUnlock, posPinkUnlock, buildBridge_Blue, buildBridge_BlueInkPos, buildBridge_Pink, buildBridge_PinkInkPos,
        buildBridge_Green, buildBridge_GreenInkPos, buildBridge_Red, buildBridge_RedInkPos;

    [TitleGroup("REVEAL___________________")]

    public GameObject reavealBridge_Red, reavealBridge_Blue, reavealBridge_Green, reavealBridge_Pink;
    public GameObject reavealPaint_Red, reavealPaint_Blue, reavealPaint_Green, reavealPaint_Pink;

    private void Start()
    {
        if (myGroundType == GroundTypeEnum.final_Bridge) return;

        Debug.LogError("START;;;");
        spawnLimitPerColor = 14;
        SpawnInk();
        InvokeRepeating("ChecKNumberInkSpawnedIntervals", 1, 0.2f);
    }

    [Button]
    public void CharacterArrive(CharacterControl charac)
    {
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inkPrefToSpawn.Add(Refer.ins.blueInk);
        if (colorType == myIDEnum.Pink) inkPrefToSpawn.Add(Refer.ins.pinkInk);
        if (colorType == myIDEnum.Green) inkPrefToSpawn.Add(Refer.ins.greenInk);
        if (colorType == myIDEnum.Red) inkPrefToSpawn.Add(Refer.ins.redInk);
        arrivedChar.Add(charac);
    }
    [Button]
    public void CharacterLeave(CharacterControl charac)
    {
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inkPrefToSpawn.Remove(Refer.ins.blueInk);
        if (colorType == myIDEnum.Pink) inkPrefToSpawn.Remove(Refer.ins.pinkInk);
        if (colorType == myIDEnum.Green) inkPrefToSpawn.Remove(Refer.ins.greenInk);
        if (colorType == myIDEnum.Red) inkPrefToSpawn.Remove(Refer.ins.redInk);
        arrivedChar.Remove(charac);
    }
    [Button]
    public void SpawnInk()
    {
        Debug.LogError("START12121;;;");

        InvokeRepeating("SpawnInteval", 1, 0.05f);
    }

    [Button]
    void SpawnInteval()//fixlater call every frame
    {
        //guard
        if (inkPrefToSpawn.Count == 0) return;
        var rdPos = inkSpawnPosGr.transform.GetChild(Random.Range(0, inkSpawnPosGr.transform.childCount - 1));
        if (rdPos.transform.childCount != 0) return;

        var newInk = inkPrefToSpawn[Random.Range(0, inkPrefToSpawn.Count)];
        if (newInk.name.Contains("Pink") && spawnedPink >= 12) return;
        if (newInk.name.Contains("Green") && spawnedGreen >= 12) return;
        if (newInk.name.Contains("Red") && spawnedRed >= 12) return;
        if (newInk.name.Contains("Blue") && spawnedBlue >= 12) return;
        //end guard


        newInk = inkPrefToSpawn[Random.Range(0, inkPrefToSpawn.Count)];
        newInk = Instantiate(inkPrefToSpawn[Random.Range(0, inkPrefToSpawn.Count)], transform, true);


        newInk.transform.position = rdPos.transform.position;
        newInk.transform.SetParent(rdPos.transform);
        spawned++;
    }



    public void ChecKNumberInkSpawnedIntervals()//fix later call many time
    {
        spawnedBlue = 0;
        spawnedGreen = 0;
        spawnedRed = 0;
        spawnedPink = 0;
        foreach (Transform child in inkSpawnPosGr.transform)
        {
            if (child.transform.childCount != 0)
            {

                if (child.transform.GetChild(0).name.Contains("Blue")) { spawnedBlue++; };
                if (child.transform.GetChild(0).name.Contains("Red")) { spawnedRed++; };
                if (child.transform.GetChild(0).name.Contains("Green")) { spawnedGreen++; };
                if (child.transform.GetChild(0).name.Contains("Pink")) { spawnedPink++; };
            }
        }


    }


    [TitleGroup("DROP INK ___________________")]

    float  a;




}
