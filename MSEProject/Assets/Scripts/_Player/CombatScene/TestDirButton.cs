using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDirButton : MonoBehaviour
{
    _Player.CombatScene.CombatManager combatManager = null;
    public void OnClickButtonUp()
    {
        if (combatManager == null)
        {
            combatManager = GameObject.Find("CombatManager").GetComponent< _Player.CombatScene.CombatManager>();
        }
        combatManager.updateSkill(Direction.UP);
    }
    public void OnClickButtonDown()
    {
        if (combatManager == null)
        {
            combatManager = GameObject.Find("CombatManager").GetComponent<_Player.CombatScene.CombatManager>();
        }
        combatManager.updateSkill(Direction.DOWN);
    }
    public void OnClickButtonLeft()
    {
        if (combatManager == null)
        {
            combatManager = GameObject.Find("CombatManager").GetComponent<_Player.CombatScene.CombatManager>();
        }
        combatManager.updateSkill(Direction.LEFT);
    }
    public void OnClickButtonRight()
    {
        if (combatManager == null)
        {
            combatManager = GameObject.Find("CombatManager").GetComponent<_Player.CombatScene.CombatManager>();
        }
        combatManager.updateSkill(Direction.RIGHT);
    }

}
