using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class AttackNote : Note
{
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


}