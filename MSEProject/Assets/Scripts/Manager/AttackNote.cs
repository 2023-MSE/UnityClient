using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;


public class AttackNote : MonoBehaviour
{
    // 다양한 방향키가 존재하야함

    private Direction dir;
    
    public int damage;
    public void Start()
    {
        Random random = new Random();

        dir=(Direction)random.Next(Enum.GetNames(typeof(Direction)).Length);
     
        damage = 5;
    }

    public Direction getdir()
    {
        return dir;
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
