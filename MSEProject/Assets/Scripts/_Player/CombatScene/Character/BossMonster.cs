using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{
    public class BossMonster : Monster
    {
        [SerializeField] private int bossAttackPower;
        [SerializeField] private GameObject effect;
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

        public void attackeffect()
        {
            StartCoroutine(monsterEffect(1f));
        }
        
        IEnumerator monsterEffect(float wait)
        {
            GameObject d = Instantiate(effect, gameObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(wait);
            
            Destroy(d);
        }
    }
}
