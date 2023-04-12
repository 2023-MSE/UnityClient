using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    private bool[] pattern = new bool[4];
    private int power;
    private int type;
    public override void hitMotion()
    {
        /*TODO*/
    }
    public override void dead()
    {
        /*TODO*/
        Debug.Log("Monster dead");
        this.GetComponent<GameObject>().SetActive(false);
    }

    public void attactMotion()
    {
        /*TODO*/
    }

    public void setPower(int power)
    {
        this.power = power;
    }
    public int getPower()
    {
        return power;
    }

    public int getType()
    {
        return power;
    }
}
