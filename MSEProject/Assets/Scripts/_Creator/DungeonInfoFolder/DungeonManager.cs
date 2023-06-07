using System;
using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonManager : Singleton<DungeonManager>
{
    public DungeonList MyDungeonList { get; set; }

    protected override void Awake()
    {
        base.Awake();
        MyDungeonList = new DungeonList();
    }

    private void Start()
    {
        // Test code
        // GetDungeonList();
    }

    public void ShowAllDungeons()
    {
        foreach (var eachDungeon in MyDungeonList.myDungeons)
        {
            Debug.Log(eachDungeon.name);
        }
    }

    public void GetDungeonList()
    {
        // !!! Test Method !!!
        MyDungeonList = new DungeonList();
        MyDungeonList.myDungeons = new List<Dungeon>();

        /*for (int i = 0; i < Random.Range(10,20); i++)
        {
            Dungeon tempDungeon = new Dungeon(true);
            switch (Random.Range(0,4))
            {
                case 0 :
                    tempDungeon.name = "박상열의 던전 " + Random.Range(0, 100);
                    tempDungeon.createdTime = DateTime.Now.ToString("HH : mm : ss");
                    break;
                case 1 :
                    tempDungeon.name = "전유미의 던전 " + Random.Range(0, 100);
                    tempDungeon.createdTime = DateTime.Now.ToString("HH : mm : ss");
                    break;
                case 2 :
                    tempDungeon.name = "이재욱의 던전 " + Random.Range(0, 100);
                    tempDungeon.createdTime = DateTime.Now.ToString("HH : mm : ss");
                    break;
                case 3 :
                    tempDungeon.name = "김휘년의 던전 " + Random.Range(0, 100);
                    tempDungeon.createdTime = DateTime.Now.ToString("HH : mm : ss");
                    break;
            }
            MyDungeonList.myDungeons.Add(tempDungeon);
        }*/
    }

    public void GetDungeonList(DungeonList inputDungeonList)
    {
        // Json have to give this item.
    }
}
