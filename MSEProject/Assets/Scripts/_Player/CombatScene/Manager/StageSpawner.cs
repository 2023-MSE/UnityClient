using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonInfoFolder;
using _Creator.DungeonInfoFolder;

/*
 * stage spawner
 * 
 * stage 정보를 이용하여 플레이어, 몬스터
 * 버프, 휴식용 모닥불을 만들어주는 script
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
            GameObject item = Resources.Load<GameObject>("Relax");
            GameObject.Instantiate(item, spawnPointBuffAndRelax.transform.position, Quaternion.identity).transform.parent = spawnPointBuffAndRelax.transform;
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
                    break;
                default:
                    Debug.Log("ERROR Unknown Node");
                    break;
            }
            GameObject.Find("CombatManager").GetComponent<CombatManager>().setVariable();
        }
    }
}
