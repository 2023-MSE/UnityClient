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
        stage0.nextStageID.Add(1);
        stage0.nextStageID.Add(4);
        stage0.elements.Add(2);
        stage0.elements.Add(2);
        stage0.elements.Add(2);
        stage0.myStageType = DungeonInfoFolder.Stage.StageType.Monster;

        DungeonInfoFolder.Stage stage1 = new DungeonInfoFolder.Stage(1);
        stage1.nextStageID.Add(2);
        stage1.nextStageID.Add(3);
        stage1.elements.Add(3);
        stage1.elements.Add(4);
        stage1.elements.Add(5);
        stage1.myStageType = DungeonInfoFolder.Stage.StageType.Monster;

        DungeonInfoFolder.Stage stage2 = new DungeonInfoFolder.Stage(2);
        stage2.nextStageID.Add(3);
        stage2.elements.Add(30);
        stage2.myStageType = DungeonInfoFolder.Stage.StageType.Totem;

        DungeonInfoFolder.Stage stage3 = new DungeonInfoFolder.Stage(3);
        
      
        stage3.nextStageID.Add(3);
        stage3.nextStageID.Add(4);
      
        
        stage3.elements.Add(1);
        stage3.myStageType = DungeonInfoFolder.Stage.StageType.Relax;

        DungeonInfoFolder.Stage stage4 = new DungeonInfoFolder.Stage(4);
        //stage4.nextStageID.Add(5);
        stage4.elements.Add(4);
        stage4.elements.Add(6);
        stage4.elements.Add(4);
        stage4.myStageType = DungeonInfoFolder.Stage.StageType.Boss;

        dungeon.dStages.Add(0, stage0);
        dungeon.dStages.Add(1, stage1);
        dungeon.dStages.Add(2, stage2);
        dungeon.dStages.Add(3, stage3);
        dungeon.dStages.Add(4, stage4);

        _Player.CombatScene.DungeonManager.instance.SetDungeon(dungeon);
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(0);
    }
    public void OnClickConvertBuffButton()
    {
        DungeonInfoFolder.Dungeon dungeon = new DungeonInfoFolder.Dungeon();
        /*
         * Test Case Monster
         * **/

        DungeonInfoFolder.Stage stage = new DungeonInfoFolder.Stage(0);
        stage.nextStageID.Add(1);
        stage.nextStageID.Add(2);
        stage.elements.Add(5);
        stage.myStageType = DungeonInfoFolder.Stage.StageType.Totem;
        dungeon.dStages.Add(0, stage);
        for (ulong i = 1; i < 11; i++)
            dungeon.dStages.Add(i, new DungeonInfoFolder.Stage(i));

        _Player.CombatScene.DungeonManager.instance.SetDungeon(dungeon);
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(0);
    }
    public void OnClickConvertRelaxButton()
    {
        DungeonInfoFolder.Dungeon dungeon = new DungeonInfoFolder.Dungeon();
        /*
         * Test Case Monster
         * **/

        DungeonInfoFolder.Stage stage = new DungeonInfoFolder.Stage(0);
        stage.nextStageID.Add(1);
        stage.nextStageID.Add(2);
        stage.myStageType = DungeonInfoFolder.Stage.StageType.Relax;
        dungeon.dStages.Add(0, stage);
        for (ulong i = 1; i < 11; i++)
            dungeon.dStages.Add(i, new DungeonInfoFolder.Stage(i));

        _Player.CombatScene.DungeonManager.instance.SetDungeon(dungeon);
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(0);
    }
}
