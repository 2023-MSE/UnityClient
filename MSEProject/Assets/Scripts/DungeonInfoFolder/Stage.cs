using System;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonInfoFolder
{
    public class Stage
    {
        public ulong nodeID;
        public List<ulong> prevStageID;
        public List<ulong> nextStageID;

        [Serializable]
        public enum StageType
        {
            Boss,
            Monster,
            Totem,
            Relax
        }

        public StageType myStageType;

        public string specificTypeInfo;

        public Stage(ulong inputNodeID)
        {
            nodeID = inputNodeID;
            myStageType = StageType.Relax;
            specificTypeInfo = "Just Default Stage for new Node";

            prevStageID = new List<ulong>();
            nextStageID = new List<ulong>();
        }

        public void PrintStageInfo()
        {
            Debug.Log("My Node ID : " + nodeID + "\n" + prevStageID.Count + "   " + nextStageID.Count);
        }
    }
}
