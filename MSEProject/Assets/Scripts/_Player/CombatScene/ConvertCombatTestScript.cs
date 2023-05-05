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

        DungeonInfoFolder.Stage stage = new DungeonInfoFolder.Stage(0);
        stage.nextStageID.Add(1);
        stage.nextStageID.Add(2);
        stage.elements.Add(5);
        stage.elements.Add(6);
        stage.elements.Add(4);
        stage.myStageType = DungeonInfoFolder.Stage.StageType.Monster;
        dungeon.stages.Add(0, stage);
        for (ulong i = 1; i < 11; i++)
            dungeon.stages.Add(i, new DungeonInfoFolder.Stage(i));

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
        dungeon.stages.Add(0, stage);
        for (ulong i = 1; i < 11; i++)
            dungeon.stages.Add(i, new DungeonInfoFolder.Stage(i));

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
        dungeon.stages.Add(0, stage);
        for (ulong i = 1; i < 11; i++)
            dungeon.stages.Add(i, new DungeonInfoFolder.Stage(i));

        _Player.CombatScene.DungeonManager.instance.SetDungeon(dungeon);
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(0);
    }
}
