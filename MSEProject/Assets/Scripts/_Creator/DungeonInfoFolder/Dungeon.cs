using System.Collections.Generic;
using UnityEditor;
using System;
using System.Collections;

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
        public ulong recentID = 0;
        public ulong level;

        public Dungeon()
        {
            //userId = UserInfoHolder.Instance.myInfo.id;
            userId = 2;
        }
        
        // Test Default Dungeon
        public Dungeon(bool thisIsTest)
        {
            if (thisIsTest)
            {
                Stage stage = new Stage(0);
                stage.nextStageID.Add(1);
                stage.nextStageID.Add(2);
                stage.nextStageID.Add(3);
                stage.elements.Add(0);
                stage.elements.Add(1);
                stage.myStageType = Stage.StageType.Monster;
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
            }
        }
        
        public void ConvertStagesListToDictionary()
        {
            dStages = new Dictionary<ulong, Stage>();
            
            foreach (var stage in stages)
            {
                dStages.Add(stage.nodeID, stage);
            }
        }
    }
}
