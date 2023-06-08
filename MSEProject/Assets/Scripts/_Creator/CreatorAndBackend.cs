using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using DungeonInfoFolder;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CreatorAndBackend : MonoBehaviour
{
    private readonly string _serverUrl = "http://localhost:8080/mse/creator"; // SpringMVC 서버 URL

    #region Create Dungeon

    /// <summary>
    /// Dungeon Editor 창에서 빠져나올 때 사용.
    /// </summary>
    public void CreateDungeonStart()
    {
        StartCoroutine(CreateDungeonRequest());
    }
    
    private IEnumerator CreateDungeonRequest() {
        // 1. Request URL 설정
        string url = _serverUrl + "/create";
        
        // 1-1. DungeonEditor.Instance.editingDungeon 을 JSON String 으로 변환
        DungeonEditor.Instance.editingDungeon.ConvertStagesDictionaryToList();
        DungeonEditor.Instance.editingDungeon.ConvertStagesMusicBytesDataToBase64();
        
        string json = JsonUtility.ToJson(DungeonEditor.Instance.editingDungeon);
        Debug.Log(json);

        // 2. Web Request 생성 및 설정
        UnityWebRequest webRequest = CommonWebRequestSetupByJson(url, json);

        // 3. HTTP Request 전송 및 대기
        yield return webRequest.SendWebRequest();
        
        // 4. HTTP Response 코드 확인
        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            // 5. HTTP Response 결과 확인
            string response = webRequest.downloadHandler.text;
            
            // 5-1. jSONObject Class를 사용하여 여부 추출
            JSONObject obj = new JSONObject(response);
            bool isSuccess = obj.GetField("success");

            if (isSuccess)
            {
                Debug.Log("던전 생성에 성공했습니다.");
            }
            else
            {
                Debug.Log("던전 생성에 실패했습니다.");
            }
        }
    }
    
    #endregion
    
    #region Edit Dungeon

    /// <summary>
    /// Edit Dungeon 끝나서 Dungeon Editor 빠져나올 때 사용.
    /// </summary>
    public void EditDungeonStart()
    {
        StartCoroutine(EditDungeonRequest());
    }
    
    private IEnumerator EditDungeonRequest() {
        // 1. Request URL 설정
        string url = _serverUrl + "/edit";
        DungeonEditor.Instance.editingDungeon.ConvertStagesDictionaryToList();
        DungeonEditor.Instance.editingDungeon.ConvertStagesMusicBytesDataToBase64();
        
        string json = JsonUtility.ToJson(DungeonEditor.Instance.editingDungeon);

        // 2. Web Request 생성 및 설정
        UnityWebRequest webRequest = CommonWebRequestSetupByJson(url, json);

        // 3. HTTP Request 전송 및 대기
        yield return webRequest.SendWebRequest();
        
        // 4. HTTP Response 코드 확인
        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            // 5. HTTP Response 결과 확인
            string response = webRequest.downloadHandler.text;
            
            // 5-1. jSONObject Class를 사용하여 여부 추출
            JSONObject obj = new JSONObject(response);
            bool isSuccess = obj.GetField("success");

            if (isSuccess)
            {
                Debug.Log("던전 생성에 성공했습니다.");
                GetMyDungeonListStart();
            }
            else
            {
                Debug.Log("던전 생성에 실패했습니다.");
            }
        }
    }
    
    #endregion

    #region get selected Dungeon stages

    public UnityEvent onGetSelectedDungeonStagesSuccess;
    
    public void GetSelectedDungeonStagesStart()
    {
        StartCoroutine(GetSelectedDungeonStagesRequest());
    }
    
    private IEnumerator GetSelectedDungeonStagesRequest() {
        // 1. Request URL 설정
        string url = _serverUrl + "/get-select-map";
        string json = "{\"mapId\":\"" + DungeonEditor.Instance.editingDungeon.id+ "\"}";

        // 2. Web Request 생성 및 설정
        UnityWebRequest webRequest = CommonWebRequestSetupByJson(url, json);

        // 3. HTTP Request 전송 및 대기
        yield return webRequest.SendWebRequest();
        
        // 4. HTTP Response 코드 확인
        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            // 5. HTTP Response 결과 확인
            string response = webRequest.downloadHandler.text;
            
            // 5-1. jsonUtility Class 를 이용하여 받아온 List<Stage> 값을 현재 editingDungeon.stages 에 덮어씌움.
            // JsonUtility.FromJsonOverwrite(response, DungeonEditor.Instance.editingDungeon.stages); - Json이 Object 타입일 때만 사용이 가능.
            DungeonEditor.Instance.editingDungeon.stages = JsonConvert.DeserializeObject<List<Stage>>(response);
            DungeonEditor.Instance.editingDungeon.ConvertStagesListToDictionary();
            
            DungeonEditor.Instance.editingDungeon.ConvertStagesBase64ToMusicBytesData();
            
            // 6. Load 가 끝나고 나서 각종 이벤트 실행.
            onGetSelectedDungeonStagesSuccess.Invoke();
        }
    }
    
    #endregion
    
    #region delete Dungeon

    public void DeleteDungeonStart()
    {
        StartCoroutine(DeleteDungeonRequest());
    }
    
    private IEnumerator DeleteDungeonRequest()
    {
        // 1. Request URL 설정
        string url = _serverUrl + "/delete";
        string json = "{\"userId\":\"" + DungeonEditor.Instance.editingDungeon.userId + "\", \"mapId\":\"" + DungeonEditor.Instance.editingDungeon.id+ "\"}";

        // 2. Web Request 생성 및 설정
        UnityWebRequest webRequest = CommonWebRequestSetupByJson(url, json);

        // 3. HTTP Request 전송 및 대기
        yield return webRequest.SendWebRequest();
        
        // 4. HTTP Response 코드 확인
        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            // 5. HTTP Response 결과 확인
            string response = webRequest.downloadHandler.text;
            
            // 5-1. jSONObject Class를 사용하여 여부 추출
            JSONObject obj = new JSONObject(response);
            bool isSuccess = obj.GetField("success");

            if (isSuccess)
            {
                Debug.Log("던전 삭제에 성공했습니다.");
            }
            else
            {
                Debug.Log("던전 삭제에 실패했습니다.");
            }
        }
    }
    
    #endregion
    
    #region deploy Dungeon

    public void DeployDungeonStart()
    {
        StartCoroutine(DeployDungeonRequest());
    }
    
    private IEnumerator DeployDungeonRequest()
    {
        // 1. Request URL 설정
        string url = _serverUrl + "/deploy-state-change";
        string json = "{\"userId\":\"" + DungeonEditor.Instance.editingDungeon.userId + "\", \"mapId\":\"" + DungeonEditor.Instance.editingDungeon.id+ "\"}";

        // 2. Web Request 생성 및 설정
        UnityWebRequest webRequest = CommonWebRequestSetupByJson(url, json);

        // 3. HTTP Request 전송 및 대기
        yield return webRequest.SendWebRequest();
        
        // 4. HTTP Response 코드 확인
        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            // 5. HTTP Response 결과 확인
            string response = webRequest.downloadHandler.text;
            
            // 5-1. jSONObject Class를 사용하여 여부 추출
            JSONObject obj = new JSONObject(response);
            bool isSuccess = obj.GetField("success");

            if (isSuccess)
            {
                Debug.Log("던전 공유에 성공했습니다.");
            }
            else
            {
                Debug.Log("던전 공유에 실패했습니다.");
            }
        }
    }
    
    #endregion
    
    #region get my dungeon list
    
    public void GetMyDungeonListStart()
    {
        StartCoroutine(GetMyDungeonListRequest());
    }
    
    private IEnumerator GetMyDungeonListRequest()
    {
        // 1. Request URL 설정
        string url = "http://localhost:8080/mse/login/get-creator-dungeon-list";
        string json = "{\"id\":\"" + UserInfoHolder.Instance.myInfo.id + "\"}";

        // 2. Web Request 생성 및 설정
        UnityWebRequest webRequest = CommonWebRequestSetupByJson(url, json);

        // 3. HTTP Request 전송 및 대기
        yield return webRequest.SendWebRequest();
        
        // 4. HTTP Response 코드 확인
        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            // 5. HTTP Response 결과 확인
            string response = webRequest.downloadHandler.text;
            Debug.Log(response);
            // 5-1. jsonUtility Class 를 이용하여 받아온 List<Stage> 값을 현재 editingDungeon.stages 에 덮어씌움.
            // JsonUtility.FromJsonOverwrite(response, DungeonManager.Instance.MyDungeonList); - Json이 Object 타입일 때만 해당 코드의 사용이 가능.
            
            List<Dungeon> dungeons = JsonConvert.DeserializeObject<List<Dungeon>>(response);
            if (dungeons != null && dungeons.Count > 0)
            {
                DungeonManager.Instance.MyDungeonList.myDungeons = dungeons;
            }
            else
            {
                // 이거 왜 안 되지??? - myDungeonList 가 없을 가능성이 제일 높음.
                DungeonManager.Instance.MyDungeonList.myDungeons = new List<Dungeon>();
            }
            
            DungeonUIVisualizer.Instance.VisualizeDungeonList();
            
            Debug.Log("Get Dungeon List done");
        }
    }
    
    #endregion
    
    public void CreateOrEditDungeonStart()
    {
        if (DungeonEditor.Instance.dungeonEditorType == DungeonEditorType.Create) 
            CreateDungeonStart();
        else if (DungeonEditor.Instance.dungeonEditorType == DungeonEditorType.Edit)
            EditDungeonStart();
    }
    
    public UnityWebRequest CommonWebRequestSetupByJson(string url, string json)
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
