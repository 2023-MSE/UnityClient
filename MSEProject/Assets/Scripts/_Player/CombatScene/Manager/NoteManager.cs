using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using _Player.CombatScene;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using Debug = UnityEngine.Debug;
using Random = System.Random;

public class NoteManager : MonoBehaviour
{

    // bpm 이란. bit per minute : 1분당 비트의 수  ex) 120bpm 이면 1분당 120개의 노트가 생성된다는 의미
    
    public int bpm = 0;
    private double currentTime = 0d;

    [SerializeField] private Transform tfNoteAppear = null;
    [SerializeField] private GameObject GNote = null;
    [SerializeField] private GameObject ANote_l = null;
    [SerializeField] private GameObject ANote_r = null;
    [SerializeField] private GameObject ANote_u= null;
    [SerializeField] private GameObject ANote_d = null;
    
   // [SerializeField] private GameObject goNote2 = null;
    public TimingManager theTimingManager;

    private CombatManager theCombatManager;
    public List<GameObject> Notes = new List<GameObject>();

    public static ObjectPool<GameObject> Instances;
    
    private Random rand = new Random();

    private GameObject t_note = null;
    
    private int num;

    private void Start()
    {
        //TimingManager 스크립트를 가지고 있는 오브젝트를 반환한다.
        theTimingManager = FindObjectOfType<TimingManager>();
        theCombatManager = FindObjectOfType<CombatManager>();
        num = Notes.Count;
     
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        
        currentTime += Time.deltaTime; //1초에 1씩 증가되게

        if (currentTime >= 60d / bpm) // 60s / bpm = 비트 한개당 등장 속도 : 1초에 1개씩 노트가 생성.. 120s / bpm : 0.5초에 1개씩 노트가 생성
        {
            
            GameObject note = theCombatManager.GetNote();
            //t_note.transform.position = tfNoteAppear.position;

            if (note != null)
            {
                t_note = Instantiate(note, tfNoteAppear.position, quaternion.identity);
            }
            else
            {
                Debug.Log("note is null");
            }

            //새로 생성된 t_note의 부모를 Canvas 안의 위치로 지정해줘야함!
            t_note.gameObject.transform.SetParent(this.transform);

            // TimingManager에 t_note 바로 생성된 노트를 보냄
            if (t_note != null)
             {
                 theTimingManager.AddNote(t_note);
             }
             else
             {
                 Debug.Log("empty");
             }
            currentTime -= 60d / bpm; // 오차로 인해 . 
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note")||other.CompareTag("GNote")||other.CompareTag("ANote"))
        {
           
            theTimingManager.RemoveNote(other.gameObject);

            
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
