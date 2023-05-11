using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenalizeNote : Note
{
    public Direction getDirection()
    {
       
        return dir;
    }
    public void setDirection(Direction _dir)
    {
  
        dir = _dir;
    }
}