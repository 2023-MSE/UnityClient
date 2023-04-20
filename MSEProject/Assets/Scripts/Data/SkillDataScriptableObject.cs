using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataScriptableObject", menuName = "Scriptable Object/SkillDataTemplate", order = int.MaxValue)]
public class SkillDataScriptableObject : ScriptableObject
{
    public List<Skill> skills;
}
