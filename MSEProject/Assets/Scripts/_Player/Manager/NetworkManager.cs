using System;
using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;
using UnityEngine.Networking;

namespace _Player
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private readonly string _serverUrl = "http://localhost:8080/mse"; // SpringMVC 서버 URL
        private DungeonUIVisualizer _visualizer;
        
        #region GetDungeonList

        public void GetDungeonListStart()
        {
            StartCoroutine(GetDungeonList());
        }

        private IEnumerator GetDungeonList()
        {
            // 1. Request URL 설정
            string url = _serverUrl + "/login/get-player-dungeon-list";

            // 2. Web Request 생성 및 설정
            UnityWebRequest webRequest = CommonPostWebRequestSetupByJson(url, "");

            // 3. HTTP Request 전송 및 대기
            yield return webRequest.SendWebRequest();

            // 4. HTTP Response 코드 확인
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                // 5. HTTP Response 결과 확인
                string response = webRequest.downloadHandler.text;

                // 5-1. jsonUtility Class 를 이용하여 받아온 List<Stage> 값을 현재 editingDungeon.stages 에 덮어씌움.
                /*TODO : Json 변환시 오류있음! 해결 필요 */
                JsonUtility.FromJsonOverwrite(response, _visualizer.dungeonList);
                _visualizer.VisualizeDungeonList();
            }
        }

        #endregion

        #region GetSelectDungeon

        public void GetSelectDungeonStart(DeployedDungeon deployedDungeon)
        {
            StartCoroutine(GetSelectDungeon(deployedDungeon));
        }
        
        private IEnumerator GetSelectDungeon(DeployedDungeon deployedDungeon)
        {
            /*TODO : NOT TESTED!! */
            // 1. Request URL 설정
            string url = _serverUrl + "/creator/get-select-map";

            // 2. Web Request 생성 및 설정
            UnityWebRequest webRequest = CommonPostWebRequestSetupByJson(url, "{\"mapId\":\"" + deployedDungeon.id+ "\"}");

            // 3. HTTP Request 전송 및 대기
            yield return webRequest.SendWebRequest();

            // 4. HTTP Response 코드 확인
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                // 5. HTTP Response 결과 확인
                string response = webRequest.downloadHandler.text;

                // 5-1. jsonUtility Class 를 이용하여 받아온 List<Stage> 값을 현재 editingDungeon.stages 에 덮어씌움.
                JsonUtility.FromJsonOverwrite(response, CombatScene.DungeonManager.Instance.dungeon.stages);
                CombatScene.DungeonManager.Instance.dungeon.ConvertStagesListToDictionary();
                
                // 5-2. 게임 시작
                CombatScene.DungeonManager.Instance.GoNextStage(0);
            }
        }
        
        #endregion

        public UnityWebRequest CommonPostWebRequestSetupByJson(string url, string json)
        {
            // HTTP Request 생성
            UnityWebRequest webRequest = UnityWebRequest.Post(url, json);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);

            // HTTP Request 헤더 설정
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Content-Type", "application/json");

            return webRequest;
        }

        public UnityWebRequest CommonGetWebRequest(string url)
        {
            // HTTP Request 생성
            UnityWebRequest webRequest = UnityWebRequest.Get(url);

            // HTTP Request 헤더 설정
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Content-Type", "application/json");

            return webRequest;
        }
        
        public void SetDungeonUIVisualizer(DungeonUIVisualizer visualizer)
        {
            _visualizer = visualizer;
        }

    }
}