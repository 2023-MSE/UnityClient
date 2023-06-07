using UnityEngine;
using RuntimeNodeEditor;
using RuntimeNodeEditor.Examples;

public class DungeonCreatorApplicationStartup : MonoBehaviour
{
    public RectTransform      editorHolder;
    public StageNodeEditor    editor;
    
    public GameObject         ctxMenuContainer;

    private void Start()
    {
        Debug.Log("Start and Create Editor");
        var graph = editor.CreateGraph<NodeGraph>(editorHolder);
        editor.StartEditor(graph);
        
        GameObject.Find("NodeGraph").transform.localScale = Vector3.one;

        Transform tempTransformForCtxMenu = GameObject.Find("CtxMenuContainer").transform;
        tempTransformForCtxMenu.localScale = Vector3.one * 2.5f;
        tempTransformForCtxMenu.parent = tempTransformForCtxMenu.parent.parent.parent;
        
        if (ctxMenuContainer != null)
            ctxMenuContainer.SetActive(false);
    }
}