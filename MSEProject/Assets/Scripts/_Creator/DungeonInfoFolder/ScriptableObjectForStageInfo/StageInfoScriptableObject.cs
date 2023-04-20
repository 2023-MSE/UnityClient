using System;
using System.Collections.Generic;
using _Creator.DungeonInfoFolder.ScriptableObjectForStageInfo;
using UnityEngine;

namespace _Creator.DungeonInfoFolder
{
    [Serializable]
    public struct StageInfoStruct
    {
        public StageInfo stageInfo;
        public uint thisStageInfoIndex;
    }
    
    [CreateAssetMenu(fileName = "StageInfoTemplate", menuName = "Scriptable Object/StageInfoTemplate", order = int.MaxValue)]
    public class StageInfoScriptableObject : ScriptableObject
    {
        public List<StageInfoStruct> stageInfoTemplate;
    }
}
