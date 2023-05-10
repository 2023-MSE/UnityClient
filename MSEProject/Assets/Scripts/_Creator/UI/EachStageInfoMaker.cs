using System.Collections;
using System.Collections.Generic;
using _Creator.DungeonInfoFolder;
using DungeonInfoFolder;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using TMPro;
using UnityEngine;

public class EachStageInfoMaker : MonoBehaviour
{
    public TextMeshProUGUI stageInfoNameText;
    public uint assignedInt;
    
    public StageInfoScriptableObject stageInfoScriptableObject;

    public void SetUpEachStageInfoUI(uint inputInt)
    {
        // inputInt 에 따라서 "Scriptable" Object 에 이름 정보를 가져와서 이를 표시해 줄 필요도 있을 것 같음.
        // 혹은 Switch 등을 기반으로 하여 표시.
        
        assignedInt = inputInt;

        foreach (var tStageInfoStruct in stageInfoScriptableObject.stageInfoTemplate)
        {
            if (tStageInfoStruct.thisStageInfoIndex == inputInt)
            {
                stageInfoNameText.text = "" + tStageInfoStruct.stageInfo;

                return;
            }
        }
        
        Debug.LogError("No StageInfo Found");
    }
    
    public void RemoveButtonClicked ()
    {
        StageEditor.Instance.instantiatedStageInfos.Remove(transform);
        StageEditor.Instance.EditingStage.elements.Remove(assignedInt);
        
        StageEditor.Instance.VisualizeStageAllInfo();
        Destroy(gameObject);
    }
}
