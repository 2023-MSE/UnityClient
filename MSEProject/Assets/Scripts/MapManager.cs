using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using _Player.CombatScene;
using TMPro;

public class MapManager : MonoBehaviour
{
    public GameObject listItemPrefab;
    public VerticalLayoutGroup Layout;
    public List<String> stringList;
    public GameObject ScrollView;
    public GameObject map;
    private bool check = false;
    private GameObject[] list;

    private bool checkList = false;

    private List<GameObject> itemButtons;
    private Color focusColor;

    private int currentIndex = 0;

    public void Start()
    {
        itemButtons = new List<GameObject>();

       // ScrollView.SetActive(false);

    }

    public void DisplayList()
    {

        Debug.Log("list count : " + stringList.Count);

       // ScrollView.SetActive(true);


        // 리스트에 저장된 문자열을 순회하며 리스트 아이템을 생성하고 배치
        foreach (var str in _Player.CombatScene.DungeonManager.instance.GetNextStages())
        {
            Debug.Log("list:" + str);
            Debug.Log("list:"+str);
            GameObject listItem = Instantiate(listItemPrefab, Layout.transform);
            itemButtons.Add(listItem);
            TextMeshProUGUI textComponent = listItem.GetComponentInChildren<TextMeshProUGUI>();
           
            if ((int)str == 0)
            {
                textComponent.text = "MONSTER";
            }
            else if ((int)str == 1)
            {
                textComponent.text = "MONSTER";
            }
            else if ((int)str == 2)
            {
                textComponent.text = "TOTEM";
            }
            else if ((int)str == 3)
            {
                textComponent.text = "RELAX";
            }
            else if ((int)str == 4)
            {
                textComponent.text = "BOSS";
            }

        }
    }
}
