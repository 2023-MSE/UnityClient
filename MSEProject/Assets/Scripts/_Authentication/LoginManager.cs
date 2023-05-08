using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    public string serverUrl = "http://localhost:80"; // SpringMVC 서버 URL
    
    public UnityEvent onLoginSuccess;
    
    public UnityEvent onLoginFailWithNoId;
    public UnityEvent onLoginFailWithWrongPw;
    
    public UnityEvent<UserInfo> onGetUserInfoDone;

    public IEnumerator Login(string id, string pw) {
        // 1. Request URL 설정
        string url = serverUrl + "/login/authentication";
        string json = "{\"loginId\":\"" + id + "\", \"loginPw\":\"" + pw + "\"}";
        
        // 2. HTTP Request 생성
        UnityWebRequest webRequest = UnityWebRequest.Post(url, json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        
        // 3. HTTP Request 헤더 설정
        webRequest.SetRequestHeader("Accept", "application/json");
        webRequest.SetRequestHeader("Content-Type", "application/json");
        
        // 4. HTTP Request 전송 및 대기
        yield return webRequest.SendWebRequest();
        
        // 5. HTTP Response 코드 확인
        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            // 6. HTTP Response 결과 확인
            string response = webRequest.downloadHandler.text;
            
            // 6-1. 정규식을 사용하여 숫자 추출
            string pattern = @"(\d+)";
            Match match = Regex.Match(response, pattern);
            long num = -3;

            if (match.Success) {
                num = long.Parse(match.Groups[1].Value);
                Debug.Log(num);
            } else {
                Debug.Log("숫자를 찾을 수 없습니다.");
            }
            
            // 6-2. num에 따른 분기 처리
            if (num > 0) {
                onLoginSuccess.Invoke();
                
                // 7. 유저 정보 받아오기
                StartCoroutine(GetUser(num));
            } else if (num == -1) {
                Debug.Log("ID가 없습니다.");
                onLoginFailWithNoId.Invoke();
            } else if (num == -2) {
                Debug.Log("비밀번호가 일치하지 않습니다.");
                onLoginFailWithWrongPw.Invoke();
            }
        }
    }

    public IEnumerator GetUser(long num)
    {
        string url = serverUrl + "/login/post-user";
        string json = "{\"id\":" + num + "}";

        UnityWebRequest webRequest = UnityWebRequest.Post(url, json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);

        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string response = webRequest.downloadHandler.text;
            UserInfo myUserInfo = JsonUtility.FromJson<UserInfo>(response);

            onGetUserInfoDone.Invoke(myUserInfo);
        }
    }
}
