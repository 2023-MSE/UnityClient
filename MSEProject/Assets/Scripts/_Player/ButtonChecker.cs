using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Player
{
    public class ButtonChecker : MonoBehaviour
    {
        public void OnClickPlayButton()
        {
            NetworkManager.Instance.GetSelectDungeonStart(CombatScene.DungeonManager.Instance.GetDeployedDungeon());
        }
    }
}
