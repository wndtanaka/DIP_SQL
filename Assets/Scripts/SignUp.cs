using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SignUp : MonoBehaviour
{
    public InputField newUsername;
    public InputField newEmail;
    public InputField newPassword;
    public InputField confirmPassword;

    public Text notify;
    public Text usernameChecks;
    public Text emailChecks;
    public Text passwordChecks;
    public Text passwordMatches;

    #region String Text
    private string firstUser = "Create First User";
    private string createdUser = "Created User";
    private string userExist = "User Already Exists";
    private string emailExist = "Email Already Exists";
    #endregion

    private int currentInputField;
    private InputField[] inputFields;

    void Start()
    {
        notify.enabled = false;
        inputFields = transform.GetComponentsInChildren<InputField>();
        inputFields[0].Select();
    }
    void Update()
    {
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
                usernameChecks.enabled = false;
                UserNameCheck();
            }
            if (inputFields[1].isFocused)
            {
                StartCoroutine(CheckingRealTime());
                emailChecks.enabled = false;
                EmailCheck();
            }
            if (inputFields[2].isFocused)
            {
                PasswordCheck();
                //StartCoroutine(CheckingRealTime());
            }
            if (inputFields[3].isFocused)
            {
                PasswordMatch();
                //StartCoroutine(CheckingRealTime());
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

    public void CreateAccountButton()
    {
        StartCoroutine(CreateAccount());
        StartCoroutine(ShowNotification());
    }

    IEnumerator CreateAccount()
    {
        string createUserURL = "http://localhost/loginsystem/insertuser.php";
        WWWForm user = new WWWForm();
        if (newUsername.text != "" && (newPassword.text != "" && newPassword.text.Length >= 6) && (newEmail.text != "" && newEmail.text.Contains("@") && newEmail.text.Contains(".") && !newEmail.text.Contains(" ")))
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
            notify.text = "There are some error in the form";
        }
    }

    IEnumerator CheckingRealTime()
    {
        //usernameChecks.enabled = false;
        //emailChecks.enabled = false;
        string checkURL = "http://localhost/loginsystem/realtimecheck.php";
        WWWForm checkForm = new WWWForm();
        checkForm.AddField("username_Post", newUsername.text);
        checkForm.AddField("email_Post", newEmail.text);
        WWW www = new WWW(checkURL, checkForm);
        yield return www;
        Debug.Log(www.text);
        if (www.text == userExist + emailExist || www.text == userExist)
        {
            usernameChecks.text = "Username taken";
        }
        else
        {
            usernameChecks.text = "Username available";
            if (newUsername.text == "" || newUsername.text.Contains(" "))
            {
                usernameChecks.text = "Username invalid";
            }
        }
        if (www.text == userExist + emailExist || www.text == emailExist)
        {
            emailChecks.text = "Email has been registered";
        }
        else
        {
            emailChecks.text = "Email available";
            if (newEmail.text == "" || !newEmail.text.Contains("@") || !newEmail.text.Contains(".") || newEmail.text.Contains(" "))
            {
                emailChecks.text = "Email Invalid";
            }
        }

    }

    public void Cancel()
    {
        SceneManager.LoadScene("Login");
    }

    IEnumerator ShowNotification()
    {
        notify.enabled = true;
        yield return new WaitForSeconds(2f);
        notify.enabled = false;
    }

    void UserNameCheck()
    {
        usernameChecks.enabled = true;
        if (newUsername.text == "")
        {
            usernameChecks.text = "Username invalid";
        }
    }
    void EmailCheck()
    {
        emailChecks.enabled = true;
        if (newEmail.text == "")
        {
            emailChecks.text = "Email Invalid";
        }
    }
    void PasswordCheck()
    {
        passwordChecks.enabled = true;
        if (newPassword.text != "" && newPassword.text.Length >= 6)
        {
            passwordChecks.text = "Password valid";
        }
        else if (newPassword.text.Length < 6)
        {
            passwordChecks.text = "Password need to be at least 6 characters";
        }
        else
        {
            passwordChecks.text = "Password invalid";
        }
    }
    void PasswordMatch()
    {
        passwordMatches.enabled = true;
        if (newPassword.text == "")
        {
            passwordMatches.text = "Password invalid";
        }
        else if (confirmPassword.text == newPassword.text)
        {
            passwordMatches.text = "Password match";
        }
        else
        {
            passwordMatches.text = "Password does not match";
        }
    }
}
