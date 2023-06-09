﻿using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;


namespace _Player.CombatScene
{
    public class BuffManager : MonoBehaviour
    {
        private float bpm = 30f;
        private double currentTime = 0d;
        
        private Player _player;

        [SerializeField] private Transform tfNoteAppear = null;
        
        public GameObject up;
        
        public GameObject down;

        public GameObject next;
        
        private Boolean check = false;

        private bool isBuffManagerReady = false;
        
        private GameObject player;
        
        private CombatManager _combatManager;
        
        private TimingManager theTimingManager;
        
        private CoolDown _coolDown;

        private FadeEffect _fadeEffect;

        private BuffMapList _buffMapList;
        
      
        private void Start()
        {
            theTimingManager = FindObjectOfType<TimingManager>();
            _combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
            _buffMapList = FindObjectOfType<BuffMapList>();
        }

        public bool ShowBuffNext()
        {
            return _buffMapList.check_note();
        }

        public void BuffManagerSetFalse()
        {
            isBuffManagerReady = false;
        }

        public void ShowNextStage()
        {
            _buffMapList.clickmap();
        }

        private void FixedUpdate()
        {
            
            currentTime += Time.deltaTime; 
            if (!isBuffManagerReady)
            {
                Debug.Log("Stage is Not Ready");
                return;
            }
            if (currentTime >= 60d / bpm) // 60s / bpm = 비트 한개당 등장 속도 : 1초에 1개씩 노트가 생성.. 120s / bpm : 0.5초에 1개씩 노트가 생성
            {
                int aa = (int)UnityEngine.Random.Range(0, 3);
                
                GameObject note =null;
                if (aa == 0)
                {
                    Debug.Log("speed up");
                    note = up;
                }
                else if (aa == 1)
                {
                    Debug.Log("speed down");
                    note = down;
                }
                else if (aa == 2)
                {
                    note = next;
                }
               
                //t_note.transform.position = tfNoteAppear.position;

                if (note != null)
                {
                    note = Instantiate(note, tfNoteAppear.position, quaternion.identity);
                    //새로 생성된 t_note의 부모를 Canvas 안의 위치로 지정해줘야함!
                    // note.gameObject.transform.SetParent(this.transform);
                    note.gameObject.transform.SetParent(GameObject.Find("Note").transform);
                }
               

                // TimingManager에 t_note 바로 생성된 노트를 보냄
                if (note != null)
                {
                    theTimingManager.AddNote(note);
                    
                }
                else
                {
                    Debug.Log("empty");
                }
                currentTime -= 60d / bpm; // 오차로 인해 .

            }
        }

        public void Scenecheck_Buff()
        {
            _fadeEffect = FindObjectOfType<FadeEffect>();
            check = !check;
            isBuffManagerReady = true;
            player = GameObject.FindWithTag("Player");
            _coolDown = GameObject.Find("CoolDown").GetComponent<CoolDown>();
            player.GetComponent<Player>().setHp(DungeonManager.Instance.GetPlayerHP());
            _coolDown.setHp(DungeonManager.Instance.GetPlayerHP()*0.001f);

        }
    }
}