using System;
using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageEditor : Singleton<StageEditor>
{
    [Header("Stage Information UI Visualization")]
    public TextMeshProUGUI NodeTypeTMP;
    public TextMeshProUGUI NodeInfoTMP;
    
    
    private Stage editingStage;
    public Stage EditingStage {
        get => editingStage;
        set
        {
            // UI 표시
            editingStage = value;

            NodeTypeTMP.text = "Node Type : " + editingStage.myStageType;
            NodeInfoTMP.text = "Node Information : " + editingStage.specificTypeInfo;
        }
    }

    
    [Serializable]
    public struct stageButtonCollectionInfo
    {
        public Transform stageButtonCollection;
        public Stage.StageType thisCollectionStageType;
    }
    [Space(10)]
    [Header("Stage Type Changable Information")]
    public List<stageButtonCollectionInfo> stageButtonCollectionInfos = new List<stageButtonCollectionInfo>();

    private void Start()
    {
        /*foreach (var stageButtonCollectionInfo in stageButtonCollectionInfos)
        {
            Transform tempTransform = stageButtonCollectionInfo.stageButtonCollection;

            for (int i = 0; i < tempTransform.childCount; i++)
            {
                Button tempButton = tempTransform.GetChild(i).GetComponent<Button>();
                tempButton.onClick.AddListener();
            }
            
            if (stageButtonCollectionInfo.thisCollectionStageType == toChangStageType)
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(true);
            else
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(false);
        }*/
    }

    public void OnClickChangeStageTypeButton(int inputForEnum)
    {
        Stage.StageType toChangStageType = (Stage.StageType)inputForEnum;
        
        if (editingStage == null)
            return;

        editingStage.myStageType = toChangStageType;

        foreach (var stageButtonCollectionInfo in stageButtonCollectionInfos)
        {
            if (stageButtonCollectionInfo.thisCollectionStageType == toChangStageType)
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(true);
            else
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(false);
        }
    }
    
}
