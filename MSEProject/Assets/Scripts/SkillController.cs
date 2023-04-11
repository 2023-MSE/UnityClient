using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject Green_3;
    [SerializeField] private GameObject Blue_5;
    [SerializeField] private GameObject Red_7;

    private PlayerController player;

    private SkillManager _skillManager;

    void Start()
    {
      
        Off_Box();
    }
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        _skillManager = FindObjectOfType<SkillManager>();
    }
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckGreen_3();
        CheckBlue_5();
        CheckRed_7();
    }

    void Off_Box()
    {
        Green_3.SetActive(false);
        Blue_5.SetActive(false);
        Red_7.SetActive(false);

    }

    public void CheckGreen_3()
    {
        if (player.getHP() >= 2)
        {
            // 초록 공격 가능해
            Green_3.SetActive(true);
            _skillManager.Skill_Input();
            
        }
    }
    public void CheckBlue_5()
    {
        if (player.getHP() >= 5)
        {
            // 파랑 공격 가능해짐
            Blue_5.SetActive(true);
            _skillManager.Skill_Input();
        }
    }
    public void CheckRed_7()
    {
        if (player.getHP()  >= 10)
        {
            // 빨강 공격 가능해짐
            Red_7.SetActive(true);
            _skillManager.Skill_Input();
        }
    }
}
