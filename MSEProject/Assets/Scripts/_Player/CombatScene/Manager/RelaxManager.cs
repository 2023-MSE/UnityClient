﻿using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace _Player.CombatScene
{
    public class RelaxManager : MonoBehaviour
    {
        private float bpm = 30f;
        private double currentTime = 0d;
        
        private Player _player;

        [SerializeField] private Transform tfNoteAppear = null;
        
        private Boolean check = false;

        private CombatManager _combatManager;
        private bool isRelaxManagerReady = false;
        
        private GameObject player;

        public GameObject rnote;
        
        public GameObject nextnote;

        [SerializeField] private GameObject card;
        [SerializeField] private GameObject note;

        [SerializeField] private GameObject heal;

        [SerializeField] private GameObject dead;

        // check cool down
        private CoolDown _coolDown;

        private CameraShake _shake;
        private TimingManager theTimingManager;

        private RelaxMapList _relaxMapList;

        private Direction _dir;
        
        public float minDamage = 100; // 최소 데미지
        public float maxDamage = 200; // 최대 데미지
        public float minHeal = 50; // 최소 회복량
        public float maxHeal = 100; // 최대 회복량


        private FadeEffect _fadeEffect;

        private GameObject[] list;
        
        private GameObject[] list2;
        private void Start()
        {
 
            card.SetActive(false);

            heal.SetActive(false);
            dead.SetActive(false);
            theTimingManager = FindObjectOfType<TimingManager>();
            _fadeEffect = FindObjectOfType<FadeEffect>();
            _relaxMapList = FindObjectOfType<RelaxMapList>();
            player = GameObject.FindWithTag("Player");
            Debug.Log("start relax Scene");
            _combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
            _shake = FindObjectOfType<CameraShake>();




        }

     

        public bool RelaxManagerReady()
        {
           
            return isRelaxManagerReady;
        }

        public bool ShowRelaxNext()
        {
            return _relaxMapList.check_note();
        }

        private int justone = 0;
        
        void FixedUpdate()
        {

           

            currentTime += Time.deltaTime; //1초에 1씩 증가되게
            if (!isRelaxManagerReady)
            {
                Debug.Log("Stage is Not Ready");
                return;
            }

            if (currentTime >= 60d / bpm) // 60s / bpm = 비트 한개당 등장 속도 : 1초에 1개씩 노트가 생성.. 120s / bpm : 0.5초에 1개씩 노트가 생성
            {
                int aa = (int)Random.Range(0, 2);
                GameObject note =null;
                if (aa == 0)
                {
                    Debug.Log("relax");
                    note = rnote;
                }
                else if (aa == 1)
                {
                    Debug.Log("next");
                    note = nextnote;
                }
               
                //t_note.transform.position = tfNoteAppear.position;

                if (note != null&&!(_relaxMapList.check_note()))
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

        public void DrinkToTem(CoolDown cooldown, float hp)
        {
            cooldown.RelaxHp(hp*0.001f);
            player.GetComponent<Player>().setHp(+300f);
         
        }
        

        public void CheckRelax_dir(Direction direction)
        {
            _dir = direction;
        }

        public Direction getDirection()
        {
            return _dir;
        }

        public void RelaxSceneOff()
        {
            isRelaxManagerReady = false;
        }

        public void Scenecheck()
        {
            check = !check;
            isRelaxManagerReady = true;
            player = GameObject.FindWithTag("Player");
            _coolDown = GameObject.Find("CoolDown").GetComponent<CoolDown>();
            _player.setHp(DungeonManager.Instance.GetPlayerHP());
            _coolDown.setHp(DungeonManager.Instance.GetPlayerHP()*0.001f);

            //note.SetActive(false);



        }
        
        

        public void ShowNextStage()
        {
           _relaxMapList.clickmap();
        }
        
        public void ApplyRandomEffect()
        {
            card.SetActive(true);
            bool shouldDamage = Random.Range(0, 2) == 0; // 0 또는 1 중에서 랜덤으로 선택
            
            Debug.Log("relax : " +  shouldDamage);
            if (shouldDamage)
            {
                //
                dead.SetActive(true);
                heal.SetActive(false);
                player.GetComponent<Player>().AnimateHitMotion();
                float damageAmount = (int)Random.Range(minDamage, maxDamage + 1);
                Debug.Log("relax damage : " +  damageAmount);
                player.GetComponent<Player>().setHp(-damageAmount);
                _coolDown.setHp(player.GetComponent<Player>().getHp()*0.001f);
                _shake.ShakeCamera();
                if (player.GetComponent<Player>().setHp(0))
                {
                    _fadeEffect.gameover();
                }
               
            }
            else
            {
                //
                heal.SetActive(true);
                dead.SetActive(false);
                player.GetComponent<Player>().AnimateIsDrink();
                float healAmount = Random.Range(minHeal, maxHeal + 1);
                Debug.Log("relax heal: " +  healAmount);
               
                player.GetComponent<Player>().setHp(healAmount);
                _coolDown.setHp(player.GetComponent<Player>().getHp()*0.001f);
            }
        }
      
    }
}