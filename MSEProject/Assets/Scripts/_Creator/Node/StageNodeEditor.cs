using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonInfoFolder;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using RuntimeNodeEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageNodeEditor : NodeEditor
{
    private string _savePath;

    public enum StageNodeEditorSaveType
    {
        Local,
        ToDungeon_Server
    }

    public StageNodeEditorSaveType thisEditorSaveType;

    public override void StartEditor(NodeGraph graph)
    {
        base.StartEditor(graph);

        _savePath = Application.dataPath + "/Resources/graph.json";

        Events.OnGraphPointerClickEvent += OnGraphPointerClick;
        Events.OnNodePointerClickEvent += OnNodePointerClick;
        Events.OnConnectionPointerClickEvent += OnNodeConnectionPointerClick;
        Events.OnSocketConnect += OnConnect;

        if (Graph == null)
            Debug.Log("Graph is null in startEditor");
        Debug.Log(gameObject.name);

        Graph.SetSize(Vector2.one * 20000);
    }

    private void OnConnect(SocketInput arg1, SocketOutput arg2)
    {
        Graph.drawer.SetConnectionColor(arg2.connection.connId, Color.green);

        // Socket 에 연결되었을 때, OwnerNode 에 접근할 수 있음.
        // 또한 StageNodeEditor 라는 상속된 Class 이므로, 이것만 따로 사용한다 해도 문제가 없음. (Interface 역할)
        // arg1 이 output으로 연결되어 지는 node, arg2 가 input으로 나오는 node 로 보임.
        DungeonEditor.Instance.editingDungeon.stages[arg1.OwnerNode.IdentifierID].prevStageID
            .Add(arg2.OwnerNode.IdentifierID);
        DungeonEditor.Instance.editingDungeon.stages[arg2.OwnerNode.IdentifierID].nextStageID
            .Add(arg1.OwnerNode.IdentifierID);

        // Test for Stage Info
        // prev node in connection
        DungeonEditor.Instance.editingDungeon.stages[arg2.OwnerNode.IdentifierID].PrintStageInfo();
        // next node in connection
        DungeonEditor.Instance.editingDungeon.stages[arg1.OwnerNode.IdentifierID].PrintStageInfo();
    }

    private void OnGraphPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Right:
            {
                var ctx = new ContextMenuBuilder()
                    .Add("nodes/stage", CreateStageNode)
                    .Add("graph/load", () => LoadDungeonGraph())
                    .Add("graph/save", () => SaveDungeonGraph())
                    .Build();

                SetContextMenu(ctx);
                DisplayContextMenu();
            }
                break;
            case PointerEventData.InputButton.Left:
                CloseContextMenu();
                break;
        }
    }

    // context item actions
    private void CreateStageNode()
    {
        // stage 추가 - 해당 스테이지가 node 에 들어가야 함. - 아 아니네. ID 만 들고 있는 게 좋겠구나. Monobehavior 로 처리되지 않으니까.
        Dungeon tempEditingDungeon = DungeonEditor.Instance.editingDungeon;
        ulong tempRecentID = tempEditingDungeon.recentID;

        Stage tempNewStage = new Stage(tempRecentID);
        tempNewStage.myStageType = Stage.StageType.Relax;
        tempNewStage.specificTypeInfo = "";

        tempEditingDungeon.stages.Add(tempRecentID, tempNewStage);
        tempEditingDungeon.recentID += 1;

        Graph.Create("Nodes/StageNode").IdentifierID = tempRecentID;
        CloseContextMenu();
    }

    public void SaveDungeonGraph()
    {
        switch (thisEditorSaveType)
        {
            /*case StageNodeEditorSaveType.Local:
            {
                string tempSavePath = Application.dataPath + "/Resources/" + DungeonEditor.Instance.editingDungeon.id +
                                      "graph.json";

                CloseContextMenu();
                Graph.SaveFile(tempSavePath);
                break;
            }*/

            case StageNodeEditorSaveType.ToDungeon_Server:
            {
                Debug.Log("Save Start");
                DungeonEditor.Instance.editingDungeon.nodeEditorJsonData = Graph.ExportJson();
                CloseContextMenu();

                Debug.Log("Save Done");
                break;
            }
        }
    }

    private void SaveGraph(string savePath)
    {
        CloseContextMenu();
        Graph.SaveFile(savePath);
    }

    public void LoadDungeonGraph()
    {
        switch (thisEditorSaveType)
        {
            /*case StageNodeEditorSaveType.Local:
            {
                string tempLoadPath = Application.dataPath + "/Resources/" + DungeonEditor.Instance.editingDungeon.id +
                                      "graph.json";

                CloseContextMenu();
                Graph.Clear();
                Graph.LoadFile(tempLoadPath);
                break;
            }*/

            case StageNodeEditorSaveType.ToDungeon_Server:
            {
                CloseContextMenu();
                Graph.Clear();
                Graph.LoadFileByJson(DungeonEditor.Instance.editingDungeon.nodeEditorJsonData);
                break;
            }
        }
    }

    private void LoadGraph(string savePath)
    {
        CloseContextMenu();
        Graph.Clear();
        Graph.LoadFile(savePath);
    }
    // context item actions done

    private void OnNodePointerClick(Node node, PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            var ctx = new ContextMenuBuilder()
                .Add("duplicate", () => DuplicateNode(node))
                .Add("clear connections", () => ClearConnections(node))
                .Add("delete", () => DeleteNode(node))
                .Build();

            SetContextMenu(ctx);
            DisplayContextMenu();
        }
    }

    private void OnNodeConnectionPointerClick(string connId, PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            var ctx = new ContextMenuBuilder()
                .Add("clear connection", () => DisconnectConnection(connId))
                .Build();

            SetContextMenu(ctx);
            DisplayContextMenu();
        }
    }

    private void DeleteNode(Node node)
    {
        // 해당 노드 스스로 하나 없애주기 + 자신 앞의 노드에게 자신 없어 진다 알려주기 + 자신 뒤의 노드에게 자신 없어진다 알려주기.
        // 사실, "자신 앞의 노드에게 자신 없어 진다 알려주기" 만 하면 되지만, 이걸 탐색 쉽게 하려고 prev ID 탐색을 만듬...
        Dungeon tempEditingDungeon = DungeonEditor.Instance.editingDungeon;
        Stage tempDeletedStage = tempEditingDungeon.stages[node.IdentifierID];

        tempEditingDungeon.stages.Remove(tempDeletedStage.nodeID);
        foreach (var tempDeletedNodeNextStageID in tempDeletedStage.nextStageID)
        {
            // 자신의 뒤의 스테이지로 가서, 앞에 연결된 Stage 중 자신이 없어졌다는 것을 말해줌.
            tempEditingDungeon.stages[tempDeletedNodeNextStageID].prevStageID.Remove(tempDeletedStage.nodeID);
        }

        foreach (var tempDeletedNodePrevStageID in tempDeletedStage.prevStageID)
        {
            // 자신의 앞의 스테이지로 가서, 뒤에 연결된 Stage 중 자신이 없어졌다는 것을 말해줌.
            tempEditingDungeon.stages[tempDeletedNodePrevStageID].nextStageID.Remove(tempDeletedStage.nodeID);
        }

        Graph.Delete(node);

        CloseContextMenu();
    }

    private void DuplicateNode(Node node)
    {
        // stage 추가 + 복제
        Dungeon tempEditingDungeon = DungeonEditor.Instance.editingDungeon;
        ulong tempRecentID = tempEditingDungeon.recentID;

        Stage tempStage = new Stage(tempRecentID);
        tempStage.myStageType = node.GetComponent<Stage>().myStageType;
        tempStage.specificTypeInfo = node.GetComponent<Stage>().specificTypeInfo;

        tempEditingDungeon.stages.Add(tempRecentID, tempStage);
        tempEditingDungeon.recentID += 1;

        Graph.Duplicate(node).IdentifierID = tempRecentID;

        CloseContextMenu();
    }

    private void DisconnectConnection(string line_id)
    {
        // disconnect 하기 전에 먼저 수행
        Dungeon tempEditingDungeon = DungeonEditor.Instance.editingDungeon;
        var connection = Graph.connections.FirstOrDefault<Connection>(c => c.connId == line_id);

        // !!!!! 이거 한번 체크해 봐야 함. Input / Output Node 가 반대 방향일 수도 있음.
        tempEditingDungeon.stages[connection.input.OwnerNode.IdentifierID].nextStageID
            .Remove(connection.output.OwnerNode.IdentifierID);
        tempEditingDungeon.stages[connection.output.OwnerNode.IdentifierID].prevStageID
            .Remove(connection.input.OwnerNode.IdentifierID);

        Graph.Disconnect(line_id);

        CloseContextMenu();
    }

    private void ClearConnections(Node node)
    {
        Dungeon tempEditingDungeon = DungeonEditor.Instance.editingDungeon;
        tempEditingDungeon.stages[node.IdentifierID].prevStageID.Clear();
        tempEditingDungeon.stages[node.IdentifierID].nextStageID.Clear();

        Graph.ClearConnectionsOf(node);
        CloseContextMenu();
    }

    private void OnDisable()
    {
        if (Graph != null)
        {
            Graph.Clear();
        }
        else
        {
            Debug.Log("Graph is null");
        }

    }
}
