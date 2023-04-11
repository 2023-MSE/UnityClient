using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class AttackNote_U : MonoBehaviour
{
    private Direction dir=Direction.Up;
    
    public int damage=5;
    
 
    public String getdir()
    {
        return "w";
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
