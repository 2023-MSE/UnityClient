using System.Collections;
using System.Collections.Generic;
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

        // �׽�Ʈ�� ���� dungeon�� public���� ������. ���� private���� ��ȯ ����
        public DungeonInfoFolder.Dungeon dungeon;
        private ulong currentStage;
        public void SetDungeon(DungeonInfoFolder.Dungeon dungeon)
        {
            this.dungeon = dungeon;
        }
        public DungeonInfoFolder.Dungeon GetDungeon()
        {
            // ���� �����ٶ� �ʿ���
            return dungeon;
        }

        public void GoNextStage(ulong nextStage)
        {
            // �̷��� ���� �� ���� �ε�Ǳ� �� �Ʒ� �ڵ尡 ����ɱ�? ���� �׷��ٸ� ��� �ذ����� ������ ������
            UnityEngine.SceneManagement.SceneManager.LoadScene("CombatScene");
            currentStage = nextStage;
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
                GameObject.Find("StageSpawner").GetComponent<StageSpawner>().spawnStage(dungeon.stages[0]);
            }
        }
    }
}
