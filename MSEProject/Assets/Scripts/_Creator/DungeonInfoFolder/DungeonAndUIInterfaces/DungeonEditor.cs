using System;
using UnityEngine;

namespace DungeonInfoFolder.DungeonAndUIInterfaces
{
    public class DungeonEditor : Singleton<DungeonEditor>
    {
        public Dungeon editingDungeon;

        private void Start()
        {
            /*// Test Code!!!!!!!!!!!!!!!!!!!!!!
            editingDungeon = new Dungeon();*/
        }
    }
}