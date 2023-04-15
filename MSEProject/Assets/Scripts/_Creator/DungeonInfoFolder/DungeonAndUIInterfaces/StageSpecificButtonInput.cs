using System;
using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using UnityEngine;
using UnityEngine.UI;

public class StageSpecificButtonInput : MonoBehaviour
{
    public uint thisButtonData = 0;
    
    /// <summary>
    /// 자신이 가지고 있는 Button 에 StageEditor 에 해당하는 동작을 넣어주는(AddListener) 형태.
    /// 자신이 가지고 있는 정보를 해당 형태로 보내줌.
    /// </summary>
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendMessageToStage);
    }

    private void SendMessageToStage()
    {
        StageEditor.Instance.AddElementsToStage(thisButtonData);
    }
}
