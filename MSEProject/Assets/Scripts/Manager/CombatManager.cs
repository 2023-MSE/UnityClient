using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private int singleTargetIndex = 0;
    private List<GameObject> monsters = new List<GameObject>();
    private GameObject player;
    private float attackMulti = 1.0f;
    private int currentSkill = 0;
    private int lastSkill = 0;
    void Start()
    {
        // 초기 설정
        player = GameObject.FindObjectOfType<Player>().gameObject;
        player.GetComponent<Player>().setHp(100);
        Monster[] monster = GameObject.FindObjectsOfType<Monster>();
        monsters.Add(monster[0].gameObject);
        monster[0].GetComponent<Monster>().setHp(100);
        monster[0].GetComponent<Monster>().setPower(10);
        monsters.Add(monster[1].gameObject);
        monster[1].GetComponent<Monster>().setHp(100);
        monster[1].GetComponent<Monster>().setPower(20);
        monsters.Add(monster[1].gameObject);
        monster[2].GetComponent<Monster>().setHp(100);
        monster[2].GetComponent<Monster>().setPower(30);
    }
    private void damage(GameObject target, float damage)
    {
        // singleTargetIndex 재설정 필요
        target.GetComponent<Character>().setHp(-damage);
    }

    public void skillActivation()
    {
        Skill skill = GameData.skills[lastSkill];
        if (skill.getSplash())
        {
            // 광역 스킬인 경우
            foreach (GameObject monster in monsters)
            {
                float typeMulti = (((skill.getType() + monster.GetComponent<Monster>().getType()) % 3) - 1) / 2;
                float skillDamage = skill.getDamage() * (1f + typeMulti);
                damage(monster, skillDamage);
            }
        }
        else
        {
            // 단일 스킬인 경우
            float typeMulti = (((skill.getType() + monsters[singleTargetIndex].GetComponent<Monster>().getType()) % 3) - 1) / 2;
            float skillDamage = skill.getDamage() * (1f + typeMulti);
            damage(monsters[singleTargetIndex], skillDamage * attackMulti);
        }
        currentSkill = lastSkill = 0;
    }

    public void updateSkill(Direction direction)
    {
        currentSkill = GameData.skills[currentSkill].getNextSkill(direction);
        if (currentSkill == -1)
        {
            // 해당 방향키의 스킬이 존재하지 않는 경우
            skillActivation();
        }
        else
        {
            // 해당 방향키의 스킬이 존재하는 경우
            if (GameData.skills[currentSkill].getEnable())
            {
                lastSkill = currentSkill;
            }
        }
    }

    public void monsterAttack(int monsterIndex)
    {
        int power = monsters[monsterIndex].GetComponent<Monster>().getPower();
        damage(player, power * attackMulti);
    }
}
