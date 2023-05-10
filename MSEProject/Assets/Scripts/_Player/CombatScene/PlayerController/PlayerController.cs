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
        float inputHor = Input.GetAxisRaw("Horizontal");
        float inputVer = Input.GetAxisRaw("Vertical");
        Direction dir = Direction.NONE;

        if (inputHor != 0)
        {
            dir = inputHor > 0 ? Direction.RIGHT : Direction.LEFT;
        }
        else if (inputVer != 0)
        {
            dir = inputVer > 0 ? Direction.UP : Direction.DOWN;
        }
        Debug.Log("입력된 방향은 " + dir.ToString());
        theTimingManager.CheckTiming_dir(dir);
    }
}
