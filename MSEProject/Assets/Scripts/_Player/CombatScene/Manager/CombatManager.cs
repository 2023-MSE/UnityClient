using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player.CombatScene
{

    public class CombatManager : MonoBehaviour
    {
        public const int MAX_HP = 9999999;
        [SerializeField]
        private SkillDataScriptableObject skillData;
        private int singleTargetIndex = 0;
        private Monster[] monsters;
        private GameObject player;
        private float attackMulti = 1.0f;
        private int currentSkill = 0;
        private int lastSkill = 0;
        private void damage(GameObject target, float damage)
        {
            // singleTargetIndex 재설정 필요
            target.GetComponent<Character>().setHp(-damage);
        }

        public void skillActivation()
        {
            if (lastSkill == 0)
            {
                Debug.Log("Skill Not Active");
                currentSkill = 0;
                return;
            }

            Debug.Log("Skill Active" + currentSkill);

            Skill skill = skillData.skills[lastSkill];
            if (skill.isSplash)
            {
                // 광역 스킬인 경우
                foreach (Monster monster in monsters)
                {
                    float typeMulti = (((skill.type + monster.getType()) % 3) - 1) / 2f;
                    float skillDamage = skill.damage * (1f + typeMulti);
                    damage(monster.gameObject, skillDamage);
                }
            }
            else
            {
                // 단일 스킬인 경우
                float typeMulti = (((skill.type + monsters[singleTargetIndex].GetComponent<Monster>().getType()) % 3) - 1) / 2f;
                float skillDamage = skill.damage * (1f + typeMulti);
                damage(monsters[singleTargetIndex].gameObject, skillDamage * attackMulti);
            }
            currentSkill = lastSkill = 0;
        }

        public void updateSkill(Direction direction)
        {
            currentSkill = skillData.skills[currentSkill].getNextSkill(direction);
            if (currentSkill == -1)
            {
                // 해당 방향키의 스킬이 존재하지 않는 경우
                skillActivation();
            }
            else
            {
                // 해당 방향키의 스킬이 존재하는 경우
                if (skillData.skills[currentSkill].isEnable)
                {
                    lastSkill = currentSkill;
                }
            }
        }

        public void monsterAttack(int monsterIndex)
        {
            int power = monsters[monsterIndex].GetComponent<Monster>().getPower();
            Debug.Log(power);
            damage(player, power * attackMulti);
        }

        public void setVariable()
        {
            player = GameObject.FindObjectOfType<Player>().gameObject;
            player.GetComponent<Player>().setHp(MAX_HP);
            monsters = GameObject.FindObjectsByType<Monster>(FindObjectsSortMode.None);
            foreach(Monster monster in monsters)
            {
                monster.setHp(MAX_HP);
            }
        }
    }

}