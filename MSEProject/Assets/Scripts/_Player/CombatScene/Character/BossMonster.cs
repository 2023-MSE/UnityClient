using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{
    public class BossMonster : Monster
    {
        [SerializeField] private int bossAttackPower;

        public void AnimateBossAttack()
        {
            animator.SetTrigger("bossAttack");
        }
        
        public int GetBossPower()
        {
            return bossAttackPower;
        }
        
        /**
         * TODO
         * 보스 공격시 이펙트 추가하기 
         */
    }
}
