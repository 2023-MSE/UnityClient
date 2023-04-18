using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using TMPro;
using UnityEngine;

public class EachStageInfoMaker : MonoBehaviour
{
    public TextMeshProUGUI stageInfoNameText;
    public uint assignedInt;

    public void SetUpEachStageInfoUI(uint inputInt)
    {
        // inputInt 에 따라서 "Scriptable" Object 에 이름 정보를 가져와서 이를 표시해 줄 필요도 있을 것 같음.
        // 혹은 Switch 등을 기반으로 하여 표시.
        
        assignedInt = inputInt;
        stageInfoNameText.text = "" + inputInt;
    }
    
    public void RemoveButtonClicked ()
    {
        StageEditor.Instance.instantiatedStageInfos.Remove(transform);
        StageEditor.Instance.EditingStage.elements.Remove(assignedInt);
        Destroy(gameObject);
    }
}
