using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TimingManager theTimingManager;

    private int Hp_Num=0;
    
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    public int getHP()
    {
        //Debug.Log(Hp_Num);
        return Hp_Num;
    }

    public void setHP(int hp)
    {
        
        this.Hp_Num = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            theTimingManager.CheckTiming_dir(Input.inputString);
        }
        
    }
}
