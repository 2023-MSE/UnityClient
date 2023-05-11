using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using _Player.CombatScene;
using MSEProject.Assets.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ParticleSystemJobs;
using Debug = UnityEngine.Debug;
using Object = System.Object;
using Random = UnityEngine.Random;


[System.Serializable]
public class StringEvent : UnityEvent<string>
{
        
}
public class TimingManager : MonoBehaviour
{
    private PlayerController player;

    private Player _player;
    
    private CoolDown _coolDown;

    private AttackNote _attackNote;
    
    [SerializeField] Transform Center = null;

    [SerializeField] RectTransform[] timingRect = null;

    [SerializeField] private GameObject RedCenter;

    [SerializeField] private IntEvent _successAttackUnityEvent;

    [SerializeField] private IntEvent _failAttackUnityEvent;

    [SerializeField] private UnityEvent _failGenalizeUnityEvent;

    [SerializeField] private DirEvent _successGenalizeUnityEvent;

    Vector2[] timingBoxs = null;
    

    // Start is called before the first frame update
    public List<GameObject> boxNoteList = new List<GameObject>();
    
    private int num;

    private CombatManager _combatManager;

    private void Awake()
    {
        RedCenter.SetActive(false);
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        _coolDown = GameObject.Find("CoolDown").GetComponent<CoolDown>();
        
        timingBoxs = new Vector2[timingRect.Length];
        _combatManager = FindObjectOfType<CombatManager>();

        for (int i = 0; i < timingRect.Length; i++)
        {
            // 중심 점을 기준으로 박스의 너비에 맞게! 판정이 됨
            timingBoxs[i].Set(Center.localPosition.x-timingRect[i].rect.width/2,Center.localPosition.x+timingRect[i].rect.width/2);
            Debug.Log(timingBoxs[i].x+" "+timingBoxs[i].y);
        }
        
        //_coolDown.DamageToFull();
    }

    public void AddNote(GameObject note)
    {
        boxNoteList.Add(note);
    }
    
    
    
    public void RemoveNote(GameObject note)
    {
       boxNoteList.Remove(note);
      // _combatManager.getQueue().Enqueue(note);
    }
    
    public void CheckTiming_dir(Direction dir)
    {
        
        // 어느 monster의 note인지 알아야함
        
        
        if (dir == Direction.NONE)
            return;

        for (int i = 0; i < boxNoteList.Count; i++)
        {
       
            float t_notePosX = boxNoteList[i].transform.localPosition.x;
            GameObject note = boxNoteList[i].gameObject;
            
                // 각 판정 범위의 최소값 x, 최대값 y 를 비교하게됨.
                if (timingBoxs[0].x <= t_notePosX && t_notePosX <= timingBoxs[0].y)
                { 
                    if (note.gameObject.CompareTag("ANote")) // 공격 노트이면!
                    {
                      
                        if (dir==note.GetComponent<AttackNote>().getDirection())// 입력값과 노트의 dir 값이 같으면!
                        {
                            Debug.Log("공격 노트 방어 성공입니다");
                            _successAttackUnityEvent.Invoke(note.GetComponent<AttackNote>().GetMonsterIndex());
                            
                            // _combatManager.monsterAttackDefence(note.GetComponent<AttackNote>().GetMonsterIndex());
                          
                            boxNoteList.Remove(note);
                            Destroy(note);

                        }
                        else if (dir != note.GetComponent<AttackNote>().getDirection())
                        {
                            Debug.Log("공격 노트 방어 실패입니다");

                            _failAttackUnityEvent.Invoke(note.GetComponent<AttackNote>().GetMonsterIndex());
                             
                             //error ? 왜?
                             Debug.Log("note index : " + note.GetComponent<AttackNote>().GetMonsterIndex());
                             //note.GetComponent<AttackNote>().GetMonsterIndex()
                             //_combatManager.monsterAttack(note.GetComponent<AttackNote>().GetMonsterIndex());
                             //_combatManager.MonsterAttackPlayer();
                        }

                    }
                    else if (note.gameObject.CompareTag("GNote")) // 일반 노트이면!
                    {
                        note.GetComponent<GenalizeNote>().setDirection(dir);
                        
                        
                        _successGenalizeUnityEvent.Invoke(note.GetComponent<GenalizeNote>().getDirection());
                        boxNoteList.Remove(note);
                        Destroy(note);
                    } 
                } 
                
                // else if (note.TryGetComponent(out _attackNote))
                // {
                //     _failAttackUnityEvent.Invoke(_attackNote.GetComponent<AttackNote>().GetMonsterIndex());
                //
                // }

        }
            
            
           
        
     
    }

    IEnumerator EnQueue(GameObject obj)
    {
        _combatManager.getQueue().Enqueue(obj);
        yield break;
    }

    IEnumerator changeColor()
    {
        RedCenter.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedCenter.SetActive(false);
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


    // Update is called once per frame
    void Update()
    {
        
    }
}

