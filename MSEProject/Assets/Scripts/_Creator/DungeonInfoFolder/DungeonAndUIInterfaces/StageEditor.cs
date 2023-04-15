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
    
    // 1-1. Stage Type Visualization
    
    public void VisualizeStageType()
    {
        NodeTypeTMP.text = "Stage Type : " + _editingStage.myStageType;
    }
    
    // 1-2. Stage Type Editor
    public void OnClickChangeStageTypeButton(int inputForEnum)
    {
        Stage.StageType toChangStageType = (Stage.StageType)inputForEnum;
        
        if (_editingStage == null)
            return;

        _editingStage.myStageType = toChangStageType;

        foreach (var stageButtonCollectionInfo in stageButtonCollectionInfos)
        {
            if (stageButtonCollectionInfo.thisCollectionStageType == toChangStageType)
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(true);
            else
                stageButtonCollectionInfo.stageButtonCollection.gameObject.SetActive(false);
        }
        
        VisualizeStageType();
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
        
        VisualizeStageSpecificInfo();
    }

    // public void DeleteElementsToStage(uint inputElements) {} : Delete 는 각 Stage Button의 EachStageInfoMaker 내에 존재함.
    

}
