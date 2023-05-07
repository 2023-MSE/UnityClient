using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public partial class SigninUpManager : MonoBehaviour
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
        
        SetUpForLogin();
        SetUpForSignUp();
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
        string id = IDInputField.text;
        string pw = passwordInputField.text;

        StartCoroutine(loginManager.Login(id, pw));
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
            string id = signupIDInputField.text;
            string nickname = signupNicknameInputField.text;

            StartCoroutine(signUpManager.DoubleCheck(true, id));
            StartCoroutine(signUpManager.DoubleCheck(false, nickname));
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
