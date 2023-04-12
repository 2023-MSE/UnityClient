using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using TMPro;
using UnityEngine;

public class EachDungeonMaker : MonoBehaviour
{
    public TextMeshProUGUI dungeonNameText;
    public Dungeon assignedDungeon;
    public Transform indicator;

    public void SetUpEachDungeonUI(string inputDungeonName, string inputCreatedTime, Dungeon inputDungeon)
    {
        dungeonNameText.text = inputDungeonName;
        assignedDungeon = inputDungeon;
    }

    public void ToggleChecked(bool inputToggleStatus)
    {
        if (inputToggleStatus)
            DungeonEditor.Instance.editingDungeon = assignedDungeon;
        
        FindObjectOfType<ActivateIndicator>().OtherTargetActivated(indicator);
    }
}
