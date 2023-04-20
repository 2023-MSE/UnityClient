using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 120;
    
    private String dir= " ";
    private int damage=0;
   
    public int getdamage()
    {
        return damage ;
    }

    public void setdamage(int _damage)
    {
        damage = _damage;
    }

    public String getDirection()
    {
        return dir;
    }

    public void setDirection(String _dir)
    {
        dir = _dir;
    }
 
    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }
}
