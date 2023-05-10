using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        [SerializeField]
        private bool[] pattern = new bool[4];
        
        [SerializeField]
        private GameObject[] patterns = new GameObject[4];
        
        [SerializeField]
        private int power = 10;
        [SerializeField]
        private int type;

        private CombatManager _combatManager;
        private ScoreManager _scoreManager;
        private Animator animator;
        public void Start()
        {
            _combatManager = FindObjectOfType<CombatManager>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            //test

        }
        public void hitMotion()
        {
            if (patterns != null)
            {
                _combatManager.setQueue(patterns);
            }
        }

        public void Update()
        {
            patterns.Shuffle();

            if (!isDead())
          
            {
                _combatManager.setQueue(patterns);
                attactMotion();
            }
        }

        public override void AnimateHitMotion()
        {
            /*TODO*/
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

        public void AnimateBossAttack()
        {
            animator.SetTrigger("bossAttack");
        }
        public override void dead()
        {
          
            /*TODO*/
            //_scoreManager.scoreUpdate(2,check);
            Debug.Log("Monster dead");
            AnimateDie();
           
        }
       
        public void attactMotion()
        {
            if (patterns != null)
            {
                foreach (var obj in patterns)
                {
                    if (obj.TryGetComponent(out _attackNote))
                    {
                        _attackNote.SetMonsterIndex(num);
                    }
                }
               _combatManager.setQueue(patterns);
                
            }
            
        }

        public void setPower(int power)
        {
            this.power = power;
        }

        public void AnimateIdle(float speed)
        {
            animator = this.GetComponent<Animator>();
            animator.SetFloat("speed", speed);
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
            DungeonManager.instance.MonsterAttack();
        }

        public bool isDead()
        {
            return hp <= 0;
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