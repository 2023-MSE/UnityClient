using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{

    private String name;
    private int damage;

    public virtual void DoSkill();
    public virtual int getDamage();
    public virtual void setDamage(int damage);

}
