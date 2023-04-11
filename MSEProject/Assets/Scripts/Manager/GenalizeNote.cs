using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;
using Direction = DefaultNamespace.Direction;

public class GenalizeNote: MonoBehaviour
{
   public String dir;

   public void setDir(String _dir)
   {
      dir = _dir;
   }

   public String getDir()
   {
      return dir;
   }
}
