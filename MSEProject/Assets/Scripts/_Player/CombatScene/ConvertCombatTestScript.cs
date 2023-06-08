using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertCombatTestScript : MonoBehaviour
{
    public void OnClickConvertMonsterButton()
    {
        DungeonInfoFolder.Dungeon dungeon = new DungeonInfoFolder.Dungeon();
        /*
         * Test Case Monster
         * **/

        DungeonInfoFolder.Stage stage0 = new DungeonInfoFolder.Stage(0);
        stage0.nextStage.Add(1);
        stage0.elements.Add(2);
        stage0.elements.Add(2);
        stage0.elements.Add(2);
        stage0.stageType = DungeonInfoFolder.Stage.StageType.Monster;

        DungeonInfoFolder.Stage stage1 = new DungeonInfoFolder.Stage(1);
        stage1.nextStage.Add(2);
        stage1.elements.Add(3);
        stage1.elements.Add(4);
        stage1.elements.Add(5);
        stage1.stageType = DungeonInfoFolder.Stage.StageType.Monster;

        DungeonInfoFolder.Stage stage2 = new DungeonInfoFolder.Stage(2);
        stage2.nextStage.Add(3);
        stage2.elements.Add(30);
        stage2.stageType = DungeonInfoFolder.Stage.StageType.Totem;

        DungeonInfoFolder.Stage stage3 = new DungeonInfoFolder.Stage(3);
        
        stage3.nextStage.Add(0);
        stage3.nextStage.Add(1);
        stage3.nextStage.Add(2);
        stage3.nextStage.Add(3);
        stage3.nextStage.Add(4);
      
        
        stage3.elements.Add(1);
        stage3.stageType = DungeonInfoFolder.Stage.StageType.Relax;

        DungeonInfoFolder.Stage stage4 = new DungeonInfoFolder.Stage(4);
        stage4.nextStage.Add(5);
        stage4.elements.Add(4);
        stage4.elements.Add(6);
        stage4.elements.Add(4);
        stage4.stageType = DungeonInfoFolder.Stage.StageType.Boss;

        dungeon.dStages.Add(0, stage0);
        dungeon.dStages.Add(1, stage1);
        dungeon.dStages.Add(2, stage2);
        dungeon.dStages.Add(3, stage3);
        dungeon.dStages.Add(4, stage4);

        _Player.CombatScene.DungeonManager.Instance.SetDungeon(dungeon);
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(0);
    }
    public void OnClickConvertBuffButton()
    {
        DungeonInfoFolder.Dungeon dungeon = new DungeonInfoFolder.Dungeon();
        /*
         * Test Case Monster
         * **/

        DungeonInfoFolder.Stage stage = new DungeonInfoFolder.Stage(0);
        stage.nextStage.Add(1);
        stage.nextStage.Add(2);
        stage.elements.Add(5);
        stage.stageType = DungeonInfoFolder.Stage.StageType.Totem;
        dungeon.dStages.Add(0, stage);
        for (ulong i = 1; i < 11; i++)
            dungeon.dStages.Add(i, new DungeonInfoFolder.Stage(i));

        _Player.CombatScene.DungeonManager.Instance.SetDungeon(dungeon);
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(0);
    }
    public void OnClickConvertRelaxButton()
    {
        DungeonInfoFolder.Dungeon dungeon = new DungeonInfoFolder.Dungeon();
        /*
         * Test Case Monster
         * **/

        DungeonInfoFolder.Stage stage = new DungeonInfoFolder.Stage(0);
        stage.nextStage.Add(1);
        stage.nextStage.Add(2);
        stage.stageType = DungeonInfoFolder.Stage.StageType.Relax;
        dungeon.dStages.Add(0, stage);
        for (ulong i = 1; i < 11; i++)
            dungeon.dStages.Add(i, new DungeonInfoFolder.Stage(i));

        _Player.CombatScene.DungeonManager.Instance.SetDungeon(dungeon);
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(0);
    }
}
