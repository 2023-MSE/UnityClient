using System.Collections.Generic;
using UnityEditor;

namespace DungeonInfoFolder
{
    public class Dungeon
    {
        // this used for local save dungeon stage info
        public string id;
        // this used for server save dungeon stage info
        public string nodeEditorJsonData;
        
        public string name;
        public string createdTime;
        public Dictionary<ulong, Stage> stages = new Dictionary<ulong, Stage>();
        public ulong recentID = 0;

        public Dungeon()
        {
            id = GUID.Generate().ToString();
        }
        
        // Test Default Dungeon
        public Dungeon(bool thisIsTest)
        {
            if (thisIsTest)
                for (ulong i = 0 ; i < 11 ; i ++)
                    stages.Add(i, new Stage(i));
        }
    }
}
