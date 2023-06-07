using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CoolDown : MonoBehaviour
{
    public Image cooldown;

    public bool coolingDown;

    public float waitTime = 30.0f;

    private float hp;

    public static CoolDown instance;
    
    public static CoolDown Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        Debug.Log("notion : CoolDown setactive"+_Player.CombatScene.DungeonManager.instance.GetPlayerHP());
        setHp(_Player.CombatScene.DungeonManager.instance.GetPlayerHP()*0.001f);
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            DamageHp(0.1f);
        }
    }

    public Image getCoolDown()
    {
        return cooldown;
    }

    public void DamageHp(float hp)
    {
        cooldown.fillAmount -= hp;
    }

    public void RelaxHp(float hp)
    {

        cooldown.fillAmount += 0.1f;

    }

    public void setHp(float hp)
    {
        cooldown.fillAmount = hp;
    }


    public void DamageToZero()
    {
        cooldown.fillAmount = 0;
    }

    public void DamageToFull()
    {
        cooldown.fillAmount = 1;
    }

    public void getFillAmount()
    {
        Debug.Log("fill: " + cooldown.fillAmount);
    }
}