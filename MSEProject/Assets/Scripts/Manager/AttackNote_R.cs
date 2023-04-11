using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class AttackNote_R : MonoBehaviour
{
    private Direction dir=Direction.Right;
    
    public int damage=4;
  

  
    public String getdir()
    {
        return "d";
    }

    public int getdamage()
    {
        return damage;
    }
    void Update()
    {
        transform.localPosition += Vector3.right * 120 * Time.deltaTime;
        
        
    }
}
