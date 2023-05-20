using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenalizeNote : Note
{
    private int monsterIndex = 0;
    public Direction getDirection()
    {
       
        return dir;
    }
    public void setDirection(Direction _dir)
    {
  
        dir = _dir;
    }
    public int GetMonsterIndex()
    {
        return monsterIndex;
    }
    public void SetMonsterIndex(int monster)
    {
        
        monsterIndex = monster;
        
        // Debug.Log("SET MONSTER INDEX: "+monsterIndex);
    }
}