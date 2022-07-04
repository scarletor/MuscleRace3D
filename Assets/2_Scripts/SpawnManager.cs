using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class SpawnManager : MonoBehaviour
{



    public static SpawnManager ins;

    private void Awake()
    {
        ins = this;

    }




    public GameObject demo, inkSpawnPosGr, blueInk, redInk, greenInk, pinkInk, inkSpawnPosGr2, inkSpawnPosGr3;
    public List<GameObject>inkBase, inks, inks2, inks3;
    private void Start()
    {
        Spawn1();
        Spawn2();
        Spawn3();
    }



    public int spawned, stopSpawn;
    public float spawnTime;



    public void Spawn1()
    {
        InvokeRepeating("SpawnInteval1", 1, spawnTime);
    }


    public void Spawn2()
    {
        InvokeRepeating("SpawnInteval2", 1, spawnTime);
    }


    public void Spawn3()
    {
        InvokeRepeating("SpawnInteval3", 1, spawnTime);
    }

    void SpawnInteval1()
    {
        if (spawned >= stopSpawn)
        {
            CancelInvoke("SpawnInteval1");
        }
        var newInk = Instantiate(inks[Random.Range(0, inks.Count)], transform, true);

        var rdPos = inkSpawnPosGr.transform.GetChild(Random.Range(0, inkSpawnPosGr.transform.childCount - 1));

        newInk.transform.position = rdPos.transform.position;

        spawned++;
    }


    [Button]
    public void RemoveSpawn1(CharacterControl charac)
    {
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inks.Remove(blueInk);
        if (colorType == myIDEnum.Pink) inks.Remove(pinkInk);
        if (colorType == myIDEnum.Green) inks.Remove(greenInk);
        if (colorType == myIDEnum.Red) inks.Remove(redInk);
    }



    public int spawned2, stopSpawn2;

    void SpawnInteval2()
    {
        if (inks2.Count == 0) return;
        if (spawned2 >= stopSpawn2)
        {
            CancelInvoke("SpawnInteval2");
        }
        var newInk = Instantiate(inks2[Random.Range(0, inks2.Count)], transform, true);

        var rdPos = inkSpawnPosGr2.transform.GetChild(Random.Range(0, inkSpawnPosGr2.transform.childCount - 1));

        newInk.transform.position = rdPos.transform.position;

        spawned2++;
    }


    [Button]
    public void RemoveSpawn2(CharacterControl charac)
    {
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inks2.Remove(blueInk);
        if (colorType == myIDEnum.Pink) inks2.Remove(pinkInk);
        if (colorType == myIDEnum.Green) inks2.Remove(greenInk);
        if (colorType == myIDEnum.Red) inks2.Remove(redInk);
    }

    [Button]
    public void AddSpawn2(CharacterControl charac)
    {
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inks2.Add(blueInk);
        if (colorType == myIDEnum.Pink) inks2.Add(pinkInk);
        if (colorType == myIDEnum.Green) inks2.Add(greenInk);
        if (colorType == myIDEnum.Red) inks2.Add(redInk);
    }






    public int spawned3, stopSpawn3;

    void SpawnInteval3()
    {
        if (inks3.Count == 0) return;
        if (spawned3 >= stopSpawn3)
        {
            CancelInvoke("SpawnInteval3");
        }
        var newInk = Instantiate(inks3[Random.Range(0, inks3.Count)], transform, true);

        var rdPos = inkSpawnPosGr3.transform.GetChild(Random.Range(0, inkSpawnPosGr3.transform.childCount - 1));

        newInk.transform.position = rdPos.transform.position;

        spawned3++;
    }


    [Button]
    public void RemoveSpawn3(CharacterControl charac)
    {
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inks3.Remove(blueInk);
        if (colorType == myIDEnum.Pink) inks3.Remove(pinkInk);
        if (colorType == myIDEnum.Green) inks3.Remove(greenInk);
        if (colorType == myIDEnum.Red) inks3.Remove(redInk);
    }

    [Button]
    public void AddSpawn3(CharacterControl charac)
    {
        var colorType = charac.colorType;
        if (colorType == myIDEnum.Blue) inks3.Add(blueInk);
        if (colorType == myIDEnum.Pink) inks3.Add(pinkInk);
        if (colorType == myIDEnum.Green) inks3.Add(greenInk);
        if (colorType == myIDEnum.Red) inks3.Add(redInk);
    }









}
