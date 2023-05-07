using System.Collections;
using System.Collections.Generic;
using _Creator.DungeonInfoFolder;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoButtonListener : MonoBehaviour
{
    public StageInfoScriptableObject stageInfoScriptableObject;
    public StageInfo thisButtonStageInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnStageInfoButtonClicked);
    }

    public void OnStageInfoButtonClicked()
    {
        foreach (var variStageInfoStruct in stageInfoScriptableObject.stageInfoTemplate)
        {
            if (variStageInfoStruct.stageInfo == thisButtonStageInfo)
            {
                StageEditor.Instance.AddElementsToStage(variStageInfoStruct.thisStageInfoIndex);
                return;
            }
        }
    }
}
