using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Player.CombatScene
{
    public class DungeonManager : MonoBehaviour
    {
        /*
         * Make it singleton
         * **/
        public static DungeonManager instance = null;

        private bool check = false;

        private DungeonManager() { }
        public static DungeonManager Instance
        {
            get
            {
                if (null == instance)
                {
                    return null;
                }
                return instance;
            }
        }
        void Awake()
        {
            if (null == instance)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
                dungeon = null;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            GetMonstersInDungeon();
        }

        // 테스트를 위해 dungeon을 public으로 수정함. 이후 private으로 변환 예정
        public DungeonInfoFolder.Dungeon dungeon;
<<<<<<< Updated upstream
=======
        private CombatManager combatManager;
        private RelaxManager relaxmanager;
>>>>>>> Stashed changes
        private ulong currentStage;
        public void SetDungeon(DungeonInfoFolder.Dungeon dungeon)
        {
            this.dungeon = dungeon;
        }
        public DungeonInfoFolder.Dungeon GetDungeon()
        {
            // 지도 보여줄때 필요함
            return dungeon;
        }

        public GameObject[] GetMonstersInDungeon()
        {
            // ? ???? ??? ?? ????
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            
            Debug.Log(monsters.Length);

            return monsters;
        }

        public void GoNextStage(ulong nextStage)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CombatScene");
            currentStage = nextStage;
        }
<<<<<<< Updated upstream
=======

        public AsyncOperationHandle GetHandle(uint index)
        {
            return assetDict[index];
        }

        public float GetSpeed()
        {
            return speed;
        }

        public void SetSpeed(float multi)
        {
            speed = (speed * multi > 2) ? 2f : speed * multi;
        }

        public void SetCombatManager()
        {
            combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
            combatManager.setVariable();
        }
        
        public void SetRelaxManager()
        {
            Debug.Log("scene relax");
            relaxmanager = GameObject.Find("RelaxManager").GetComponent<RelaxManager>();
            relaxmanager.Scenecheck();
        }

        public void SkillActivation()
        {
            combatManager.skillActivation();
        }

        public void MonsterAttack()
        {
            combatManager.MonsterAttackPlayer();
        }

>>>>>>> Stashed changes
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene Loaded");
            if (Instance != null && dungeon != null)
            {
                GameObject.Find("StageSpawner").GetComponent<StageSpawner>().spawnStage(dungeon.stages[0]);
            }
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
    }
}
