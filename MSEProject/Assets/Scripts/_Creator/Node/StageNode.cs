using System.Collections;
using System.Collections.Generic;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using RuntimeNodeEditor;
using UnityEngine;
using UnityEngine.UI;

public class StageNode : Node
{
    public SocketOutput outputSocket;   //  added from editor
    public SocketInput inputSocket;     //  added from editor

    public Button editButton;
    public Transform indicator;
    
    public override void Setup()
    {
        Register(outputSocket);
        Register(inputSocket);
        SetHeader("Stage");

        StageEditor.Instance.EditingStage = DungeonEditor.Instance.editingDungeon.stages[IdentifierID];
        editButton.onClick.AddListener(OnEditButtonClick);
    }

    private void OnEditButtonClick()
    {
        FindObjectOfType<ActivateIndicator>().OtherTargetActivated(indicator);
        StageEditor.Instance.EditingStage = DungeonEditor.Instance.editingDungeon.stages[IdentifierID];
        StageEditor.Instance.VisualizeStageType();
        StageEditor.Instance.VisualizeStageSpecificInfo();
        
        MusicDungeonInterface.Instance.LoadMusicToAudioSource();
    }

    public override void OnSerialize(Serializer serializer)
    {
        // ex - serializer.Add("floatValue", valueField.text);
    }
    
    public override void OnDeserialize(Serializer serializer)
    {
    }
}
