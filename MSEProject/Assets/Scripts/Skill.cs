using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private int type;
    private int damage;
    private bool isSplash;
    private bool isEnable;
    private int dirUp;
    private int dirDown;
    private int dirLeft;
    private int dirRight;

    public Skill(int type, int damage, bool isSplash, bool isEnable, int dirUp, int dirDown, int dirLeft, int dirRight)
    {
        this.type = type;
        this.damage = damage;
        this.isSplash = isSplash;
        this.isEnable = isEnable;
        this.dirUp = dirUp;
        this.dirDown = dirDown;
        this.dirLeft = dirLeft;
        this.dirRight = dirRight;
    }

    public int getType()
    {
        return type;
    }

    public int getDamage()
    {
        return damage;
    }

    public bool getSplash()
    {
        return isSplash;
    }
    public bool getEnable()
    {
        return isEnable;
    }
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
}
