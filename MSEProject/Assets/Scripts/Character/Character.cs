using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected float maxHp = 100;
    protected float hp = 0;

    public abstract void hitMotion();
    public abstract void dead();
    public float getHp()
    {
        return hp;
    }
    public void setHp(float value)
    {
        hp = hp + value >= maxHp ? maxHp : hp + value;
        Debug.Log("Hp is " + hp);
        if (hp < 0)
            dead();
    }
}
