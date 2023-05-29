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
    public class DungeonManager : MonoBehaviour
    {
        /*
         * Make it singleton
         * **/
        public static DungeonManager instance = null;

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


        // ?????? ???? dungeon?? public???? ??????. ???? private???? ??? ????
        public DungeonInfoFolder.Dungeon dungeon;
        private CombatManager combatManager;
        private RelaxManager relaxmanager;
        private BuffManager _buffManager;
        private ulong currentStage;
        [SerializeField]
        private StageInfoScriptableObject stageInfo;
        private Dictionary<uint, AsyncOperationHandle> assetDict;
        private float playerHP;
        private bool check = false;
 

        private void Start()
        {
            
            assetDict = new Dictionary<uint, AsyncOperationHandle>();
            currentStage = 0;
            playerHP = CombatManager.MAX_HP;
            foreach (StageInfoStruct info in stageInfo.stageInfoTemplate)
            {
                Addressables.LoadAssetAsync<GameObject>(info.prefabPath).Completed +=
                (handle) =>
                {
                    Debug.Log("Load Asset " + info.stageInfo);
                    Debug.Assert(handle.Status == AsyncOperationStatus.Succeeded, "Fail to load Asset" + handle.Status);
                    assetDict.Add(info.thisStageInfoIndex, handle);
                };
                
            }
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
            return dungeon.stages[currentStage].myStageType;
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

        public AsyncOperationHandle GetHandle(uint index)
        {
            return assetDict[index];
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
        public void SetBUFFManager()
        {
            Debug.Log("scene relax");
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
            if (Instance != null && dungeon != null)
            {
                GameObject.Find("StageSpawner").GetComponent<StageSpawner>().spawnStage(dungeon.stages[currentStage]);
            }
        }

        public float GetPlayerHP()
        {
            return playerHP;
        }

        public void SetPlayerHP(float hp)
        {
            playerHP = hp;
            Debug.Log("Set Player Hp to :" + hp);
        }

        public void SetspeedUpDown(int i)
        {
            if (i == 0)
            {
                Time.timeScale -= 0.1f;
            }
            else if (i == 1)
            {
                Time.timeScale += 0.1f;
            }
        }

        public List<ulong> GetNextStages()
        {
            foreach (var ex in dungeon.stages[currentStage].nextStageID)
            {
                Debug.Log("list n"+ex);
                
                
            }
            foreach (var ex in dungeon.stages[currentStage].prevStageID)
            {
                Debug.Log("list p"+ex);
            }
            return dungeon.stages[currentStage].nextStageID;
        }
    }
}
