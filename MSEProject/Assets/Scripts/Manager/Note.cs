using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 120;
    public Direction dir;
    public int Damage;
   
    public int getdamage()
    {
        return 0;
    }
 

    public virtual Direction GetDirection()
    {
        return dir;
    }

    public virtual Direction sendDirection(Direction _dir)
    {
        return _dir;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
        
        
    }
}
