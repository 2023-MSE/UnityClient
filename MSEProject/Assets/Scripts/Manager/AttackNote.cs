using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;


public class AttackNote : Note
{
    public String dir;
    
    public int damage;
    public void Start()
    {
       Debug.Log(dir + " " + damage);
    }

    public int getdamage()
    {
        Debug.Log("It is AttackNote | getdamage");
        return damage;
    }

    public String getDirection()
    {
        Debug.Log("It is AttackNote | getDirection");
        return dir;
    }

    
}
