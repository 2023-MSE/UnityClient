using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Creator.DungeonInfoFolder;
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
        private ulong currentStage;
        [SerializeField]
        private StageInfoScriptableObject stageInfo;
        private Dictionary<uint, AsyncOperationHandle> assetDict;
        private float speed = 1f;
        private bool check = false;
 

        private void Start()
        {
            
            assetDict = new Dictionary<uint, AsyncOperationHandle>();
            currentStage = 0;
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
            speed = 1.5f;
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

 
    
    }
}
