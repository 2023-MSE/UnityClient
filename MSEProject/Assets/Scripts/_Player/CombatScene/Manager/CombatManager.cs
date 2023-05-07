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
        [SerializeField] private SkillDataScriptableObject skillData;
        private int singleTargetIndex = 0;
        private Monster[] monsters;
        private GameObject player;
        private float attackMulti = 1.0f;
        private int currentSkill = 0;
        private int lastSkill = 0;

        private Queue<GameObject> queue;

        private Queue<GameObject> ObjectPool;

        private List<GameObject> list = new List<GameObject>();

        private GameObject note;


        private DungeonManager _dungeonManager;

        private CoolDown _coolDown;

        private void Start()
        {
            queue = new Queue<GameObject>();
            ObjectPool = new Queue<GameObject>();
            _coolDown = FindObjectOfType<CoolDown>();
        }


        public void setQueue(GameObject[] note)
        {
        
            foreach (var v in note)
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

            _dungeonManager = FindObjectOfType<DungeonManager>();
            setMonsters();

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
            // singleTargetIndex �缳�� �ʿ�
            target.GetComponent<Character>().setHp(-damage);
        }

        private void setMonsters()
        {
            int i = 0;
            foreach (var mon in _dungeonManager.GetMonstersInDungeon())
            {

                // target is dead
                if (target.GetComponent<Character>() is Player)
                {
                    
                    // player is dead
                    _coolDown.DamageToZero();

                }
                else if (target.GetComponent<Character>() is Monster) 
                {
                    // monster is dead

                }
            }
            else
            {
                //target is already alive
                if (target.GetComponent<Character>() is Player)
                {
                    _coolDown.DamageHp(0.1f);

                }
                else if (target.GetComponent<Character>() is Monster) 
                {
                    // monster is dead

                }

            }
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
                
                // ���� ��ų�� ���
                foreach (Monster monster in monsters)
                {
                    float typeMulti = (((skill.type + monster.getType()) % 3) - 1) / 2f;
                    float skillDamage = skill.damage * (1f + typeMulti);
                    damage(monster.gameObject, skillDamage);
                   //skill.effect.transform.position = monster.transform.position;

                    //StartCoroutine(skillTime(skill.effect, 2f));
                }
            }
            else
            {
                // ���� ��ų�� ���
                float typeMulti = (((skill.type + monsters[singleTargetIndex].GetComponent<Monster>().getType()) % 3) - 1) / 2f;
                float skillDamage = skill.damage * (1f + typeMulti);
                damage(monsters[singleTargetIndex].gameObject, skillDamage * attackMulti);
                
                //skill.effect.transform.position = monsters[singleTargetIndex].transform.position;
                    
                //StartCoroutine(skillTime(skill.effect, 2f));
            }
            currentSkill = lastSkill = 0;
        }

        IEnumerator skillTime(GameObject skill,float wait)
        {
            skill.SetActive(true);

            yield return wait;
            
            skill.SetActive(false);
        }

        public void updateSkill(Direction direction)
        {
            currentSkill = skillData.skills[currentSkill].getNextSkill(direction);
            if (currentSkill == -1)
            {
                // �ش� ����Ű�� ��ų�� �������� �ʴ� ���
                skillActivation();
            }
            else
            {
                // �ش� ����Ű�� ��ų�� �����ϴ� ���
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

            Debug.Log("mosterAttack");
            isPlayerHit = true;
            player.GetComponent<Player>().AnimateDefenceMotion();
            attackMonsterPower = monsters[monsterIndex].GetComponent<Monster>().getPower();
            monsters[monsterIndex].AnimateAttack();
        }
        public void monsterAttackDefence(int monsterIndex)
        {
            Debug.Log("monsterAttackDefence");
            isPlayerHit = false;
            attackMonsterPower = monsters[monsterIndex].GetComponent<Monster>().getPower();
            monsters[monsterIndex].AnimateAttack();
            player.GetComponent<Player>().AnimateDefenceMotion();
        }

        public void MonsterAttackPlayer()
        {
            if (isPlayerHit)
            {
                damage(player, attackMonsterPower * DungeonManager.instance.GetSpeed());
            }
            else
            {
                player.GetComponent<Player>().AnimateDefendeHitMotion();
            }

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


            player.GetComponent<Player>().AnimateIdle(DungeonManager.instance.GetSpeed());
            FindObjectOfType<NoteManager>().CombatManagerReady(this);
        }

        public void InteractBuff()
        {
            DungeonManager.instance.SetSpeed(1.5f);
        }

        public void InteractRelax(int heal)
        {
            if (player.GetComponent<Player>())
            {
                Debug.Log("interact r elax");
                player.GetComponent<Player>().setHp(heal);
                player.GetComponent<Player>().AnimateIsDrink();
               
            }

            
            
        }

        public GameObject GetPlayer()
        {
            return player;
        }

        public bool GetStageReady()
        {
            return isStageReady;

        }
    }

}