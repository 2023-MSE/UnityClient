using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SigninUpManager : MonoBehaviour
{

    private TMP_InputField IDInputField;
    private TMP_InputField passwordInputField;
    private TMP_InputField signupIDInputField;
    private TMP_InputField signupPasswordInputField;
    private TMP_InputField signupPasswordConfirmInputField;
    private TMP_InputField signupNicknameInputField;

    private GameObject signupPopup;
    private GameObject dialogPopup;

    // Start is called before the first frame update
    void Start()
    {
        Transform canvasObject = GameObject.Find("Canvas").GetComponent<Transform>();

        IDInputField = canvasObject.GetChild(1).GetComponent<TMP_InputField>();
        passwordInputField = canvasObject.GetChild(2).GetComponent<TMP_InputField>();

        Transform popupObject = canvasObject.GetChild(5).GetChild(0);
        signupPopup = canvasObject.GetChild(5).gameObject;
        signupPopup.SetActive(false);

        dialogPopup = canvasObject.GetChild(6).gameObject;
        dialogPopup.SetActive(false);

        signupNicknameInputField = popupObject.GetChild(0).GetComponent<TMP_InputField>();
        signupIDInputField = popupObject.GetChild(1).GetComponent<TMP_InputField>();
        signupPasswordInputField = popupObject.GetChild(2).GetComponent<TMP_InputField>();
        signupPasswordConfirmInputField = popupObject.GetChild(3).GetComponent<TMP_InputField>();
}

    private void emphasisInput(TMP_InputField inputField)
    {
        inputField.image.color = new Color(1,0,0,0.4f);
    }
    private void makeDialogtMessage(string str)
    {
        dialogPopup.SetActive(true);
        dialogPopup.GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = str;
    }
    
    [Space(10)]
    [Header("SigninUpEvents")]
    public UnityEvent onSigninSuccess;
    
    public void onClickSigninButton()
    {
        Debug.Log(IDInputField.text);
        Debug.Log(passwordInputField.text);

        SigninupResult result = NetworkManager.instance.requestSignin(IDInputField.text, passwordInputField.text);
        switch (result)
        {
            case SigninupResult.SUCCESS:
                Debug.Log("SigninSuccess");

                // TODO GO TO NEXT SCENE

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

    public void onClickSignupButton()
    {
        signupPopup.SetActive(true);
    }

    public void onClickSubmitButton()
    {
        if (signupPasswordConfirmInputField.text != signupPasswordInputField.text)
        {
            // 패스워드가 서로 다름
            makeDialogtMessage("Password are different.");
            emphasisInput(signupPasswordConfirmInputField);
        }
        else
        {
            SigninupResult result = NetworkManager.instance.requestSignup(
                signupIDInputField.text,
                signupPasswordInputField.text,
                signupNicknameInputField.text
            );

            switch (result)
            {
                case SigninupResult.SUCCESS:
                    Debug.Log("SignupSuccess");
                    onClickCancleButton();
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
    }

    public void onClickCancleButton()
    {
        signupNicknameInputField.text = "";
        signupIDInputField.text = "";
        signupPasswordInputField.text = "";
        signupPasswordConfirmInputField.text = "";

        signupPopup.SetActive(false);
    }

    public void onClickDialogCancleButton()
    {
        dialogPopup.SetActive(false);
    }

    public void resetEmphasis(TMP_InputField inputField)
    {
        inputField.image.color = Color.white;
    }
}
