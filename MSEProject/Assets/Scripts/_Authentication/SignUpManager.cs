using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Defective.JSON;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SignUpManager : MonoBehaviour
{
    public string serverUrl = "http://localhost:80/mse"; // SpringMVC 서버 URL
    
    public UnityEvent<bool> onDoubleCheckSuccess;
    public UnityEvent<bool> onDoubleCheckFail;
    
    public UnityEvent onSignUpSuccess;
    public UnityEvent onSignUpFail;
    
    // ID & Nickname 두 가지의 중복체크를 위한 메서드. 두 리퀘스트를 따로따로 전송할 필요가 있음.
    public IEnumerator DoubleCheck(bool isID, string s) {
        string url = serverUrl + "/login/double-check";
        string json = "{\"isId\":" + isID.ToString().ToLower() + ", \"s\":\"" + s + "\"}";
        Debug.Log(url);
        Debug.Log(json);
        
        UnityWebRequest webRequest = UnityWebRequest.Post(url, json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            string response = webRequest.downloadHandler.text;
            
            JSONObject obj = new JSONObject(response);
            bool isDouble = obj.GetField("isDouble");

            if (isDouble) {
                Debug.Log("사용 가능한 " + (isID ? "ID" : "닉네임") + "입니다.");
                if (isID)
                    onDoubleCheckSuccess.Invoke(true);
                else 
                    onDoubleCheckSuccess.Invoke(false);
            } else {
                Debug.Log("이미 사용중인 " + (isID ? "ID" : "닉네임") + "입니다.");
                
                if (isID)
                    onDoubleCheckFail.Invoke(true);
                else 
                    onDoubleCheckFail.Invoke(false);
            }
        }
    }
    
    // Double Check 후 실행. 단, ID / PW / Nickname 등에 대한 정보가 추가적으로 필요하므로 Interface 가 되는 Class 에서 실행.
    public IEnumerator SignUp(string id, string pw, string nickname) {
        string url = serverUrl + "/login/signup";
        string json = "{\"loginId\":\"" + id + "\", \"loginPw\":\"" + pw + "\", \"nickname\":\"" + nickname + "\"}";

        UnityWebRequest webRequest = UnityWebRequest.Post(url, json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");
        
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success) {
            Debug.Log(webRequest.error);
        } else {
            string response = webRequest.downloadHandler.text;
            
            JSONObject obj = new JSONObject(response);
            bool success = obj.GetField("success");

            if (success) {
                Debug.Log("회원가입이 성공적으로 완료되었습니다.");
                onSignUpSuccess.Invoke();
            } else {
                Debug.Log("회원가입에 실패했습니다.");
                onSignUpFail.Invoke();
            }
        }
    }
}
