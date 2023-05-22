using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SearchService;

namespace _Player.CombatScene
{

    public class CombatManager : MonoBehaviour
    {
        public const int MAX_HP = 9999999;
        [SerializeField] private SkillDataScriptableObject skillData;
        [SerializeField]
        private GameObject[] patterns = new GameObject[4];
        public GameObject[] getNotes
        {
            get => patterns;
        }

        [SerializeField] private GameObject gnote;
        
        [SerializeField] private GameObject uinote;



        public GameObject getGNote
        {
            get => gnote;
        }
        
        private List<Monster> monsters;
        private Player player;
        private int currentSkill = 0;
        private bool isStageReady = false;
        private int attackMonsterPower;
        private bool _isPlayerHit;
        private int deadMonster = 0;

        public GameObject GameOver;

        private FadeEffect _fadeEffect;
        
        public bool isPlayerHit
        {
            get => _isPlayerHit;
            set
            {
                _isPlayerHit = value;
                Debug.Log("isPlayerHit Changed");
            }
        }
        
        private Queue<GameObject> queue;

        private List<String> skilllist=new List<string>();
        
        private Queue<GameObject> ObjectPool;

        private DungeonManager _dungeonManager;

        private GameObject note;

        private CoolDown _coolDown;

        private TestDirButton test;
        
        private bool check = false;

        public GameObject effect;

        private void Start()
        {
            
            _fadeEffect = FindObjectOfType<FadeEffect>();
            queue = new Queue<GameObject>();
            ObjectPool = new Queue<GameObject>();
            _coolDown = FindObjectOfType<CoolDown>();
            player = FindObjectOfType<Player>();
            _dungeonManager = FindObjectOfType<CombatScene.DungeonManager>();
            test = FindObjectOfType<TestDirButton>();
            GameOver.SetActive(false);
  
        }

        private void Update()
        {
            //StopCombat();
        }

        public Monster GetRandomMonster()
        {
            return monsters[UnityEngine.Random.Range(0, monsters.Count)];
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

                //obj.SetActive(true);
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
            // singleTargetIndex ??? ??
            target.GetComponent<Character>().AnimateHitMotion();
            if (target.GetComponent<Character>().setHp(-damage))
            {
                // target is dead
                if (target.GetComponent<Character>() is Player)
                {
                    _fadeEffect.fadein();
                    
                    GameOver.SetActive(true);
                    uinote.SetActive(false);
                    

                }
                else if (target.GetComponent<Character>() is Monster)
                {
                    // monster is dead
                    deadMonster++;
                    monsters.Remove(target.GetComponent<Monster>());
                    if (deadMonster == 3)
                    {
                       
                       
                        uinote.SetActive(false);


                        if (!_fadeEffect.isFading())
                        {
                            _fadeEffect.fadein();
                            Debug.Log("fade "+_fadeEffect.isFading());
                            //StartCoroutine(FadeIn(3f));
                        }


                    }

                }
            }
            else
            {
                //target is already alive
                if (target.GetComponent<Character>() is Player)
                {
                

                }
                else if (target.GetComponent<Character>() is Monster)
                {
                   

                }
            }
        }

       

      

        public List<String> sendSkill()
        {
            foreach (var effect in skillData.skills)
            {
                if (effect.isEnable)
                {
                    skilllist.Add(effect.getEffect());
                }
            }

            return skilllist;
        }

        // !!!!!Relax Scene -> ??? ? ????
        public void StopCombat()
        {
            //TestDirButton test = FindObjectOfType<TestDirButton>();
            int num = 0;
            foreach (Monster m in monsters)
            {
                if (m.setHp(0))
                {
                    num++;
                }
                    
            }

            if (num == monsters.Count)
            {
                Debug.Log("Success!! go to Next Stage");
                //test.OnClickButtonNextStage();
            }

            if (player.setHp(0))
            {
                //Time.timeScale = 0;
            }
        }
        


        public void skillActivation()
        {
            Debug.Log("Skill Active" + currentSkill+"dungeon speed"+ _dungeonManager.GetSpeed());

            float speed = 3 / DungeonManager.instance.GetSpeed();
            
            Skill skill = skillData.skills[currentSkill];
            if (skill.isSplash)
            {
                List<Monster> tempMonsters = new List<Monster>(monsters);
                // ?? ??? ??
                foreach (Monster monster in tempMonsters)
                {
                    if (!monster.isDead())
                    {  
                       StartCoroutine(skillTime(skillData.skills[currentSkill].effect,monster.transform.position,speed));
                        
                        float typeMulti = (((skill.type + monster.getType()) % 3) - 1) / 2f;
                        float skillDamage = skill.damage * (1f + typeMulti);
                        damage(monster.gameObject, skillDamage);
                    }
                }
            }
            else
            {
                Monster singleTraget = GetRandomMonster();
                StartCoroutine(skillTime(skillData.skills[currentSkill].effect, singleTraget.transform.position,speed));
                
                float typeMulti =
                    (((skill.type + singleTraget.getType()) % 3) - 1) / 2f;
                float skillDamage = skill.damage * (1f + typeMulti);
                damage(singleTraget.gameObject, skillDamage * DungeonManager.instance.GetSpeed());
            }


            currentSkill = 0;
        }
        IEnumerator skillTimeWait(GameObject skill, float wait)
        {
            skill.SetActive(true);

            yield return wait;
            
            
            skill.SetActive(false);

            
        }
        IEnumerator skillTime(GameObject skill, Vector3 pos,float wait)
        {
            GameObject s=Instantiate(skill,pos, Quaternion.identity);
            s.SetActive(true);
            
            yield return new WaitForSeconds(wait);
            
  
            Destroy(s);
            
        }

