using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonInfoFolder;
using _Creator.DungeonInfoFolder;

/*
 * stage spawner
 * 
 * stage ������ �̿��Ͽ� �÷��̾�, ����
 * ����, �޽Ŀ� ��ں��� ������ִ� script
 * 
 * **/

namespace _Player.CombatScene
{
    public class StageSpawner : MonoBehaviour
    {
        [SerializeField]
        private StageInfoScriptableObject stageInfo;
        [SerializeField]
        GameObject spawnPointPlayer;
        [SerializeField]
        GameObject[] spawnPointMonster;
        [SerializeField]
        GameObject spawnPointBuffAndRelax;
        
        private void Start()
        {
        

        }

        // spawn monster
        private void spawnMonster(uint monsterIndex, int spawnIndex)
        {
            GameObject item = null;
            foreach (StageInfoStruct info in stageInfo.stageInfoTemplate)
            {
                if(info.thisStageInfoIndex == monsterIndex)
                {
                    item = Resources.Load<GameObject>(info.prefabPath);
                    break;
                }
            } 
            GameObject.Instantiate(item, spawnPointMonster[spawnIndex].transform.position, Quaternion.identity).transform.parent = spawnPointMonster[spawnIndex].transform;
        }

        // spawn player;
        private void spawnPlayer()
        {
            GameObject item = Resources.Load<GameObject>("Player");
            GameObject.Instantiate(item, spawnPointPlayer.transform.position, Quaternion.identity).transform.parent = spawnPointPlayer.transform;
        }
        // spawn relax;
        private void spawnRelax()
        {
<<<<<<< Updated upstream
            GameObject item = Resources.Load<GameObject>("Relax");
            GameObject.Instantiate(item, spawnPointBuffAndRelax.transform.position, Quaternion.identity).transform.parent = spawnPointBuffAndRelax.transform;
=======
            // WARNING
            // Relax's index SHOULD BE 1

            AsyncOperationHandle handle = DungeonManager.instance.GetHandle(1);
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result is GameObject)
            {
                Instantiate(handle.Result as GameObject, spawnPointBuffAndRelax.transform.position, Quaternion.identity);
                Debug.Log("instantiate Success: Relax");
   
                
            }
            else
            {
                Debug.Log("instantiate Fail: Relax");
            }
>>>>>>> Stashed changes
        }
        // spawn buff;
        private void spawnBuff(uint buffindex)
        {
            GameObject item = null;
            foreach (StageInfoStruct info in stageInfo.stageInfoTemplate)
            {
                if (info.thisStageInfoIndex == buffindex)
                {
                    item = Resources.Load<GameObject>(info.prefabPath);
                    break;
                }
            }
            GameObject.Instantiate(item, spawnPointBuffAndRelax.transform.position, Quaternion.identity).transform.parent = spawnPointBuffAndRelax.transform;
        }

        public void spawnStage(Stage stage)
        {
            spawnPlayer();
            switch(stage.myStageType)
            {
                case DungeonInfoFolder.Stage.StageType.Boss:
                    {
                        int i = 0;
                        foreach (uint index in stage.elements)
                        {
                            spawnMonster(index, i++);
                        }
                        break;
                    }
                case DungeonInfoFolder.Stage.StageType.Monster:
                    {
                        int i = 0;
                        foreach (uint index in stage.elements)
                        {
                            spawnMonster(index, i++);
                        }
                        break;
                    }
                case DungeonInfoFolder.Stage.StageType.Totem:
                    spawnBuff(stage.elements[0]);
                    break;
                case DungeonInfoFolder.Stage.StageType.Relax:
                    spawnRelax();
                    DungeonManager.instance.SetRelaxManager();
                    break;
                default:
                    Debug.Log("ERROR Unknown Node");
                    break;
            }
            GameObject.Find("CombatManager").GetComponent<CombatManager>().setVariable();
        }
    }
}
