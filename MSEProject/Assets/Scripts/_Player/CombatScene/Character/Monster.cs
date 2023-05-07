using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using Random = System.Random;

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
        [SerializeField] private int num = 0;
        
        [SerializeField]
        private bool[] pattern = new bool[4];
        
        [SerializeField]
        private GameObject[] patterns = new GameObject[4];
        
        [SerializeField]
        private int power = 10;
        [SerializeField]
        private int type;

        private CombatManager _combatManager;
        public void Start()
        {
            _combatManager = FindObjectOfType<CombatManager>();

        }

        public void Update()
        {

            hitMotion();

            if (hp == 0)
            {
                dead();
            }
        }

        public override void hitMotion()
        {
            /*TODO*/
            if (patterns != null)

            patterns.Shuffle();

            if (!isDead())

            {
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
            
            // monster change
            Debug.Log("Monster dead");

            this.GetComponent<GameObject>().SetActive(false);

            AnimateDie();
        }

     
       
        public void attactMotion()
        {
            if (patterns != null)
            {
             
               _combatManager.setQueue(patterns);
                
            }

            
       
            
        }
        
        


        public void setPower(int power)
        {
            this.power = power;
        }
        public int getPower()
        {
            return power;
        }

        public int getType()
        {
            return power;
        }
    }

}