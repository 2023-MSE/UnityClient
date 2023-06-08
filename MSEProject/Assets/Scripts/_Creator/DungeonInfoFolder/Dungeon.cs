using System.Collections.Generic;
using UnityEditor;
using System;
using System.Collections;
using UnityEngine;

namespace DungeonInfoFolder
{
    [Serializable]
    public class Dungeon
    {
        // this used for local save dungeon stage info
        // public string id;
        // this used for server save dungeon stage info
        public string nodeEditorJsonData;
        public long id;
        
        public string name;
        public string createdTime;
        
        public Dictionary<ulong, Stage> dStages = new Dictionary<ulong, Stage>();
        public List<Stage> stages = new List<Stage>();

        public long userId;
        public ulong recentID = 2;
        public ulong level;

        public Dungeon()
        {
            if (UserInfoHolder.Instance.myInfo == null)
                userId = 0;
            else
                userId = UserInfoHolder.Instance.myInfo.id;
        }
        
        // Test Default Dungeon
        public Dungeon(bool thisIsTest)
        {
            if (thisIsTest)
            {
                Stage stage = new Stage(0);
                stage.nextStage.Add(1);
                stage.nextStage.Add(2);
                stage.nextStage.Add(3);
                stage.elements.Add(0);
                stage.elements.Add(1);
                stage.stageType = Stage.StageType.Monster;
                dStages.Add(0, stage);
                for (ulong i = 1; i < 11; i++)
                    dStages.Add(i, new Stage(i));
            }
        }

        public void ConvertStagesDictionaryToList()
        {
            stages = new List<Stage>();
            
            foreach (var stage in dStages)
            {
                stages.Add(stage.Value);
                Debug.Log("Stage ID : " + stage.Key);
            }
        }
        
        public void ConvertStagesListToDictionary()
        {
            dStages = new Dictionary<ulong, Stage>();
            
            foreach (var stage in stages)
            {
                dStages.Add(stage.identifierId, stage);
            }
            
            recentID = (ulong)(dStages.Count + 10);
        }
        
        public ulong ConvertStagesListToDictionaryAndReturnFirst()
        {
            ConvertStagesListToDictionary();
            return stages[0].identifierId;
        }
        public void ConvertStagesMusicBytesDataToBase64()
        {
            foreach (var stage in dStages)
            {
                stage.Value.MusicBytesDataToBase64();
            }
        }
        
        public void ConvertStagesBase64ToMusicBytesData()
        {
            foreach (var stage in dStages)
            {
                stage.Value.Base64ToMusicBytesData();
            }
        }

        public void ShowDungeonData()
        {
            // Show Dungeon Fields
            Debug.Log("Dungeon ID : " + id);
            Debug.Log("Dungeon NodeJsonData: " + nodeEditorJsonData);
            
            Debug.Log("Dungeon Name: " + name);
            Debug.Log("Created Time: " + createdTime);
            Debug.Log("Dungeon Level: " + level);

            foreach (var stage in dStages)
            {
                // Show Stage Fields
                Debug.Log("Stage Dictionary ID : " + stage.Key);
                Debug.Log("Stage ID : " + stage.Value.identifierId);
            }

            foreach (var stage in stages)
            {
                Debug.Log("Stage ID : " + stage.identifierId);
            }
        }
        
        
    }
}
