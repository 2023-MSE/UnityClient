using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonInfoFolder;
using _Creator.DungeonInfoFolder;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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

        // spawn monster
        private void spawnMonster(uint monsterIndex, int spawnIndex)
        {
            AsyncOperationHandle handle = DungeonManager.instance.GetHandle(monsterIndex);
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result is GameObject)
            {
                Instantiate(handle.Result as GameObject, spawnPointMonster[spawnIndex].transform.position, Quaternion.identity);
                Debug.Log("instantiate Success: Monster");
            }
            else
            {
                Debug.Log("instantiate Fail: Monster");
            }
        }

        // spawn player;
        private void spawnPlayer()
        {
            // WARNING
            // Player's index SHOULD BE 0
            AsyncOperationHandle handle = DungeonManager.instance.GetHandle(0);
            Instantiate(handle.Result as GameObject, spawnPointPlayer.transform.position, Quaternion.Euler(0f, 180f, 0f));
        }

        // spawn relax;
        private void spawnRelax()
        {
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
        }

        // spawn buff;
        private void spawnBuff(uint buffindex)
        {
            AsyncOperationHandle handle = DungeonManager.instance.GetHandle(buffindex);
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result is GameObject)
            {
                Instantiate(handle.Result as GameObject, spawnPointBuffAndRelax.transform.position, Quaternion.identity);
                Debug.Log("instantiate Success: Buff");
            }
            else
            {
                Debug.Log("instantiate Fail: Buff");
            }
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
                    //물어보기
                   //DungeonManager.instance.SetDungeon();
                    break;
                case DungeonInfoFolder.Stage.StageType.Relax:
                    spawnRelax();
                    DungeonManager.instance.SetRelaxManager();
                    break;
                default:
                    Debug.Log("ERROR Unknown Node");
                    break;
            }
            DungeonManager.instance.SetCombatManager();
        }
    }
}
