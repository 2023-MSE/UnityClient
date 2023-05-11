using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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


    private bool getAxisInUse = false;
    
    // Update is called once per frame
    void Update()
    {
        float inputHor = Input.GetAxisRaw("Horizontal");
        float inputVer = Input.GetAxisRaw("Vertical");
        Direction dir = Direction.NONE;


        if (!getAxisInUse)
        {
            if (inputHor != 0)
            {
                Debug.Log("change getAxisInUse to True");
                Debug.Log("change getAxisInUse to True" + gameObject.name);
                getAxisInUse = true;

                dir = inputHor > 0 ? Direction.RIGHT : Direction.LEFT;
                theTimingManager.CheckTiming_dir(dir);
            }
            else if (inputVer != 0)
            {
                Debug.Log("change getAxisInUse to True");
                Debug.Log("change getAxisInUse to True" + gameObject.name);
                getAxisInUse = true;
                
                dir = inputVer > 0 ? Direction.UP : Direction.DOWN;
                theTimingManager.CheckTiming_dir(dir);
            }
        }

        if (getAxisInUse == true && inputHor == 0 && inputVer == 0)
        {
            Debug.Log("change getAxisInUse to False");
            getAxisInUse = false;
        }

    }
}
