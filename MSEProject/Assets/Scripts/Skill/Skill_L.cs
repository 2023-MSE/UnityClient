using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_L : Skill
{

    private String name;
    private int damage;

   
    public void DoSkill()
    {

    }

    public int getDamage()
    {
        return damage;
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

}
