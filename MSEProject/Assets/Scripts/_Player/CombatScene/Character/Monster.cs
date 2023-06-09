using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Player.CombatScene
{
    public static class ArrayExtensions
    {
        // 배열을 랜덤으로 섞는 Fisher-Yates 알고리즘
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    public class Monster : Character
    {
        private int num;

        private AttackNote _attackNote;
        private GenalizeNote _gNote;
        [SerializeField]
        private bool[] pattern = new bool[4];

        public GameObject[] patterns = new GameObject[4];
        private GameObject[] _patterns = new GameObject[4];
        [SerializeField]
        private int power = 10;
        [SerializeField]
        private int type;

        private CombatManager _combatManager;
        private ScoreManager _scoreManager;
        protected Animator animator;
        protected bool deadcheck = false;
        public void Start()
        {
            _combatManager = FindObjectOfType<CombatManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();

            _patterns = _combatManager.getNotes;

            
            for (int i = 0; i < 4; i++)
            {
                bool randomBool = (Random.value > 0.65f);
                pattern[i] = randomBool;
              
            }
            
            
           for (int i = 0; i < 4; i++)
            {
                if (pattern[i]) // 1:true -> 
                {
                    int aa = (int)Random.Range(0, 4);
                    patterns[i] = _patterns[aa];
                }
                else // 0:false -> 
                {
                    patterns[i] = _combatManager.getGNote;
                }
            }
           attactMotion();
        }
        

        public override void AnimateHitMotion()
        {
            animator.SetTrigger("isGetDamage");
        }
        public void AnimateAttack()
        {
            animator.SetTrigger("attack");
        }

        public void AnimateDie()
        {
            animator.SetTrigger("isDie");
        }

        public void AnimateWin()
        {
            animator.SetTrigger("isWin");
        }

        public override void dead()
        {
          
            /*TODO*/
            //_scoreManager.scoreUpdate(2,check);
            Debug.Log("Monster dead");
            AnimateDie();
            deadcheck = true;

        }

        
        public void attactMotion()
        { 
            List<GameObject> list =new List<GameObject>();
            
            if (patterns != null)
            {
                foreach (var obj in patterns)
                {
                    Debug.Log("--check-- num" + num);
                    if (obj.TryGetComponent(out _attackNote))
                    {
                        _attackNote.SetMonsterIndex(num);
                        list.Add(_attackNote.gameObject);
                    }
                    else if(obj.TryGetComponent(out _gNote))
                    {
                        _gNote.SetMonsterIndex(num);
                        list.Add(_gNote.gameObject);
                    }
                }
                _combatManager.setQueue(list);
                
            }
            
        }

        public void setPower(int power)
        {
            this.power = power;
        }

        public void AnimateIdle()
        {
            animator = this.GetComponent<Animator>();
        }
        public int getPower()
        {
            return power;
        }

        public int getType()
        {
            return power;
        }

        public void MonsterAttack()
        {
            DungeonManager.Instance.MonsterAttack();
        }

        public bool isDead()
        {
            return deadcheck;
        }

        public void setNum(int _num)
        {
            Debug.Log("MONSTER NUM"+_num);
            num = _num;
        }

        public int getNum()
        {
            return num;
        }
    }

}