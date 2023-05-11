using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using _Player.CombatScene;
using TMPro;

public class ListDisplay : MonoBehaviour
{

    private CombatManager _combatManager;
    public GameObject listItemPrefab;
    public VerticalLayoutGroup Layout;
    public List<String> stringList;
    public GameObject ScrollView;
    public GameObject map;
    private bool check = false;
    private GameObject[] list;


    public void Start()
    {
    
        ScrollView.SetActive(false);
        _combatManager = FindObjectOfType<CombatManager>();
        stringList=_combatManager.sendSkill();
    }

    public void clickmap()
    {
        check = !check;

        if (check)
        {
            DisplayList();
        }
        else
        {
            RemoveList();
        }
    }


    public void DisplayList()
    {
        Debug.Log("list count : " +stringList.Count);
        ScrollView.SetActive(true);
       
        // 리스트에 저장된 문자열을 순회하며 리스트 아이템을 생성하고 배치합니다.
        foreach (String str in stringList)
        {
            Debug.Log("list:"+str);
            GameObject listItem = Instantiate(listItemPrefab, Layout.transform);
            TextMeshProUGUI textComponent = listItem.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = str;
        }

        // GridLayoutGroup 업데이트를 통해 아이템들을 자동으로 배치합니다.
        Layout.enabled = true;
    }

    public void RemoveList()
    {
        list = GameObject.FindGameObjectsWithTag("List");

        foreach (var l in list)
        {
            Destroy(l);
        }
        
        ScrollView.SetActive(false);

    }
    
    
}