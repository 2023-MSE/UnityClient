using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using DefaultNamespace;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Random = System.Random;

public class NoteManager : MonoBehaviour
{

    // bpm 이란. bit per minute : 1분당 비트의 수  ex) 120bpm 이면 1분당 120개의 노트가 생성된다는 의미
    
    public int bpm = 0;
    private double currentTime = 0d;

    [SerializeField] private Transform tfNoteAppear = null;
    [SerializeField] private GameObject goNote1 = null;
    [SerializeField] private GameObject goNote2 = null;
    
   // [SerializeField] private GameObject goNote2 = null;
    public TimingManager theTimingManager;

    public List<GameObject> Notes = new List<GameObject>();

    private Random rand = new Random();
    
    private int num;
    private void Start()
    {
        //TimingManager 스크립트를 가지고 있는 오브젝트를 반환한다.
        theTimingManager = FindObjectOfType<TimingManager>();
        num = Notes.Count;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        currentTime += Time.deltaTime; //1초에 1씩 증가되게

        if (currentTime >= 60d / bpm) // 60s / bpm = 비트 한개당 등장 속도 : 1초에 1개씩 노트가 생성.. 120s / bpm : 0.5초에 1개씩 노트가 생성
        {
            int RandGenarate = rand.Next(0, 2);
            GameObject t_note=null;
            if (RandGenarate==0)
            {
                t_note = Instantiate(goNote1, tfNoteAppear.position, Quaternion.identity);
            }
            else if (RandGenarate == 1)
            {
                t_note = Instantiate(goNote2, tfNoteAppear.position, Quaternion.identity);
            }

            
            
            Debug.Log(t_note.gameObject.name); // GeanlizeNote(Clone)으로 출력

            t_note.gameObject.transform.SetParent(this.transform);
            
            
            //새로 생성된 t_note의 부모를 Canvas 안의 위치로 지정해줘야함!

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
            Destroy(other.gameObject);
            
            
        }
    }
}
