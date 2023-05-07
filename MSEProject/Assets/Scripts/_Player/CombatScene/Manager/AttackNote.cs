using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class AttackNote : Note
{
    private int monsterIndex = 0;
    public Direction dir;
    public int damage;

    public void Start()
    {
        Debug.Log(dir + " " + damage);
    }

    public int getdamage()
    {
        return damage;
    }

    public Direction getDirection()
    {
        return dir;
    }

    public int GetMonsterIndex()
    {
        return monsterIndex;
    }
}