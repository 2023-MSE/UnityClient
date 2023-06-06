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
    public GameObject noteui;
    
    private PlayerController player;

    private Player _player;
    
    private CoolDown _coolDown;

    private AttackNote _attackNote;

    private TestDirButton test;
    
    private GenalizeNote _gnote;

    private FadeEffect _fadeEffect;

    private RelaxManager _relaxManager;

    private BuffManager _buffManager;
    
    [SerializeField] Transform Center = null;

    [SerializeField] RectTransform[] timingRect = null;

    [SerializeField] private GameObject RedCenter;

    [SerializeField] private IntEvent _successAttackUnityEvent;

    [SerializeField] private IntEvent _failAttackUnityEvent;

    [SerializeField] private IntEvent _failGenalizeUnityEvent;

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
        _buffManager = FindObjectOfType<BuffManager>();
    }

    void Start()
    {
        _fadeEffect = FindObjectOfType<FadeEffect>();
        test = FindObjectOfType<TestDirButton>();
        _coolDown = GameObject.Find("CoolDown").GetComponent<CoolDown>();
        _relaxManager = FindObjectOfType<RelaxManager>();
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

    private void FixedUpdate()
    {
         
        if (_relaxManager.ShowRelaxNext())
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("NextNote");
            foreach (var l in list)
            {
                Destroy(l);
            }
                        
            GameObject[] list2 = GameObject.FindGameObjectsWithTag("RelaxNote");
            foreach (var la in list2)
            {
                Destroy(la);
            }

               
            return;
        }
        else if (_buffManager.ShowBuffNext())
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("UpNote");
            foreach (var l in list)
            {
                Destroy(l);
            }
                        
            GameObject[] list2 = GameObject.FindGameObjectsWithTag("DownNote");
            foreach (var la in list2)
            {
                Destroy(la);
            }
            
            GameObject[] list3 = GameObject.FindGameObjectsWithTag("NextNoteBuff");
            foreach (var la in list3)
            {
                Destroy(la);
            }

               
            return;
        }
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

    private bool check = true;

    public void checking_timing()
    {
        check = !check;
    }

    public void CheckTiming_dir(Direction dir)
    {

        // 어느 monster의 note인지 알아야함
        if (check)
        {
           
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

                        if (dir == note.GetComponent<AttackNote>().getDirection()) // 입력값과 노트의 dir 값이 같으면!
                        {
                            Debug.Log("--check--:"+note.GetComponent<AttackNote>().GetMonsterIndex());
                            note.GetComponent<AttackNote>().noteCheckTrue();
                            _successAttackUnityEvent.Invoke(note.GetComponent<AttackNote>().GetMonsterIndex());

                            

                            boxNoteList.Remove(note);
                            Destroy(note.gameObject);

                        }
                        else if (dir != note.GetComponent<AttackNote>().getDirection())
                        {
                            Debug.Log("--check--:"+note.GetComponent<AttackNote>().GetMonsterIndex());
                            note.GetComponent<AttackNote>().noteCheckTrue();
                            _failAttackUnityEvent.Invoke(note.GetComponent<AttackNote>().GetMonsterIndex());

                            boxNoteList.Remove(note);
                            Destroy(note.gameObject);

                        }
                    }
                    else if (note.gameObject.CompareTag("GNote")) // 일반 노트이면!
                    {
                        Debug.Log("--check--:"+note.GetComponent<GenalizeNote>().GetMonsterIndex());
                        note.GetComponent<GenalizeNote>().setDirection(dir);
                        note.GetComponent<GenalizeNote>().noteCheckTrue();

                        _successGenalizeUnityEvent.Invoke(note.GetComponent<GenalizeNote>().getDirection());
                        boxNoteList.Remove(note);
                        Destroy(note.gameObject);
                    }
                    else if (note.gameObject.CompareTag("RelaxNote")) 
                    {

                        _relaxManager.ApplyRandomEffect();
                        boxNoteList.Remove(note);
                        Destroy(note.gameObject);
                    }
                    else if (note.gameObject.CompareTag("NextNote")) 
                    {

                        _relaxManager.ShowNextStage();
                        boxNoteList.Remove(note);
                        Destroy(note.gameObject);
                    }
                    else if (note.gameObject.CompareTag("UpNote")) 
                    {

                        _Player.CombatScene.DungeonManager.instance.SetspeedUpDown(1);
                        boxNoteList.Remove(note);
                        Destroy(note.gameObject);
                    }
                    else if (note.gameObject.CompareTag("DownNote")) 
                    {

                        _Player.CombatScene.DungeonManager.instance.SetspeedUpDown(0);
                        boxNoteList.Remove(note);
                        Destroy(note.gameObject);
                    }
                    else if (note.gameObject.CompareTag("NextNoteBuff")) 
                    {

                        _buffManager.ShowNextStage();
                        
                        boxNoteList.Remove(note);
                        Destroy(note.gameObject);
                    }



                }
            }
        }
        else // not in the range
            {
            /*    if (note.gameObject.CompareTag("ANote"))
                {
                    if (note.gameObject.TryGetComponent(out _attackNote))
                    {
                        Debug.Log("--check--:range a");
                        //_attackNote.GetComponent<AttackNote>().noteCheckTrue();
                        if (!_attackNote.GetComponent<AttackNote>().noteCheckIt()) _failAttackUnityEvent.Invoke(_attackNote.GetComponent<AttackNote>().GetMonsterIndex());
            
                    }
           
                }
                else if (note.gameObject.CompareTag("GNote"))
                {
                    if (note.gameObject.TryGetComponent(out _gnote))
                    {
                        Debug.Log("--check--:range g");
                        //_gnote.GetComponent<GenalizeNote>().noteCheckTrue();
                        if (!_gnote.GetComponent<GenalizeNote>().noteCheckIt()) _failGenalizeUnityEvent.Invoke(_gnote.GetComponent<GenalizeNote>().GetMonsterIndex());
            
                    }
                } 
                */
            }
            
            
            
        
    }

    //OUT OF THE UI
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("ANote"))
        {
            if (other.gameObject.TryGetComponent(out _attackNote))
            {
                if (_attackNote.GetComponent<AttackNote>().noteCheckIt() == false)
                {
                    _failAttackUnityEvent.Invoke(_attackNote.GetComponent<AttackNote>().GetMonsterIndex());
                }
            }
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
           
        }
        else if (other.CompareTag("GNote"))
        {
            if (other.gameObject.TryGetComponent(out _gnote))
            {
                if (_gnote.GetComponent<GenalizeNote>().noteCheckIt() == false)
                {
                    _failGenalizeUnityEvent.Invoke(_gnote.GetComponent<GenalizeNote>().GetMonsterIndex());
                }
            }
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        
        else if (other.CompareTag("RelaxNote"))
        {
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("NextNote"))
        {
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
            
        else if (other.CompareTag("UpNote"))
        {
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("DownNote"))
        {
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("NextNoteBuff"))
        {
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        
    }
    /*private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("ANote"))
        {
            if (other.gameObject.TryGetComponent(out _attackNote))
            {
                if (_attackNote.GetComponent<AttackNote>().noteCheckIt() == false)
                {
                    _failAttackUnityEvent.Invoke(_attackNote.GetComponent<AttackNote>().GetMonsterIndex());
                }
            }
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
           
        }
        else if (other.CompareTag("GNote"))
        {
            if (other.gameObject.TryGetComponent(out _gnote))
            {
                if (_gnote.GetComponent<GenalizeNote>().noteCheckIt() == false)
                {
                    _failGenalizeUnityEvent.Invoke(_gnote.GetComponent<GenalizeNote>().GetMonsterIndex());
                }
            }
            boxNoteList.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        
    }
    */

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

