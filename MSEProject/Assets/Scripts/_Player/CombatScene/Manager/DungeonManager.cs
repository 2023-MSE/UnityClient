using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Creator.DungeonInfoFolder;
using DungeonInfoFolder;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Player.CombatScene
{
    public class DungeonManager : Singleton<DungeonManager>
    {

        // ?????? ???? dungeon?? public???? ??????. ???? private???? ??? ????
        public DungeonInfoFolder.Dungeon dungeon;
        private DeployedDungeon _dungeonInfo;
        private CombatManager combatManager;
        private RelaxManager relaxmanager;
        private BuffManager _buffManager;
        private ulong currentStage;
        [SerializeField]
        private StageInfoScriptableObject stageInfo;
        private float playerHP;
        private bool check = false;
        private AudioSource audioSource;
        private NoteManager _noteManager;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        public void SetDeployedDungeon(DeployedDungeon dungeon)
        {
            _dungeonInfo = dungeon;
        }
        public DeployedDungeon GetDeployedDungeon()
        {
            return _dungeonInfo;
        }
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            currentStage = 0;
            playerHP = CombatManager.MAX_HP;
            AddressableManager.Instance.SetAddressable(stageInfo.stageInfoTemplate);
        }
        public void SetDungeon(DungeonInfoFolder.Dungeon dungeon)
        {
            this.dungeon = dungeon;
        }

        public ulong GetCurrentStage()
        {
            return currentStage;
        }
        public DungeonInfoFolder.Dungeon GetDungeon()
        {
            // ???? ??????? ?????
            return dungeon;
        }
        public DungeonInfoFolder.Stage.StageType GetCurrentStageType()
        {
            return dungeon.dStages[currentStage].stageType;
        }
        public void GoNextStage(ulong nextStage)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CombatScene");
            currentStage = nextStage;
        }

        public void GotoMain()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TestConvertTo Combat Scene");
        }

        public void SetCombatManager()
        {
            combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
            combatManager.setVariable();
        }
        
        public void SetRelaxManager()
        {
            relaxmanager = GameObject.Find("RelaxManager").GetComponent<RelaxManager>();
            relaxmanager.Scenecheck();
        }
        public void SetBUFFManager()
        {
            _buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
            _buffManager.Scenecheck_Buff();
        }

        public void SkillActivation()
        {
            combatManager.skillActivation();
        }

        public void MonsterAttack()
        {
            combatManager.MonsterAttackPlayer();
        }
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
            GameObject stageSpawner = GameObject.Find("StageSpawner");
            
            if (dungeon != null && stageSpawner != null)
            {
                stageSpawner.GetComponent<StageSpawner>().spawnStage(dungeon.dStages[currentStage]);
                Debug.Log(dungeon.dStages[currentStage].musicData);
                audioSource.clip = dungeon.dStages[currentStage].musicData;
                audioSource.Play();
                _noteManager = GameObject.Find("Note").GetComponent<NoteManager>();
                _noteManager.SetBpm(dungeon.dStages[currentStage].bpm);
            }
        }

        public float GetPlayerHP()
        {
            Debug.Log("notion : get Player Hp to :" + playerHP);
            return playerHP;
        }

        public void SetPlayerHP(float hp)
        {
            playerHP = hp;
            Debug.Log("notion : Set Player Hp to :" + hp);
        }

        public void SetspeedUpDown(int i)
        {
            if (i == 0)
            {
                Time.timeScale -= 0.1f;
                audioSource.pitch = Time.timeScale;
            }
            else if (i == 1)
            {
                Time.timeScale += 0.1f;
                audioSource.pitch = Time.timeScale;
            }
        }

        public List<ulong> GetNextStages()
        {
            foreach (var ex in dungeon.dStages[currentStage].nextStage)
            {
                Debug.Log("list n"+ex);
                
                
            }
            foreach (var ex in dungeon.dStages[currentStage].prevStage)
            {
                Debug.Log("list p"+ex);
            }
            return dungeon.dStages[currentStage].nextStage;
        }
    }
}
