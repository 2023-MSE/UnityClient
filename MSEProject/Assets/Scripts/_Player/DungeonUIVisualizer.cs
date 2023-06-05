using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Player
{
public class DungeonUIVisualizer : MonoBehaviour
{
    
        [Header("Dungeon List")]
        // Related to Dungeon List Visualizaition
        public Transform dungeonListContainer;
        public Transform eachDungeonElementUIPrefab;
        public DeployedDungeonList dungeonList { set; get; }
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

            // 리스트 항목 추가 부분
            foreach (DeployedDungeon variableDungeon in dungeonList.deployedList)
            {
                Transform tempDungeon = Instantiate(eachDungeonElementUIPrefab, dungeonListContainer);
                _instantiatedDungeons.Add(tempDungeon);
                
                if (tempDungeon.GetComponent<DungeonElementMaker>() != null)
                    tempDungeon.GetComponent<DungeonElementMaker>().SetUpEachDungeonUI(variableDungeon);
            }
        }
        
        
        [Space(10)][Header("Dungeon Editor")]
        public TextMeshProUGUI dungeonNameText;
        public List<Transform> stageInputConnection = new List<Transform>();
    
        // Start is called before the first frame update
        void Start()
        {
            dungeonList = new DeployedDungeonList();
            if (dungeonListContainer == null)
            {
                Debug.Log("There is no dungeonListContainer!!");
            }
            else
            {
                StartCoroutine(SetDungeonList());
            }
        }

        IEnumerator SetDungeonList()
        {
            NetworkManager.Instance.SetDungeonUIVisualizer(this);
            yield return null;
            NetworkManager.Instance.GetDungeonListStart();
        }
        
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
    
