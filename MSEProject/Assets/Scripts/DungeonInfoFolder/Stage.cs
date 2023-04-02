namespace DungeonInfoFolder
{
    public class Stage
    {
        private Stage _nextStage;

        public enum StageType
        {
            Boss,
            Monster,
            Totem
        }

        public StageType myStageType;

        public string specificTypeInfo;
    }
}
