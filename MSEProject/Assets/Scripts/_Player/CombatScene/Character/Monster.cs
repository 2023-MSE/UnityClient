using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{
    public class Monster : Character
    {
        [SerializeField]
        private bool[] pattern = new bool[4];
        [SerializeField]
        private int power = 10;
        [SerializeField]
        private int type;
        public override void hitMotion()
        {
            /*TODO*/
        }
        public override void dead()
        {
            /*TODO*/
            Debug.Log("Monster dead");
            this.GetComponent<GameObject>().SetActive(false);
        }

        public void attactMotion()
        {
            /*TODO*/
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