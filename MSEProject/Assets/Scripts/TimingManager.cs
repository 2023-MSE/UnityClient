using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using DefaultNamespace;
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

            for (int x = 0; x < timingBoxs.Length; x++) // Pefect -> Cool -> Good -> Bad 순으로 판별하게됨.
            {
                
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                { // 각 판정 범위의 최소값 x, 최대값 y 를 비교하게됨.

                    
            
                    // 성공시
                    if (boxNoteList[i].CompareTag("ANote"))
                    {
                        Debug.Log("anote");
                        /

                        AttackNote note = boxNoteList[i].GetComponent<AttackNote>;
                        
                        // note 안에 String 과 파라메터 dir이 같으면 -> monster attack note 성공
                         if (note.getdir()==dir)
                         {
                             //monster attack note :
                         }
                         else // note 안에 String 과 파라메터 dir이 같지 않으면 -> note 성공 못함
                         {
                             //monster attack note : fail
                             player.setHP(note.getdamage());
                         }
                         
                        
                        
                    }
                    else if(boxNoteList[i].CompareTag("GNote"))
                    {
                        Debug.Log("gnote");
                        Direction direction = SendGenalizeData(dir);
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
        
        Debug.Log("miss!");
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

    public Direction SendGenalizeData(String s)
    {
        Direction dir=0;
        switch (s)
        {
            case "a":
                dir= Direction.Left;
                break;
            case "s":
                dir= Direction.Down;
                break;
            case "d":
                dir=Direction.Right;
                break;
            case "w":
                dir= Direction.Up;
                break;
            default:
                MissNote();
                break;

        }

        return dir;
    }
    public void MissNote()
    {
        // 범위 안에 miss -> hp 가 1씩 깍이게 
       
        player.setHP(num--);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
