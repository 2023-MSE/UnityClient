using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_U : Skill
{

    private String name;
    private int damage = 2;

 
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