        public void updateSkill(Direction direction)
        {
            
            Debug.Log(skillData.skills.Count);
            currentSkill = skillData.skills[currentSkill].getNextSkill(direction);
            Debug.Log("Current Skill num: " + currentSkill);
            if (currentSkill == -1)
            {
                // ?? ???? ??? ???? ?? ??
                player.GetComponent<Player>().AnimateMissMotion();
            }
            else
            {
                if (skillData.skills[currentSkill].isEnable)
                {
                    // ?? ???? ??? ???? ??
                    Debug.Log("SKILL ENABLE");
                    player.GetComponent<Player>().AnimateSkillMotion();
                   
                }

                player.GetComponent<Player>().AnimateDirectionMotion(direction);
            }
        }

        public void monsterAttack(int monsterIndex)
        {
            Debug.Log("--check--attack");
            Debug.Log("monster attack "+ monsterIndex);
            
            isPlayerHit = true;
            player.GetComponent<Player>().AnimateDefenceMotion();
            if (monsters[monsterIndex] is BossMonster)
            {
                int random = UnityEngine.Random.Range(0, 1);
                BossMonster boss = monsters[monsterIndex] as BossMonster;
                switch (random)
                {
                    case 0:
                        attackMonsterPower = boss.GetBossPower();
                        boss.AnimateBossAttack();
                        break;
                    case 1:
                        attackMonsterPower = boss.getPower();
                        boss.AnimateAttack();
                        break;
                }
            }
            else
            {
                attackMonsterPower = monsters[monsterIndex].getPower();
                monsters[monsterIndex].AnimateAttack();
            }
        }

        public void monsterAttackDefence(int monsterIndex)
        {
            Debug.Log("--check--defence");
            isPlayerHit = false;
            
            if (monsters[monsterIndex] is BossMonster)
            {
                int random = UnityEngine.Random.Range(0, 1);
                BossMonster boss = monsters[monsterIndex] as BossMonster;
                switch (random)
                {
                    case 0:
                        attackMonsterPower = boss.GetBossPower();
                        boss.AnimateBossAttack();
                        boss.attackeffect();
                        break;
                    case 1:
                        attackMonsterPower = boss.getPower();
                        boss.AnimateAttack();
                        boss.attackeffect();
                        break;
                }
            }
            else
            {
                attackMonsterPower = monsters[monsterIndex].getPower();
                monsters[monsterIndex].AnimateAttack();
            }
            player.GetComponent<Player>().AnimateDefenceMotion();
        }

        public void MonsterAttackPlayer()
        {
            Debug.Log("--check--" + isPlayerHit);
            if (isPlayerHit)
            {
                Debug.Log("Hit");
                Debug.Log("attack :"+attackMonsterPower);
               damage(player.gameObject, attackMonsterPower * DungeonManager.instance.GetSpeed());
               StartCoroutine(effectPlayer(1f));
               
               _coolDown.DamageHp(attackMonsterPower * DungeonManager.instance.GetSpeed()* 0.001f);
            }
            else
            {
                Debug.Log("Defend");
                player.GetComponent<Player>().effectDefend(1f);
                player.GetComponent<Player>().AnimateDefendeHitMotion();
            }
        }
        
        IEnumerator effectPlayer(float wait)
        {
            Debug.Log("player effect");
            GameObject b=Instantiate(effect, player.gameObject.transform.position-new Vector3(0,0,1.5f), Quaternion.identity);

            yield return new WaitForSeconds(wait);
            
            Destroy(b);

        }

        public void setVariable()
        {
            
            
            
            GameOver.SetActive(false);
            int i = 0;
            Debug.Log("SetVariable");
            player = GameObject.FindObjectOfType<Player>();
            player.GetComponent<Player>().setHp(MAX_HP);
            monsters = new List<Monster>(GameObject.FindObjectsByType<Monster>(FindObjectsSortMode.None));
            Debug.Log("aaaaa"+monsters.Count);
            foreach (Monster monster in monsters)
            {  
                monster.setNum(i++);
                monster.setHp(MAX_HP);
                monster.AnimateIdle(DungeonManager.instance.GetSpeed());
               
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
            
            Debug.Log("interact relax");
           }

        public GameObject GetPlayer()
        {
            return player.gameObject;
        }

        public bool GetStageReady()
        {
            return isStageReady;
        }
        public void StopScene()
        {
            check = !check;

            if (check)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void SpeedUp()
        {
            Time.timeScale += 0.1f;
        }
        
        public void SpeedDown()
        {
            Time.timeScale -= 0.1f;
        }
        
     
    }
}

