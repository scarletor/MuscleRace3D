using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class GroundBase : MonoBehaviour
{


    public List<GameObject> arrived, inkPrefToSpawn;
    public GroundTypeEnum myGroundType;

    public int spawned, stopSpawn;

    public GameObject inkSpawnPosGr, botMovePosGr,posGreenUnlock,posBlueUnlock,posPinkUnlock, buildBridge_Blue, buildBridge_BlueInkPos, buildBridge_Pink, buildBridge_PinkInkPos,
        buildBridge_Green, buildBridge_GreenInkPos, buildBridge_Red, buildBridge_RedInkPos;

    private void Start()
    {
        SpawnInk();
    }

    [Button]
    public void CharacterArrive(CharacterControl charac)
    {
        Debug.LogError("arrive:" + charac.colorType);
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inkPrefToSpawn.Add(Refer.ins.blueInk);
        if (colorType == myIDEnum.Pink) inkPrefToSpawn.Add(Refer.ins.pinkInk);
        if (colorType == myIDEnum.Green) inkPrefToSpawn.Add(Refer.ins.greenInk);
        if (colorType == myIDEnum.Red) inkPrefToSpawn.Add(Refer.ins.redInk);

    }


    [Button]
    public void SpawnInk()
    {
        InvokeRepeating("SpawnInteval", 1, 0.2f);
    }


    void SpawnInteval()
    {
        if (spawned >= stopSpawn)
        {
            CancelInvoke("SpawnInteval1");
        }
        if (inkPrefToSpawn.Count == 0) return;
        var rdPos = inkSpawnPosGr.transform.GetChild(Random.Range(0, inkSpawnPosGr.transform.childCount - 1));
        if (rdPos.transform.childCount != 0) return;

        var newInk = inkPrefToSpawn[Random.Range(0, inkPrefToSpawn.Count)];



        newInk = Instantiate(inkPrefToSpawn[Random.Range(0, inkPrefToSpawn.Count)], transform, true);


        newInk.transform.position = rdPos.transform.position;
        newInk.transform.SetParent(rdPos.transform);
        spawned++;
    }

}
