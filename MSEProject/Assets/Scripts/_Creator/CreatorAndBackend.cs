using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Defective.JSON;
using DungeonInfoFolder.DungeonAndUIInterfaces;
using UnityEngine;
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
            }
            else
            {
                Debug.Log("던전 생성에 실패했습니다.");
            }
        }
    }
    
    #endregion

    #region get selected Dungeon stages

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
            JsonUtility.FromJsonOverwrite(response, DungeonEditor.Instance.editingDungeon.stages);
            DungeonEditor.Instance.editingDungeon.ConvertStagesListToDictionary();
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
        string json = "{\"id\":\"" + DungeonEditor.Instance.editingDungeon.userId + "\"}";

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
            JsonUtility.FromJsonOverwrite(response, DungeonManager.Instance.MyDungeonList);
            DungeonUIVisualizer.Instance.VisualizeDungeonList();
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
