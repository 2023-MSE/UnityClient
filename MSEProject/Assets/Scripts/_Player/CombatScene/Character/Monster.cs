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
        public void Start()
        {
            _combatManager = FindObjectOfType<CombatManager>();
            //test
            
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
            {
                _combatManager.setQueue(patterns);
            }
        }
        public override void dead()
        {
            /*TODO*/
            
            // monster change
            Debug.Log("Monster dead");
                this.GetComponent<GameObject>().SetActive(false);
            
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