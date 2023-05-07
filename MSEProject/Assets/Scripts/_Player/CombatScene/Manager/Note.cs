using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 120;

    protected Direction dir;
    protected int damage = 0;

    public int getdamage()
    {
        return damage;
    }

    public void setdamage(int _damage)
    {
        damage = _damage;
    }

    public Direction getDirection()
    {
        return dir;
    }

    public void setDirection(Direction _dir)
    {
        dir = _dir;
    }

    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }
}