using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DungeonInfoFolder.DungeonAndUIInterfaces
{
    public class DungeonUIVisualizer : Singleton<DungeonUIVisualizer>
    {
        [Header("Dungeon List")]
        // Related to Dungeon List Visualizaition
        public Transform dungeonListContainer;
        public Transform eachDungeonElementUIPrefab;

        private List<Transform> _instantiatedDungeons = new List<Transform>();

        // 비동기 프로그래밍을 고려하여, Json 정보를 불러온 후, 실행시켜 주기 위해 따로 method 처리.
        public void VisualizeDungeonList()
        {
            if (_instantiatedDungeons.Count > 0)
            {
                foreach (var variDungeon in _instantiatedDungeons)
                {
                    Destroy(variDungeon.gameObject);
                }

                _instantiatedDungeons = new List<Transform>();
            }
            
            DungeonList tempDungeonList = DungeonManager.Instance.MyDungeonList;

            foreach (var variableDungeon in tempDungeonList.myDungeons)
            {
                Transform tempDungeon = Instantiate(eachDungeonElementUIPrefab, dungeonListContainer);
                _instantiatedDungeons.Add(tempDungeon);
                
                if (tempDungeon.GetComponent<EachDungeonMaker>() != null)
                    tempDungeon.GetComponent<EachDungeonMaker>().SetUpEachDungeonUI(variableDungeon.name, variableDungeon.createdTime, variableDungeon);
            }
        }
        
        
        [Space(10)][Header("Dungeon Editor")]
        public TextMeshProUGUI dungeonNameText;
        public List<Transform> stageInputConnection = new List<Transform>();
        
        public void VisualizeDungeonEditor ()
        {
            Dungeon tempDungeon = DungeonEditor.Instance.editingDungeon;
            
            
        }
        
        // Start is called before the first frame update
        void Start()
        {
            if (dungeonListContainer == null)
            {
                Debug.Log("There is no dungeonListContainer!!");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
