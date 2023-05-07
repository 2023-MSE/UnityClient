using System;
using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageEditor : Singleton<StageEditor>
{
    // 아예 다시 짜기.
    
    // 0. Default Editing Stage
    private Stage _editingStage;
    public Stage EditingStage {
        get => _editingStage;
        set
        {
            _editingStage = value;
            
            VisualizeStageType();
            VisualizeStageSpecificInfo();
        }
    }
    
    
    
    // 1. Stage Type
    
    // 1-0. Stage Type Setting
    
    [Space(10)]
    [Header("Stage Type")]
    
    public TextMeshProUGUI NodeTypeTMP;
    
    [Serializable]
    public struct stageButtonCollectionInfo
    {
        public Transform stageButtonCollection;
        public Stage.StageType thisCollectionStageType;
    }
    
    public List<stageButtonCollectionInfo> stageButtonCollectionInfos = new List<stageButtonCollectionInfo>();
    
        // 23.05.03 Limitation Visualizer
    public TextMeshProUGUI limitForElementsTMP;
    
    // 1-1. Stage Type Visualization
    
    public void VisualizeStageType()
    {
        NodeTypeTMP.text = "Stage Type : " + _editingStage.myStageType;
        limitForElementsTMP.text = "<color=green>"+ _editingStage.elements.Count + " / " + _editingStage.limitForElements + "</color>";
    }
    
    // 1-2. Stage Type Editor
    public void OnClickChangeStageTypeButton(int inputForEnum)
    {
        Stage.StageType toChangStageType = (Stage.StageType)inputForEnum;
        
        if (_editingStage == null)
            return;

        // stage 의 type 변경
        _editingStage.myStageType = toChangStageType;
        _editingStage.elements.Clear();

        switch (_editingStage.myStageType)
        {
            case Stage.StageType.Boss:
            case Stage.StageType.Totem:
            case Stage.StageType.Relax:
                _editingStage.limitForElements = 1;
                break;
            case Stage.StageType.Monster:
                _editingStage.limitForElements = 3;
                break;
        }

        // stage type 변경에 따른 UI 처리
        foreach (var stageButtonCollectionInfo in stageButtonCollectionInfos)
        {
            if (stageButtonCollectionInfo.thisCollectionStageType == toChangStageType)
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(true);
            else
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(false);
        }
        
        VisualizeStageType();
        VisualizeStageSpecificInfo();
    }



    // 2. Stage Specific Info
    
    // 2-0. Stage Info Setting
    
    [Space(10)]
    [Header("Space Info")]
    public Transform stageInfoElementPrefab;
    
    public Transform stageInfoCollector;
    public List<Transform> instantiatedStageInfos = new List<Transform>();
    
    // 2-1. Stage Specific Info Visualization

    public void VisualizeStageSpecificInfo()
    {
        if (instantiatedStageInfos.Count > 0)
        {
            foreach (var variableStage in instantiatedStageInfos)
            {
                Destroy(variableStage.gameObject);
            }

            instantiatedStageInfos = new List<Transform>();
        }

        foreach (var stageElement in _editingStage.elements)
        {
            Transform tempStageInfo = Instantiate(stageInfoElementPrefab, stageInfoCollector);
            instantiatedStageInfos.Add(tempStageInfo);

            if (tempStageInfo.GetComponent<EachStageInfoMaker>() != null)
            {
                tempStageInfo.GetComponent<EachStageInfoMaker>().SetUpEachStageInfoUI(stageElement);
            }
        }
    }
    
    // 2-2. Stage Specific Info Editor
    public void AddElementsToStage(uint inputElements)
    {
        // Stage part
        if (_editingStage.limitForElements <= _editingStage.elements.Count)
        {
            Debug.Log("already Too much!!");
            return;
        }
        
        _editingStage.elements.Add(inputElements);
        
        VisualizeStageType();
        VisualizeStageSpecificInfo();
        
        limitForElementsTMP.text = "<color=red>"+ _editingStage.elements.Count + " / " + _editingStage.limitForElements + "</color>";
    }

    // public void DeleteElementsToStage(uint inputElements) {} : Delete 는 각 Stage Button의 EachStageInfoMaker 내에 존재함.
    
    // A. Show Now editing Stage Info
    public void ShowNowEditingStageInfo()
    {
        _editingStage.ShowAllStageInformation();
    }
}
