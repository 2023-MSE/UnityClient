using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonInfoFolder;
public class ValidationCheck : MonoBehaviour
{
    public bool checkValidation(Dungeon dungeon)
    {
        bool endPoint = false;
        List<Stage> startList = new List<Stage>();

        foreach (var stage in dungeon.stages)
        {
            if (stage.Value.nextStageID.Count == 0)
            {
                if (endPoint)
                    // ���� ��� ���� �Ǵ� ���� ��尡 2�� �̻�
                    return false;
                endPoint = true;
            }
            if (stage.Value.prevStageID.Count == 0)
            {
                // ���� ���� ������ ����
                startList.Add(stage.Value);
            }
            // ���� ���� ���� Ȯ�� �ʿ�
            /*TODO*/
        }

        // �ڻ���� �˰���
        Stack<ulong> stack = new Stack<ulong>();
        Dictionary<ulong, bool> visited = new Dictionary<ulong, bool>();
        foreach (Stage stage in startList)
        {
            visited.Add(stage.nodeID, true);
            DFS(stack, dungeon, visited, stage.nodeID);
        }
        visited.Clear();

        while (stack.Count != 0)
        {
            ulong current = stack.Pop();
            visited.Add(current, true);
            foreach (var next in dungeon.stages[current].prevStageID)
            {
                if (!visited.ContainsKey(next))
                {
                    // ����Ŭ ����
                    return false;
                }
            }
        }

        return true;
    }

    // ���� �켱 Ž��
    private void DFS(Stack<ulong> stack, Dungeon dungeon, Dictionary<ulong, bool> visited, ulong current)
    {
        foreach (var next in dungeon.stages[current].nextStageID)
        {
            if (!visited.ContainsKey(next))
            {
                visited.Add(next, true);
                DFS(stack, dungeon, visited, next);
            }
        }

        stack.Push(current);
    }
}
