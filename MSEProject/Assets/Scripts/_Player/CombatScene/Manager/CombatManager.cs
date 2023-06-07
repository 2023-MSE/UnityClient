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
        public static int MAX_HP = 1000;
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
        private MapManager _mapManager;
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

        [SerializeReference] private GameObject Gnote;

        private void Start()
        {
            
            _fadeEffect = FindObjectOfType<FadeEffect>();
            queue = new Queue<GameObject>();
            _mapManager = FindObjectOfType<MapManager>();
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

        public void setQueue(List<GameObject> note)
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

        private int i = 0;


        public GameObject GetNote()
        {
            if (i == 3)
            {
                GetRandomMonster().attactMotion();
                i = 0;
            }

            if (queue.Count > 0)
            {
                var obj = queue.Dequeue();
                //queue.Enqueue(obj);

                //obj.SetActive(true);
                i++;
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
                    
                    GameOver.SetActive(true);
                    uinote.SetActive(false);

                    _fadeEffect.gameover();
                    
                    
                    

                }
                else if (target.GetComponent<Character>() is Monster)
                {
                    // monster is dead
                    deadMonster++;
                    monsters.Remove(target.GetComponent<Monster>());
                    if (deadMonster == 3) // 3?? ?? ? ? ??? ?? ??.
                    {
                        Debug.Log("notion 191 1 : "+player.getHp());
                        DungeonManager.instance.SetPlayerHP(player.getHp());
                       
                        uinote.SetActive(false);


                        if (!_fadeEffect.isFading())
                        {
                            _mapManager.DisplayList();

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
            float speed = 3 / Time.timeScale;
            
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
                damage(singleTraget.gameObject, skillDamage * Time.timeScale);
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

        public void MissGeneralNote()
        {
            currentSkill = 0;
            player.GetComponent<Player>().AnimateMissMotion();
        }
        
        public void updateSkill(Direction direction)
        {
            
        
            currentSkill = skillData.skills[currentSkill].getNextSkill(direction);

            if (currentSkill == -1)
            {
                // ?? ???? ??? ???? ?? ??
                player.GetComponent<Player>().AnimateMissMotion();
                currentSkill = 0;
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
            Monster m = GetRandomMonster();
            isPlayerHit = true;
            player.GetComponent<Player>().AnimateDefenceMotion();
            if (m is BossMonster)
            {
                int random = UnityEngine.Random.Range(0, 1);
                BossMonster boss = m as BossMonster;
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
                attackMonsterPower = m.getPower();
                m.AnimateAttack();
            }
        }

        //
        public void monsterAttackDefence(int monsterIndex)
        {

            isPlayerHit = false;
            Monster m = GetRandomMonster();
            if (m is BossMonster)
            {
                int random = UnityEngine.Random.Range(0, 1);
                BossMonster boss = m as BossMonster;
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
                attackMonsterPower = m.getPower();
                m.AnimateAttack();
            }
            player.GetComponent<Player>().AnimateDefenceMotion();
        }

        public void MonsterAttackPlayer()
        {
   
            if (isPlayerHit)
            {
         
               damage(player.gameObject, attackMonsterPower * Time.timeScale);
               StartCoroutine(effectPlayer(1f));
               _coolDown.DamageHp(attackMonsterPower * Time.timeScale * 0.001f);
            }
            else
            {
           
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
            Debug.Log("SetVariable");
            player = GameObject.FindObjectOfType<Player>();
            Debug.Log("notion : 455 player"+DungeonManager.instance.GetPlayerHP());
            
            player.GetComponent<Player>().setHp(DungeonManager.instance.GetPlayerHP());

            if (_coolDown == null)
            {
                Debug.Log("no!");
            }
            else
            {
                _coolDown.setHp(DungeonManager.instance.GetPlayerHP()*0.001f);
            }

            monsters = new List<Monster>(GameObject.FindObjectsByType<Monster>(FindObjectsSortMode.None));

            foreach (Monster monster in monsters)
            {
                
                monster.setNum(i++);
                monster.setHp(MAX_HP);
                monster.AnimateIdle();
            }

            player.GetComponent<Player>().AnimateIdle();
            
            FindObjectOfType<NoteManager>().CombatManagerReady(this);
        }

        public void InteractBuff()
        {
            Time.timeScale *= 1.5f;
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

