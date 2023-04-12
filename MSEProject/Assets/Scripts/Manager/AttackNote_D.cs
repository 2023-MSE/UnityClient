using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class AttackNote_D : MonoBehaviour
{
    // Start is called before the first frame update
    private Direction dir=Direction.DOWN;
    
    public int damage=1;
  

    public String getdir()
    {
        return "s";
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
