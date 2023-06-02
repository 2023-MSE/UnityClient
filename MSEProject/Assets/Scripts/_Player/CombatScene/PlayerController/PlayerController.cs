using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using _Player.CombatScene;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TimingManager theTimingManager;
    private RelaxManager theRelaxManager;

    private int Hp_Num=0;
    private Direction[][] directions; // hor, ver
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        theTimingManager = FindObjectOfType<TimingManager>();
        theRelaxManager = FindObjectOfType<RelaxManager>();
        
        directions = new Direction[3][];
        directions[0] = new Direction[3];
        directions[1] = new Direction[3];
        directions[2] = new Direction[3];

        directions[0][0] = directions[0][2] = directions[1][1] = directions[2][0] = directions[2][2] = Direction.NONE;
        directions[0][1] = Direction.LEFT;
        directions[2][1] = Direction.RIGHT;
        directions[1][0] = Direction.DOWN;
        directions[1][2] = Direction.UP;
    }


    private bool getAxisInUse = false;

    
    [MethodImpl(MethodImplOptions.Synchronized)]
    // Update is called once per frame
    void Update()
    {
        int inputHor = (int)Input.GetAxisRaw("Horizontal");
        int inputVer = (int)Input.GetAxisRaw("Vertical");
        if (!getAxisInUse)
        {
            Debug.Log("change getAxisInUse to True");
            Direction dir = directions[++inputHor][++inputVer];
            Debug.Log("dir is " + dir);
            if (dir != Direction.NONE)
            {
                getAxisInUse = true;
                //theTimingManager.CheckTiming_dir(dir);
                theTimingManager.CheckTiming_dir(dir);
            }
        }
        else
        {
            if (inputHor == 0 && inputVer == 0)
            {
                
                getAxisInUse = false;
            }
        }
    }
}
