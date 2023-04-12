using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = System.Object;

public class TimingManager : MonoBehaviour
{
    private PlayerController player;
    
    [SerializeField] Transform Center = null;

    [SerializeField] RectTransform[] timingRect = null;

    [SerializeField] private GameObject RedCenter;
    
    Vector2[] timingBoxs = null;

    private Direction Dir;
    // Start is called before the first frame update
    public List<GameObject> boxNoteList = new List<GameObject>();

    public List<int> dir_int = new List<int>();
    private int num;
    private String _dir;
    


    private void Awake()
    {
        RedCenter.SetActive(false);
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        

        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            // 중심 점을 기준으로 박스의 너비에 맞게! 판정이 됨
            timingBoxs[i].Set(Center.localPosition.x-timingRect[i].rect.width/2,Center.localPosition.x+timingRect[i].rect.width/2);
            Debug.Log(timingBoxs[i].x+" "+timingBoxs[i].y);
        }
    }

    public void AddNote(GameObject note)
    {
        boxNoteList.Add(note);


    }
    
    
    
    public void RemoveNote(GameObject note)
    {
        boxNoteList.Remove(note);
        
    }
    
    public void CheckTiming_dir(String dir)
    {

        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxs.Length; x++) // Pefect -> Cool -> Good -> Bad 순으로 판별하게됨. -> 이건 차후에!
            {
                // 각 판정 범위의 최소값 x, 최대값 y 를 비교하게됨.
                if (timingBoxs[0].x <= t_notePosX && t_notePosX <= timingBoxs[0].y)
                { 
                    Debug.Log(i);
                    // 인식 된 노트를 note 로 지정
                    GameObject note = boxNoteList[i].gameObject;
                    

                    // 성공시
                    if (note.CompareTag("ANote_L"))
                    {
                         _dir=note.GetComponent<AttackNote_L>().getdir();
              
                        // note 안에 String 과 파라메터 dir이 같으면 -> monster attack note 성공
                         if (_dir==dir)
                         {
                             //monster attack note : 방어 성공
                             SendSuccess_Attack();

                         }
                         else // note 안에 String 과 파라메터 dir이 같지 않으면 -> note 성공 못함
                         {
                             //monster attack note : 방어 실패
                             SendFail_Attack(note.GetComponent<AttackNote_L>().getdamage());
                             MissNote();
                         }
                         
                         
                        
                        
                    }
                    else if (note.CompareTag("ANote_D"))
                    {
                        _dir=note.GetComponent<AttackNote_D>().getdir();
                  
                        // note 안에 String 과 파라메터 dir이 같으면 -> monster attack note 성공
                        if (_dir=="s")
                        {
                            
                            //monster attack note : 방어 성공
                            SendSuccess_Attack();

                        }
                        else // note 안에 String 과 파라메터 dir이 같지 않으면 -> note 성공 못함
                        {
                            //monster attack note : 방어 실패
                            SendFail_Attack(note.GetComponent<AttackNote_D>().getdamage());
                            
                        }
                         
                    }
                    else if (note.CompareTag("ANote_R"))
                    {
                        _dir=note.GetComponent<AttackNote_R>().getdir();
                    
                        // note 안에 String 과 파라메터 dir이 같으면 -> monster attack note 성공
                        if (_dir=="d")
                        {
                            
                            //monster attack note : 방어 성공
                            SendSuccess_Attack();

                        }
                        else // note 안에 String 과 파라메터 dir이 같지 않으면 -> note 성공 못함
                        {
                            //monster attack note : 방어 실패
                            SendFail_Attack(note.GetComponent<AttackNote_R>().getdamage());
                            
                        }
                        
                        
                    }
                    else if (note.CompareTag("ANote_U"))
                    {
                        _dir=note.GetComponent<AttackNote_U>().getdir();
                        // note 안에 String 과 파라메터 dir이 같으면 -> monster attack note 성공
                        if (_dir=="w")
                        {
                            
                            //monster attack note : 방어 성공
                            SendSuccess_Attack();

                        }
                        else // note 안에 String 과 파라메터 dir이 같지 않으면 -> note 성공 못함
                        {
                            //monster attack note : 방어 실패
                            SendFail_Attack(note.GetComponent<AttackNote_U>().getdamage());
                            
                        }
                         
                         
                        
                        
                    }
                    else if(note.CompareTag("GNote"))
                    {
                        SendGenalizeData(dir);
                    }
                    
                    



                    Destroy(boxNoteList[i].gameObject);
                   
                    RemoveNote(boxNoteList[i]);
                    
                    
                    StartCoroutine(changeColor());
                    CheckNote(x);
                    return;
                }
            }
    
        }
        //실패시
        MissNote();
    }

    IEnumerator changeColor()
    {
        RedCenter.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedCenter.SetActive(false);
    }

    public void CheckNote(int x)
    {
        // 범위 값에 맞게 -> Perfect : 0 - Cool : 1 - Good : 2 - Bad : 3 순으로 스킬 up 가중치 주기 -> switch 문으로
        // 일단, Good 범위에만 들어오면 skill up 되게!

     
            player.setHP(num++);
            
        
    }

    public void SendSuccess_Attack()
    {
        Debug.Log("SendSuccess_Attack");
    }

    public int SendFail_Attack(int damage)
    {
        Debug.Log("SendFail_Attack"+damage);
        return damage;
    }

    public String SendGenalizeData(String s)
    {
        Direction dir=0;
        switch (s)
        {
            case "a":
                dir= Direction.LEFT;
                break;
            case "s":
                dir= Direction.DOWN;
                break;
            case "d":
                dir=Direction.RIGHT;
                break;
            case "w":
                dir= Direction.UP;
                break;
            default:
                MissNote();
                break;

        }

        return s;
    }
    public void MissNote()
    {
        // 범위 안에 miss -> hp 가 1씩 깍이게 
        Debug.Log("miss!");
        player.setHP(num--);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
