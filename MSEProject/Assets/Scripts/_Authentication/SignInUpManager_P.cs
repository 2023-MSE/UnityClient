using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class SigninUpManager : MonoBehaviour
{
    public LoginManager loginManager;
    public SignUpManager signUpManager;
    
    # region Login
    
    private void SetUpForLogin()
    {
        loginManager.onLoginFailWithNoId.AddListener(OnLoginFailWithNoIdListener);
        loginManager.onLoginFailWithWrongPw.AddListener(OnLoginFailWithWrongPwListener);
        loginManager.onLoginSuccess.AddListener(OnLoginSuccessListener);
        
        loginManager.onGetUserInfoDone.AddListener(OnSignInDone);
    }
    
    // Login Manager 의 모듈성을 해치지 않도록 별도의 Listener 를 제작.
    // 또한 애초에 Event 의 용도가 구분되어 있었기 때문에 이와 같이 구분.
    private void OnLoginFailWithNoIdListener()
    {
        OnClickSignInButtonDone(SigninupResult.INVALID_ID);
    }
    
    private void OnLoginFailWithWrongPwListener()
    {
        OnClickSignInButtonDone(SigninupResult.INVALID_PASSWD);
    }
    
    private void OnLoginSuccessListener()
    {
        OnClickSignInButtonDone(SigninupResult.SUCCESS);
    }
    
    public void OnClickSignInButtonDone(SigninupResult result)
    {
        switch (result)
        {
            case SigninupResult.SUCCESS:
                Debug.Log("SigninSuccess");
                break;
            case SigninupResult.NETWORK_ERROR:
                makeDialogtMessage("Network Error Occur");
                break;
            case SigninupResult.INVALID_ID:
                makeDialogtMessage("ID is invalid");
                emphasisInput(IDInputField);
                break;
            case SigninupResult.INVALID_PASSWD:
                makeDialogtMessage("Password is invalid");
                emphasisInput(passwordInputField);
                break;
        }
    }
    
    public void OnSignInDone(UserInfo userInfo)
    {
        UserInfoHolder.Instance.myInfo = userInfo;
        onSigninSuccess.Invoke();
    }
    
    #endregion

    #region Signup Submit

    private void SetUpForSignUp()
    {
        signUpManager.onDoubleCheckSuccess.AddListener(OnDoubleCheckSuccessListener);
        signUpManager.onDoubleCheckFail.AddListener(OnDoubleCheckFailListener);
        
        signUpManager.onSignUpFail.AddListener(OnSignUpFailListener);
        signUpManager.onSignUpSuccess.AddListener(OnSignUpSuccessListener);
    }

    private void OnDoubleCheckSuccessListener(bool isID)
    {
        if (isID)
            OnClickSubmitButton(SigninupResult.ID_DOUBLE_CHECK_SUCCESS);
        else
        {
            OnClickSubmitButton(SigninupResult.NICKNAME_DOUBLE_CHECK_SUCCESS);
        }
    }
    
    private void OnDoubleCheckFailListener(bool isID)
    {
        if (isID)
            OnClickSubmitButton(SigninupResult.INVALID_ID);
        else
        {
            OnClickSubmitButton(SigninupResult.INVALID_NICKNAME);
        }
    }
    
    private void OnSignUpFailListener()
    {
        OnClickSubmitButton(SigninupResult.NETWORK_ERROR);
    }
    
    private void OnSignUpSuccessListener()
    {
        OnClickSubmitButton(SigninupResult.SUCCESS);
    }

    private bool id;
    private bool nickname;
    
    public void OnClickSubmitButton(SigninupResult result)
    {
        switch (result)
        {
            case SigninupResult.SUCCESS:
                Debug.Log("SignupSuccess");
                onClickCancleButton();
                break;
            case SigninupResult.ID_DOUBLE_CHECK_SUCCESS :
                Debug.Log("ID Double Check Success");
                id = true;
                NicknameAndIdCheck();
                break;
            case SigninupResult.NICKNAME_DOUBLE_CHECK_SUCCESS :
                Debug.Log("Nickname Double Check Success");
                nickname = true;
                NicknameAndIdCheck();
                break;
            case SigninupResult.NETWORK_ERROR:
                Debug.Log("Network error occur");
                makeDialogtMessage("Network Error Occur");
                break;
            case SigninupResult.INVALID_ID:
                makeDialogtMessage("ID is invalid");
                emphasisInput(signupIDInputField);
                break;
            case SigninupResult.INVALID_PASSWD:
                makeDialogtMessage("Password is invalid");
                emphasisInput(signupPasswordInputField);
                break;
            case SigninupResult.INVALID_NICKNAME:
                makeDialogtMessage("NickName is invalid");
                emphasisInput(signupNicknameInputField);
                break;
        }
    }

    private void NicknameAndIdCheck()
    {
        if (id && nickname)
        {
            id = false;
            nickname = false;
            
            StartCoroutine(signUpManager.SignUp(signupIDInputField.text, signupPasswordInputField.text, signupNicknameInputField.text));
        }
    }

    #endregion
}
