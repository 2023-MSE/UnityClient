using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using _Player.CombatScene;
using TMPro;

public class RelaxMapList : MonoBehaviour
{

  
    public GameObject listItemPrefab;
    public VerticalLayoutGroup Layout;
    public List<String> stringList;
    public GameObject ScrollView;
    public GameObject map;
    private bool check = false;
    private GameObject[] list;

    private bool checkList = false;

    private RelaxManager _relaxManager;

    private List<GameObject> itemButtons;
    private Color focusColor;

    private int currentIndex = 0;

    public void Start()
    {
        itemButtons = new List<GameObject>();
        ScrollView.SetActive(false);
        _relaxManager = FindObjectOfType<RelaxManager>();

    }

  

    public void clickmap()
    {
        check = !check;

        if (check)
        {
            //Time.timeScale = 0;
            DisplayList();
        }
        else
        {
            Time.timeScale = 1;
            RemoveList();
            checkList = false;
            
        }
    }

    public bool check_note()
    {
        return checkList;
    }


    public void DisplayList()
    {
        
        Debug.Log("list count : " +stringList.Count);
        
        ScrollView.SetActive(true);

        // 리스트에 저장된 문자열을 순회하며 리스트 아이템을 생성하고 배치
        foreach (var str in _Player.CombatScene.DungeonManager.instance.GetNextStages())
        {
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

        // GridLayoutGroup 업데이트를 통해 아이템들을 자동으로 배치
        Layout.enabled = true;
        checkList = true;
    }

    public void FixedUpdate()
    {
        if (checkList)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveSelection(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveSelection(1);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("enter!!"+_Player.CombatScene.DungeonManager.Instance.GetCurrentStage() + currentIndex);
                
                
                _Player.CombatScene.DungeonManager.instance.GoNextStage(
                   // _Player.CombatScene.DungeonManager.Instance.GetCurrentStage() + _Player.CombatScene.DungeonManager.Instance.GetNextStages()[currentIndex]
                     (ulong)currentIndex
                    );
                
                _relaxManager.RelaxSceneOff();

            }
        }
    }


    public void PlayInList()
    {
        // 방향키 입력을 감지하여 아이템 선택 및 focus 변경
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelection(1);
        }
        
    }
    private void MoveSelection(int direction)
    {
        // 이전 아이템 focus 해제
        Debug.Log(currentIndex);
        for (int i = 0; i < itemButtons.Count; i++)
        {
            itemButtons[currentIndex].GetComponent<Image>().color = Color.white;
        }
        //itemButtons[currentIndex].GetComponent<Image>().color = Color.yellow;
        // 아이템 인덱스 업데이트
        currentIndex += direction;
        Debug.Log("list :: white"+currentIndex);
        // 인덱스 범위 체크
        if (currentIndex < 0)
        {
            currentIndex = itemButtons.Count - 1;
        }
        else if (currentIndex >= itemButtons.Count)
        {
            currentIndex = 0;
        }

        // 현재 아이템 focus 설정 및 시각적인 효과 적용
        SelectItem(currentIndex);
        
        
    }
    private void SelectItem(int index)
    {
        // 현재 아이템 focus 설정
        //itemButtons[index].GetComponent<Button>().Select();

      
        itemButtons[currentIndex].GetComponent<Image>().color=Color.magenta;
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