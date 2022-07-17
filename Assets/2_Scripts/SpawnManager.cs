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


    public int spawn2_red, spawn2_blue, spawn2_green, spawn2_pink, spawn3_red, spawn3_pink, spawn3_blue, spawn3_green;

    public GameObject demo, inkSpawnPosGr, blueInk, redInk, greenInk, pinkInk, inkSpawnPosGr2, inkSpawnPosGr3;
    public List<GameObject> inkBase, inks, inks2, inks3;
    private void Start()
    {
        return;
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
        if (inks.Count == 0) return;
        var rdPos = inkSpawnPosGr.transform.GetChild(Random.Range(0, inkSpawnPosGr.transform.childCount - 1));
        if (rdPos.transform.childCount != 0) return;

        var newInk = inks[Random.Range(0, inks.Count)];



        newInk = Instantiate(inks[Random.Range(0, inks.Count)], transform, true);


        newInk.transform.position = rdPos.transform.position;
        newInk.transform.SetParent(rdPos.transform);
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
        var rdPos = inkSpawnPosGr2.transform.GetChild(Random.Range(0, inkSpawnPosGr2.transform.childCount - 1));
        if (rdPos.transform.childCount != 0) return;

        var newInk = inks2[Random.Range(0, inks2.Count)];
        if (newInk == redInk) spawn2_red++;
        if (newInk == blueInk) spawn2_blue++;
        if (newInk == greenInk) spawn2_green++;
        if (newInk == pinkInk) spawn2_pink++;
        if (spawn2_red >= 25) inks2.Remove(redInk);
        if (spawn2_blue >= 25) inks2.Remove(blueInk);
        if (spawn2_green >= 25) inks2.Remove(greenInk);
        if (spawn2_pink >= 25) inks2.Remove(pinkInk);






        newInk = Instantiate(inks2[Random.Range(0, inks2.Count)], transform, true);





        newInk.transform.position = rdPos.transform.position;
        newInk.transform.SetParent(rdPos.transform);

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
