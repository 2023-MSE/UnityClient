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
                    // 독립 노드 존재 또는 최종 노드가 2개 이상
                    return false;
                endPoint = true;
            }
            if (stage.Value.prevStageID.Count == 0)
            {
                // 시작 지점 노드들을 저장
                startList.Add(stage.Value);
            }
            // 음악 파일 유무 확인 필요
            /*TODO*/
        }

        // 코사라주 알고리즘
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
                    // 싸이클 존재
                    return false;
                }
            }
        }

        return true;
    }

    // 깊이 우선 탐색
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
