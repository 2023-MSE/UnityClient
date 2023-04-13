using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;
using Direction = DefaultNamespace.Direction;

public class GenalizeNote: Note
{
   public String dir;
   public String getDirection()
   {
      Debug.Log("It is GenalizeNote | getDirection");
      return dir;
   }
   public void setDirection(String _dir)
   {
      Debug.Log("It is GenalizeNote | setDirection");
      dir = _dir;
   }
   
}
