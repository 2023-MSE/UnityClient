using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using _Player.CombatScene;
using TMPro;
using UnityEditor.SceneManagement;
using Stage = DungeonInfoFolder.Stage;

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

    private List<int> maplist;

    private int currentIndex = 0;

    private FadeEffect _fadeEffect;

    public void Start()
    {
        maplist = new List<int>();
        itemButtons = new List<GameObject>();
        _fadeEffect = FindObjectOfType<FadeEffect>();
        ScrollView.SetActive(false);

    }

    // 1. 넘어갈 수 있는 map list 호출
    public void DisplayList()
    {

        Debug.Log("list count : " + stringList.Count);

        ScrollView.SetActive(true);


        // 리스트에 저장된 문자열을 순회하며 리스트 아이템을 생성하고 배치
        foreach (var str in _Player.CombatScene.DungeonManager.Instance.GetNextStages())
        {
            GameObject listItem = Instantiate(listItemPrefab, Layout.transform);
            itemButtons.Add(listItem);
            TextMeshProUGUI textComponent = listItem.GetComponentInChildren<TextMeshProUGUI>();

            Stage currentStage = _Player.CombatScene.DungeonManager.Instance.GetDungeon().dStages[str];
            
            if (currentStage.stageType == Stage.StageType.Boss)
            {
                textComponent.text = "BOSS";
                maplist.Add((int)str);
            }
            else if (currentStage.stageType == Stage.StageType.Monster)
            {
                textComponent.text = "MONSTER";
                maplist.Add((int)str);
            }
            else if (currentStage.stageType == Stage.StageType.Totem)
            {
                textComponent.text = "TOTEM";
                maplist.Add((int)str);
            }
            else if (currentStage.stageType == Stage.StageType.Relax)
            {
                textComponent.text = "RELAX";
                maplist.Add((int)str);
            }

        }
        Layout.enabled = true;
        checkList = true;
    }

    private bool nextcheck=false;

    public bool GetNextCheck()
    {
        return nextcheck;
    }
    
    // 2. 사용자가 key 조정하면서 , 넘어갈 씬 조정
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

                StartCoroutine(nextScene());

                nextcheck = true;
                
               _fadeEffect.fadein_map((ulong)maplist[currentIndex]);

            }
        }
    }

    public ulong getCurrentIndex()
    {
        return (ulong)currentIndex;
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2f); // 2초간 대기
        RemoveList();
        yield return null;

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
        itemButtons[currentIndex].GetComponent<Image>().color=Color.magenta;


        if (itemButtons[currentIndex] == null)
        {
            return;
        }
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

