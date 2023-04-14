using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenalizeNote: Note
{
   public Direction dir;
   public Direction getDirection()
   {
      Debug.Log("It is GenalizeNote | getDirection");
      return dir;
   }
   public void setDirection(Direction _dir)
   {
      Debug.Log("It is GenalizeNote | setDirection");
      dir = _dir;
   }
   
}
