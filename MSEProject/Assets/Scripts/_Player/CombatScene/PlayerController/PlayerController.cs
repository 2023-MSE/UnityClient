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

   
    // Update is called once per frame
    void Update()
    {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");
        Direction dir = Direction.NONE;

        if (inputHor != 0)
        {
            dir = inputHor > 0 ? Direction.LEFT : Direction.RIGHT;
        }
        else if (inputVer != 0)
        {
            dir = inputHor > 0 ? Direction.UP : Direction.DOWN;
        }
        Debug.Log("입력된 방향은 " + dir.ToString());
        theTimingManager.CheckTiming_dir(dir);
    }
}
