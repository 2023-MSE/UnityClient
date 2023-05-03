using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{
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
        private Animator animator;
        public void Start()
        {
            _combatManager = FindObjectOfType<CombatManager>();
            //test
            attactMotion();
        }
        public void hitMotion()
        {
            if (patterns != null)
            {
                _combatManager.setQueue(patterns);
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
            Debug.Log("Monster dead");
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
    }

}