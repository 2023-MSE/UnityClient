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
        // �ʱ� ����
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
        // singleTargetIndex �缳�� �ʿ�
        target.GetComponent<Character>().setHp(-damage);
    }

    public void skillActivation()
    {
        Skill skill = GameData.skills[lastSkill];
        if (skill.getSplash())
        {
            // ���� ��ų�� ���
            foreach (GameObject monster in monsters)
            {
                float typeMulti = (((skill.getType() + monster.GetComponent<Monster>().getType()) % 3) - 1) / 2;
                float skillDamage = skill.getDamage() * (1f + typeMulti);
                damage(monster, skillDamage);
            }
        }
        else
        {
            // ���� ��ų�� ���
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
            // �ش� ����Ű�� ��ų�� �������� �ʴ� ���
            skillActivation();
        }
        else
        {
            // �ش� ����Ű�� ��ų�� �����ϴ� ���
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
