using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using UnityEngine;

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
            int RandGenarate = rand.Next(0, 4);
            GameObject t_note=null;
            if (RandGenarate==0)
            {
                t_note = Instantiate(GNote, tfNoteAppear.position, Quaternion.identity);
            }
            else if (RandGenarate == 1)
            {
                t_note = Instantiate(ANote_l, tfNoteAppear.position, Quaternion.identity);
            }
            else if (RandGenarate == 2)
            {
                t_note = Instantiate(ANote_r, tfNoteAppear.position, Quaternion.identity);
            }
            else if (RandGenarate == 3)
            {
                t_note = Instantiate(ANote_u, tfNoteAppear.position, Quaternion.identity);
            }
            else if (RandGenarate == 4)
            {
                t_note = Instantiate(ANote_d, tfNoteAppear.position, Quaternion.identity);
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
            Destroy(other.gameObject);
            
            
        }
    }
}
