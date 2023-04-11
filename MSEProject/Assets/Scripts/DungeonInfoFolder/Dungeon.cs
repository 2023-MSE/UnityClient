using System.Collections.Generic;

namespace DungeonInfoFolder
{
    public class Dungeon
    {
        public string name;
        public string createdTime;
        public Dictionary<long, Stage> stages = new Dictionary<long, Stage>();
        
        // Test Default Dungeon
        public Dungeon()
        {
            for (int i = 0 ; i < 11 ; i ++)
                stages.Add(i, new Stage(i));
        }
    }
}
