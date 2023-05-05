using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class AttackNote : Note
{
<<<<<<< Updated upstream
    public Direction dir;
    
    public int damage;
=======
    private int monsterIndex = 0;

    public Direction dir;
    
>>>>>>> Stashed changes
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
