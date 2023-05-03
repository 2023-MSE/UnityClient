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
    
    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            DamageHp(0.1f);
        }
    }

    public void DamageHp(float hp)
    {
        cooldown.fillAmount -= hp;
    }
}
