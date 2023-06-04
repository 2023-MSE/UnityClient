using System;
using UnityEngine;

namespace DungeonInfoFolder.DungeonAndUIInterfaces
{
    public enum DungeonEditorType
    {
        Create,
        Edit
    }
    
    public class DungeonEditor : Singleton<DungeonEditor>
    {
        public DungeonEditorType dungeonEditorType = DungeonEditorType.Create;
        public Dungeon editingDungeon;
        
        public void SetDungeonEditorType(DungeonEditorType type)
        {
            dungeonEditorType = type;
        }
        
        public void SetDungeonEditorTypeToCreate()
        {
            SetDungeonEditorType(DungeonEditorType.Create);
        }
        
        public void SetDungeonEditorTypeToEdit()
        {
            SetDungeonEditorType(DungeonEditorType.Edit);
        }
    }
}
