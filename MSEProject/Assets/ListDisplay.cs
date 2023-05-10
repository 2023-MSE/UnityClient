using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ListDisplay : MonoBehaviour
{
    public GameObject listItemPrefab;
    public VerticalLayoutGroup Layout;
    public List<string> stringList;
    //private ListManager listmanager;
  /*  private void Start()
    {
        listmanager = GetComponent<ListManager>();
        DisplayList();

    }
    
    

    public void DisplayList()
    {
        stringList = listmanager.getList();
        Debug.Log(stringList.Count);
        // 리스트에 저장된 문자열을 순회하며 리스트 아이템을 생성하고 배치합니다.
        foreach (string str in stringList)
        {
            GameObject listItem = Instantiate(listItemPrefab, Layout.transform);
            TextMeshProUGUI textComponent = listItem.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = str;
        }

        // GridLayoutGroup 업데이트를 통해 아이템들을 자동으로 배치합니다.
        Layout.enabled = true;
    }
    
    */
}