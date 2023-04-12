using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class AttackNote_L : MonoBehaviour
{
    // Start is called before the first frame update
    private Direction dir=Direction.LEFT;
    
    public int damage=3;
   

  
    public String getdir()
    {
        return "a";
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
