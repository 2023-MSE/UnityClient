using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
            GetComponent<Button>().onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            Debug.Log("Select dungeon " + dungeonInfo);
            CombatScene.DungeonManager.Instance.SetDeployedDungeon(dungeonInfo);
            CombatScene.DungeonManager.Instance.SetDeployedDungeon(dungeonInfo);
        }
    }
}
