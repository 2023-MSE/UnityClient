using System.Collections.Generic;

namespace DungeonInfoFolder
{
    public class Stage
    {
        public long nodeID;
        public List<long> nextStageID;

        public enum StageType
        {
            Boss,
            Monster,
            Totem,
            Relax
        }

        public StageType myStageType;

        public string specificTypeInfo;

        public Stage(long inputNodeID)
        {
            nodeID = inputNodeID;
            myStageType = StageType.Relax;
            specificTypeInfo = "Just Default Stage for new Node";
        }
    }
}
