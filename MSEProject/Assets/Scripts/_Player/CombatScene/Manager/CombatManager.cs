using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SearchService;

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
        private int currentSkill = 0;

        private Queue<GameObject> queue;

        private Queue<GameObject> ObjectPool;

        private List<GameObject> list = new List<GameObject>();
        
        private GameObject note;

        public void setQueue(GameObject[] q)
        {
            foreach (var v in q)
            {
                queue.Enqueue(v);
            }
           
        }

        public Queue<GameObject> getQueue()
        {
            return queue;
        }

        private void Start()
        {
            queue = new Queue<GameObject>();
            ObjectPool = new Queue<GameObject>();

        }

        public void EnqueueObjectPool(GameObject note)
        {
            ObjectPool.Enqueue(note);
        }

        public GameObject DequeueObjectPool()
        {
            return ObjectPool.Dequeue();
        }


        public GameObject GetNote()
        {
            if (queue.Count > 0)
            {
                var obj = queue.Dequeue();
                queue.Enqueue(obj);
                obj.SetActive(true);
                return obj;
            }
            else
            {
                //queue is empty
                Debug.Log("empty");
                return null;
            }

        }

        public void ReturnNoteToQueue(GameObject note)
        {
            Debug.Log("put note");
            queue.Enqueue(note);
           
            note.gameObject.SetActive(false);
 
        }

        private void damage(GameObject target, float damage)
        {
            // singleTargetIndex 재설정 필요
            target.GetComponent<Character>().AnimateHitMotion();
            if (target.GetComponent<Character>().setHp(-damage))
            {
                // target is dead
                if (target.GetComponent<Character>() is Player)
                {
                    // player is dead

                }
                else if (target.GetComponent<Character>() is Monster) 
                {
                    // monster is dead

                }
            }
        }

        public void skillActivation()
        {
            player.GetComponent<Player>().AnimateSkillMotion();
            Debug.Log("Skill Active" + currentSkill);

            Skill skill = skillData.skills[currentSkill];
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
                damage(monsters[singleTargetIndex].gameObject, skillDamage * DungeonManager.instance.GetSpeed());
            }
            currentSkill = 0;
        }

        public void updateSkill(Direction direction)
        {
            currentSkill = skillData.skills[currentSkill].getNextSkill(direction);

            if (currentSkill == -1)
            {
                // 해당 방향키의 스킬이 존재하지 않는 경우
                player.GetComponent<Player>().AnimateMissMotion();
            }
            else
            {
                player.GetComponent<Player>().AnimateDirectionMotion(direction);
                // 해당 방향키의 스킬이 존재하는 경우
                if (skillData.skills[currentSkill].isEnable)
                {
                    skillActivation();
                }
            }
        }

        public void monsterAttack(int monsterIndex)
        {
            int power = monsters[monsterIndex].GetComponent<Monster>().getPower();
            Debug.Log(power);
            monsters[monsterIndex].AnimateAttack();
            damage(player, power * DungeonManager.instance.GetSpeed());
        }
        public void monsterAttackDefence()
        {
            player.GetComponent<Player>().AnimateDefenceMotion();
        }
        public void setVariable()
        {
            player = GameObject.FindObjectOfType<Player>().gameObject;
            player.GetComponent<Player>().setHp(MAX_HP);
            monsters = GameObject.FindObjectsByType<Monster>(FindObjectsSortMode.None);
            foreach(Monster monster in monsters)
            {
                monster.setHp(MAX_HP);
                monster.AnimateIdle(DungeonManager.instance.GetSpeed());
            }
            player.GetComponent<Player>().AnimateIdle(DungeonManager.instance.GetSpeed());
        }

        public void InteractBuff()
        {
            DungeonManager.instance.SetSpeed(1.5f);
        }

        public void InteractRelax(int heal)
        {
            player.GetComponent<Player>().setHp(heal);
        }

        public GameObject GetPlayer()
        {
            return player;
        }
    }

}