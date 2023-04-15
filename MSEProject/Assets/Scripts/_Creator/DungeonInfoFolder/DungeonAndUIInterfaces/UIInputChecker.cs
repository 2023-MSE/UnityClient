using System;
using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using TMPro;
using UnityEngine;

public class UIInputChecker : MonoBehaviour
{
    public Transform dungeonEditorWindow;
    public TMP_InputField dungeonNameInput;

    private void Start()
    {
        if (dungeonEditorWindow == null || dungeonNameInput == null)
        {
            Debug.Log("Please check UI Input Checker!!!");
        }
    }

    public void OnNewDungeonMakingDone()
    {
        Dungeon tempDungeon = new Dungeon();
        tempDungeon.name = dungeonNameInput.text;
        tempDungeon.createdTime = DateTime.Now.ToString("HH : mm : ss");
        
        DungeonManager.Instance.MyDungeonList.myDungeons.Add(tempDungeon);
        DungeonEditor.Instance.editingDungeon = tempDungeon;
        
        DungeonUIVisualizer.Instance.VisualizeDungeonList();
    }

    public void OnEditButtonClick()
    {
        FindObjectOfType<StageNodeEditor>().LoadDungeonGraph();
    }

    public void OnDeleteButtonClicked()
    {
        Dungeon tempDungeon = DungeonEditor.Instance.editingDungeon;
        DungeonManager.Instance.MyDungeonList.myDungeons.Remove(tempDungeon);
        DungeonEditor.Instance.editingDungeon = null;
        
        // UI 도 다시 띄워야 함!! 
        DungeonUIVisualizer.Instance.VisualizeDungeonList();
    }
    
    
}
