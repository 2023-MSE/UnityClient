using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

[Serializable]
public class Skill
{
    public GameObject effect;
    public int type;
    public int damage;
    public bool isSplash;
    public bool isEnable;
    public int dirUp;
    public int dirDown;
    public int dirLeft;
    public int dirRight;
    public int getNextSkill(Direction direction) 
    {
        switch(direction)
        {
            case Direction.UP:
                return dirUp;
            case Direction.DOWN:
                return dirDown;
            case Direction.LEFT:
                return dirLeft;
            case Direction.RIGHT:
                return dirRight;
            default:
                return -1;
        }
    }

    public void seteffect(Vector3 pos)
    {
        effect.gameObject.transform.position = pos;
        effect.SetActive(true);    
    }
}
