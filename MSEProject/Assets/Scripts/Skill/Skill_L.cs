using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_L : Skill
{

    private String name;
    private int damage;

   
    public override void DoSkill()
    {
        
    }
    public override int getDamage()
    {
        return damage;
    }
    public override void setDamage(int damage)
    {
        this.damage = damage;
    }

}
