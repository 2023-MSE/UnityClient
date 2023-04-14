using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using MSEProject.Assets.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ParticleSystemJobs;
using Debug = UnityEngine.Debug;
using Object = System.Object;

[System.Serializable]
public class StringEvent : UnityEvent<string>
{
        
}
public class TimingManager : MonoBehaviour
{
    private PlayerController player;
    
    [SerializeField] Transform Center = null;

    [SerializeField] RectTransform[] timingRect = null;

    [SerializeField] private GameObject RedCenter;

    [SerializeField] private UnityEvent _successAttackUnityEvent;

    [SerializeField] private IntEvent _failAttackUnityEvent;

    [SerializeField] private UnityEvent _failGenalizeUnityEvent;

    [SerializeField] private DirEvent _successGenalizeUnityEvent;

    Vector2[] timingBoxs = null;

    // Start is called before the first frame update
    public List<GameObject> boxNoteList = new List<GameObject>();
    
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
    
    public void CheckTiming_dir(Direction dir)
    {
        if (dir == Direction.NONE)
            return;

        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;
            GameObject note = boxNoteList[i].gameObject;

            for (int x = 0; x < timingBoxs.Length; x++) // Pefect -> Cool -> Good -> Bad 순으로 판별하게됨. -> 이건 차후에!
            {
                // 각 판정 범위의 최소값 x, 최대값 y 를 비교하게됨.
                if (timingBoxs[0].x <= t_notePosX && t_notePosX <= timingBoxs[0].y)
                { 
                    if (note.gameObject.CompareTag("ANote")) // 공격 노트이면!
                    {
                      
                        if (dir==note.GetComponent<AttackNote>().getDirection())// 입력값과 노트의 dir 값이 같으면!
                        {
                            Debug.Log("공격 노트 방어 성공입니다");
                            _successAttackUnityEvent.Invoke();
                        }
                        else if (dir != note.GetComponent<AttackNote>().getDirection())
                        {
                            Debug.Log("공격 노트 방어 실패입니다");
                            _failAttackUnityEvent.Invoke(note.GetComponent<AttackNote>().getdamage());
                        }
                            
                    }
                    else if (note.gameObject.CompareTag("GNote")) // 일반 노트이면!
                    {
                      
                        note.GetComponent<GenalizeNote>().setDirection(dir);
                        _successGenalizeUnityEvent.Invoke(dir);
                    }
                    Debug.Log("note Success");



                    Destroy(boxNoteList[i].gameObject);
                   
                    RemoveNote(boxNoteList[i]);
                    
                    
                    
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

