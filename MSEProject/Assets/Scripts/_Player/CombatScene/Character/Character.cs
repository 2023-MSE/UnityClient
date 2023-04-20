using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected float maxHp = 1000;
        [SerializeField]
        protected float hp = 0;

        public abstract void hitMotion();
        public abstract void dead();
        public float getHp()
        {
            return hp;
        }
        public void setHp(float value)
        {
            Debug.Log("Hp is " + hp + "value is" + value);
            hp = (hp + value >= maxHp ? maxHp : hp + value);
            Debug.Log("Hp is " + hp);
            if (hp <= 0)
                dead();
        }
    }
}
