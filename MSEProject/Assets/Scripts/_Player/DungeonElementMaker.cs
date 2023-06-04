using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _Player
{
    public class DungeonElementMaker : MonoBehaviour
    {
        public TextMeshProUGUI dungeonNameText;
        public TextMeshProUGUI dungeonIDText;
        public DeployedDungeon dungeonInfo;

        public void SetUpEachDungeonUI(DeployedDungeon dungeon)
        {
            dungeonNameText.text = dungeon.name;
            dungeonIDText.text = "#" +dungeon.id;
            dungeonInfo = dungeon;
        }
    }
}
