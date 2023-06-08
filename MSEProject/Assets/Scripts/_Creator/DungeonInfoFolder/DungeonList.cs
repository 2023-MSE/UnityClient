using System.Collections.Generic;
using System;
namespace DungeonInfoFolder
{
    [Serializable]
    public class DungeonList
    {
        public List<Dungeon> myDungeons = new List<Dungeon>();
        
        public void SetDungeonList(List<Dungeon> inputDungeons)
        {
            myDungeons = inputDungeons;
        }
    }
}
