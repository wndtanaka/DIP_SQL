using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SignUp : MonoBehaviour
{
    #region Variables
    public InputField newUsername;
    public InputField newEmail;
    public InputField newPassword;
    public InputField confirmPassword;

    public Button signUpButton;
    public Text notify;
    public bool canSignUp = false;

    #region Success Check
    public Image usernameCheck;
    public Image emailCheck;
    public Image passwordCheck;
    public Image passwordMatch;
    #endregion

    #region Fail Check
    public Image usernameFail;
    public Image emailFail;
    public Image passwordFail;
    public Image passwordFailMatch;

    private int currentInputField;
    private InputField[] inputFields;
    #endregion
    #endregion
    #region String Text
    private string firstUser = "Create First User";
    private string createdUser = "Created User";
    private string userExist = "User Already Exists";
    private string emailExist = "Email Already Exists";
    #endregion

    void Start()
    {
        inputFields = transform.GetComponentsInChildren<InputField>();
        inputFields[0].Select();
    }
    // Toggling InputFields using Tab key and keep in track of InputFields number when user click the InputField
    void Update()
    {
        if (usernameCheck.enabled == false || emailCheck.enabled == false || passwordCheck.enabled == false || passwordMatch.enabled == false)
        {
            signUpButton.interactable = false;
            if (signUpButton.interactable == false)
            {
                signUpButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            signUpButton.interactable = true;
            signUpButton.GetComponent<EventTrigger>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CreateAccount());
            StartCoroutine(ShowNotification());
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputFields[0].isFocused)
            {
                StartCoroutine(CheckingRealTime());
                usernameCheck.enabled = false;
                usernameFail.enabled = false;
                UserNameCheck();
            }
            if (inputFields[1].isFocused)
            {
                StartCoroutine(CheckingRealTime());
                emailCheck.enabled = false;
                emailFail.enabled = false;
                EmailCheck();
            }
            if (inputFields[2].isFocused)
            {
                PasswordCheck();
            }
            if (inputFields[3].isFocused)
            {
                PasswordMatch();
            }
            TogglingInputFields(1);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                return;
            }
            if (EventSystem.current.currentSelectedGameObject == newUsername.gameObject)
            {
                StartCoroutine(CheckingRealTime());
                UserNameCheck();
                currentInputField = 0;
            }
            if (EventSystem.current.currentSelectedGameObject == newEmail.gameObject)
            {
                StartCoroutine(CheckingRealTime());
                EmailCheck();
                currentInputField = 1;
            }
            if (EventSystem.current.currentSelectedGameObject == newPassword.gameObject)
            {
                PasswordCheck();
                currentInputField = 2;
            }
            if (EventSystem.current.currentSelectedGameObject == confirmPassword.gameObject)
            {
                PasswordMatch();
                currentInputField = 3;
            }
        }
    }
    // togglinf and counter inputfields
    void TogglingInputFields(int direction)
    {
        currentInputField += direction;
        if (currentInputField > inputFields.Length - 1)
        {
            currentInputField = 0;
        }
        if (currentInputField < 0)
        {
            inputFields[inputFields.Length - 1].Select();
        }
        inputFields[currentInputField].Select();
    }
    // create account button will call php script, check all details input by user and then will give feedback accordingly
    public void CreateAccountButton()
    {
        StartCoroutine(CreateAccount());
        StartCoroutine(ShowNotification());
    }
    #region PHP
    IEnumerator CreateAccount()
    {
        string createUserURL = "http://localhost/loginsystem/insertuser.php";
        WWWForm user = new WWWForm();
        if ((newUsername.text != "") && (newPassword.text != "" && newPassword.text.Length >= 6) && (newEmail.text != "" && newEmail.text.Contains("@") && newEmail.text.Contains(".") && !newEmail.text.Contains(" ")) && (newPassword.text == confirmPassword.text))
        {
            user.AddField("username_Post", newUsername.text);
            user.AddField("email_Post", newEmail.text);
            user.AddField("password_Post", newPassword.text);
            WWW www = new WWW(createUserURL, user);
            yield return www;
            Debug.Log(www.text);
            if (www.text == firstUser || www.text == createdUser)
            {
                notify.text = "Your account is created";
            }
            else if (www.text == userExist)
            {
                notify.text = newUsername.text + " is taken, please choose another username";
            }
            else if (www.text == emailExist)
            {
                notify.text = newEmail.text + " has been registered.";
            }
        }
        else
        {
            canSignUp = false;
            notify.text = "There are some error in the form";
        }
    }

    IEnumerator CheckingRealTime()
    {
        string checkURL = "http://localhost/loginsystem/realtimecheck.php";
        WWWForm checkForm = new WWWForm();
        checkForm.AddField("username_Post", newUsername.text);
        checkForm.AddField("email_Post", newEmail.text);
        WWW www = new WWW(checkURL, checkForm);
        yield return www;
        Debug.Log(www.text);
        if (www.text == userExist + emailExist || www.text == userExist)
        {
            usernameFail.enabled = true;
            usernameCheck.enabled = false;
        }
        else
        {
            usernameCheck.enabled = true;
            usernameFail.enabled = false;
            if (newUsername.text == "" || newUsername.text.Contains(" "))
            {
                usernameFail.enabled = true;
                usernameCheck.enabled = false;
            }
        }
        if (www.text == userExist + emailExist || www.text == emailExist)
        {
            emailFail.enabled = true;
            emailCheck.enabled = false;
        }
        else
        {
            emailCheck.enabled = true;
            emailFail.enabled = false;
            if (/*newEmail.text == "" || */!newEmail.text.Contains("@") || !newEmail.text.Contains(".") || newEmail.text.Contains(" "))
            {
                emailFail.enabled = true;
                emailCheck.enabled = false;
            }
        }

    }
    #endregion
    // cancel button will take user back to login screen
    public void Cancel()
    {
        SceneManager.LoadScene("Login");
    }
    // notify user feedback
    IEnumerator ShowNotification()
    {
        notify.enabled = true;
        yield return new WaitForSeconds(2f);
        notify.enabled = false;
    }
    // checking user details realtime then also show check or cross mark depends on criteria
    #region User Check
    void UserNameCheck()
    {
        if (newUsername.text == "")
        {
            usernameFail.enabled = true;
            usernameCheck.enabled = false;
        }
    }
    void EmailCheck()
    {
        if (newEmail.text == "")
        {
            emailFail.enabled = true;
            emailCheck.enabled = false;
        }
    }
    void PasswordCheck()
    {
        if (newPassword.text != "" && newPassword.text.Length >= 6)
        {
            passwordCheck.enabled = true;
            passwordFail.enabled = false;
        }
        else if (newPassword.text.Length < 6)
        {
            passwordFail.enabled = true;
            passwordCheck.enabled = false;
        }
        else
        {
            passwordFail.enabled = true;
            passwordCheck.enabled = false;
        }
    }
    void PasswordMatch()
    {
        if (newPassword.text == "")
        {
            passwordFailMatch.enabled = true;
            passwordMatch.enabled = false;
        }
        else if (confirmPassword.text == newPassword.text)
        {
            passwordMatch.enabled = true;
            passwordFailMatch.enabled = false;
        }
        else
        {
            passwordFailMatch.enabled = true;
            passwordMatch.enabled = false;
        }
    }
    #endregion

}
