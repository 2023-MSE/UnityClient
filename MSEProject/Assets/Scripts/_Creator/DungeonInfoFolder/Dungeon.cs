using System.Collections.Generic;
using UnityEditor;
using System;

namespace DungeonInfoFolder
{
    [Serializable]
    public class Dungeon
    {
        // this used for local save dungeon stage info
        // public string id;
        // this used for server save dungeon stage info
        public string nodeEditorJsonData;
        
        public string name;
        public string createdTime;
        public Dictionary<ulong, Stage> stages = new Dictionary<ulong, Stage>();
        public ulong recentID = 0;
        public ulong level;

        public Dungeon(){ }
        
        // Test Default Dungeon
        public Dungeon(bool thisIsTest)
        {
            if (thisIsTest)
            {
                Stage stage = new Stage(0);
                stage.nextStageID.Add(1);
                stage.nextStageID.Add(2);
                stage.elements.Add(0);
                stage.elements.Add(1);
                stage.myStageType = Stage.StageType.Monster;
                stages.Add(0, stage);
                for (ulong i = 1; i < 11; i++)
                    stages.Add(i, new Stage(i));
            }
        }
    }
}
