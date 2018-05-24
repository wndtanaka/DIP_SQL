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

    // by default selecting the top inputfield as default selection
    void Start()
    {
        // get the Transform Component in children gameObject
        inputFields = transform.GetComponentsInChildren<InputField>();
        // select the first inputFields
        inputFields[0].Select();
    }
    // Toggling InputFields using Tab key and keep in track of InputFields number when user click the InputField
    void Update()
    {
        // if one of the inputfield is empty then disabled the SignUpButton
        if (usernameCheck.enabled == false || emailCheck.enabled == false || passwordCheck.enabled == false || passwordMatch.enabled == false)
        {
            // signUpButton not interactable
            signUpButton.interactable = false;
            // if signUpButton not interactable
            if (signUpButton.interactable == false)
            {
                // disable signUpButton EventTrigger
                signUpButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            // if all the inputfield are presents, set the signUpButton.interactable to true
            // signUpButton is interactable
            signUpButton.interactable = true;
            // enable signUpButton EventTrigger
            signUpButton.GetComponent<EventTrigger>().enabled = true;
        }
        // press enter to call the function
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // call CreateAccount()
            StartCoroutine(CreateAccount());
            // call ShowNotification()
            StartCoroutine(ShowNotification());
        }
        // when you press tab then it will give 1 int to TogglingInputFields so then it will incrementing the selected tab to 1, also checking it realtime by sending php/SQL to server for username and email
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // if the first inputFields is focused
            if (inputFields[0].isFocused)
            {
                // call CheckingRealTime()
                StartCoroutine(CheckingRealTime());
                // disabled userNameCheck
                usernameCheck.enabled = false;
                // disabled userNameFail
                usernameFail.enabled = false;
                // call UserNameCheck()
                UserNameCheck();
            }
            // if the second inputFields is focused
            if (inputFields[1].isFocused)
            {
                // call CheckingRealTime()
                StartCoroutine(CheckingRealTime());
                // disabled emailCheck
                emailCheck.enabled = false;
                // disabled emailFail
                emailFail.enabled = false;
                // call EmailCheck()
                EmailCheck();
            }
            // if the third inputFields is focused
            if (inputFields[2].isFocused)
            {
                // call PasswordCheck()
                PasswordCheck();
            }
            // if the fourth inputFields is focused
            if (inputFields[3].isFocused)
            {
                // call PasswordMatch()
                PasswordMatch();
            }
            // call TogglingInputFields taking integer 1
            TogglingInputFields(1);
        }
        // checking the inputfield position when clicked with mouse
        if (Input.GetMouseButtonDown(0))
        {
            // EventSystem.current.currentSelectedGameObject is null
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                // return
                return;
            }
            // EventSystem.current.currentSelectedGameObject equal to newUsername.gameObject
            if (EventSystem.current.currentSelectedGameObject == newUsername.gameObject)
            {
                // call CheckingRealTime()
                StartCoroutine(CheckingRealTime());
                // call UserNameCheck()
                UserNameCheck();
                // set currentInputField to zero
                currentInputField = 0;
            }
            // EventSystem.current.currentSelectedGameObject equal to newEmail.gameObject
            if (EventSystem.current.currentSelectedGameObject == newEmail.gameObject)
            {
                // call CheckingRealTime()
                StartCoroutine(CheckingRealTime());
                // call EmailCheck()
                EmailCheck();
                // set currentInputField to one
                currentInputField = 1;
            }
            // EventSystem.current.currentSelectedGameObject equal to newPassword.gameObject
            if (EventSystem.current.currentSelectedGameObject == newPassword.gameObject)
            {
                // call PasswordCheck()
                PasswordCheck();
                // set currentInputField to two
                currentInputField = 2;
            }
            // EventSystem.current.currentSelectedGameObject equal to confirmPassword.gameObject
            if (EventSystem.current.currentSelectedGameObject == confirmPassword.gameObject)
            {
                // call PasswordMatch()
                PasswordMatch();
                // set currentInputField to three
                currentInputField = 3;
            }
        }
    }
    // toggling and counter inputfields
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
        // call CreateAccount()
        StartCoroutine(CreateAccount());
        // call ShowNotification()
        StartCoroutine(ShowNotification());
    }
    #region PHP
    IEnumerator CreateAccount()
    {
        // store createUserURL as string 
        string createUserURL = "http://localhost/loginsystem/insertuser.php";
        // create a new WWWForm()
        WWWForm user = new WWWForm();
        // if newUsername is not empty and newPassword is not empty and is or longer than 6 and newEmail is not empty, contains @, contains ., and does not contains spaces and newPassword is equal to confirmPassword
        if ((newUsername.text != "") && (newPassword.text != "" && newPassword.text.Length >= 6) && (newEmail.text != "" && newEmail.text.Contains("@") && newEmail.text.Contains(".") && !newEmail.text.Contains(" ")) && (newPassword.text == confirmPassword.text))
        {
            // match username_Post in PHP with newUsername.text
            user.AddField("username_Post", newUsername.text);
            // match email_Post in PHP with newEmail.text
            user.AddField("email_Post", newEmail.text);
            // match password_Post in PHP with newPassword.text
            user.AddField("password_Post", newPassword.text);
            // create a new WWW taking createUserURL and user
            WWW www = new WWW(createUserURL, user);
            // return www
            yield return www;
            Debug.Log(www.text);
            // www.text equal to firstUser or www.text equal to createdUser
            if (www.text == firstUser || www.text == createdUser)
            {
                // change notify.text
                notify.text = "Your account is created";
            }
            // else if www.text equal to userExist
            else if (www.text == userExist)
            {
                // change notify.text
                notify.text = newUsername.text + " is taken, please choose another username";
            }
            // else if www.text equal to emailExist
            else if (www.text == emailExist)
            {
                // change notify text
                notify.text = newEmail.text + " has been registered.";
            }
        }
        else
        {
            // set canSignUp to false
            canSignUp = false;
            // change notify text
            notify.text = "There are some error in the form";
        }
    }

    IEnumerator CheckingRealTime()
    {
        // store checkURL as string
        string checkURL = "http://localhost/loginsystem/realtimecheck.php";
        // create a new WWWForm
        WWWForm checkForm = new WWWForm();
        // match username_Post in PHP with newUsername.text
        checkForm.AddField("username_Post", newUsername.text);
        // match email_Post in PHP with newEmail.text
        checkForm.AddField("email_Post", newEmail.text);
        // create a new WWW taking checkURL and checkForm
        WWW www = new WWW(checkURL, checkForm);
        // return www
        yield return www;
        Debug.Log(www.text);
        // if www.text equal to userExist + emailExist or userExist
        if (www.text == userExist + emailExist || www.text == userExist)
        {
            // enable usernameFaile
            usernameFail.enabled = true;
            // disable usernameCheck
            usernameCheck.enabled = false;
        }
        else
        {
            // enable usernameCheck
            usernameCheck.enabled = true;
            // disable usernameFail
            usernameFail.enabled = false;
            // if newUsername is empty or contains spaces
            if (newUsername.text == "" || newUsername.text.Contains(" "))
            {
                // enabled usernameFail
                usernameFail.enabled = true;
                // disable usernameCheck
                usernameCheck.enabled = false;
            }
        }
        // if www.text equal to userExist + emailExist or emailExist
        if (www.text == userExist + emailExist || www.text == emailExist)
        {
            // enable emailFail
            emailFail.enabled = true;
            // enable emailCheck
            emailCheck.enabled = false;
        }
        else
        {
            // enable emailCheck
            emailCheck.enabled = true;
            // disabled emailFail
            emailFail.enabled = false;
            // if newEmail does not contains @ or . or contains spaces
            if (!newEmail.text.Contains("@") || !newEmail.text.Contains(".") || newEmail.text.Contains(" "))
            {
                // enable emailFail
                emailFail.enabled = true;
                // disable emailCheck
                emailCheck.enabled = false;
            }
        }
    }
    #endregion
    // cancel button will take user back to login screen
    public void Cancel()
    {
        // load Login scene
        SceneManager.LoadScene("Login");
    }
    // notify user feedback
    IEnumerator ShowNotification()
    {
        // enable notify
        notify.enabled = true;
        // wait for 2 seconds
        yield return new WaitForSeconds(2f);
        // disable notify
        notify.enabled = false;
    }
    // checking user details realtime then also show check or cross mark depends on criteria
    #region User Check
    void UserNameCheck()
    {
        // if username is empty then fails
        if (newUsername.text == "")
        {
            // enable usernameFail
            usernameFail.enabled = true;
            // disable usernameCheck
            usernameCheck.enabled = false;
        }
    }
    void EmailCheck()
    {
        // if email is empty then fails
        if (newEmail.text == "")
        {
            // enable emailFail
            emailFail.enabled = true;
            // disable emailCheck
            emailCheck.enabled = false;
        }
    }
    void PasswordCheck()
    {
        // checking password if it is not empty and 6 or longer characters
        if (newPassword.text != "" && newPassword.text.Length >= 6)
        {
            // enable passwordCheck
            passwordCheck.enabled = true;
            // disable passwordFail
            passwordFail.enabled = false;
        }
        // else if newPassword is less than 6 characters
        else if (newPassword.text.Length < 6)
        {
            // enable passwordFail
            passwordFail.enabled = true;
            // disable passwordCheck
            passwordCheck.enabled = false;
        }
        // else
        else
        {
            // enabled passwordFail
            passwordFail.enabled = true;
            // disabled passwordCheck
            passwordCheck.enabled = false;
        }
    }
    // function that check if the Password Matches
    void PasswordMatch()
    {
        // if the newPassword is empty then fail 
        if (newPassword.text == "")
        {
            // enable passwordFailMatch
            passwordFailMatch.enabled = true;
            // disable passwordMatch
            passwordMatch.enabled = false;
        }
        // if the newPassword and confirmPassword is the same then proceed
        else if (confirmPassword.text == newPassword.text)
        {
            // enable passwordMatch
            passwordMatch.enabled = true;
            // disable passwordFailMatch
            passwordFailMatch.enabled = false;
        }
        else
        {
            // enable passwordFailMatch
            passwordFailMatch.enabled = true;
            // disable passwordMatch
            passwordMatch.enabled = false;
        }
    }
    #endregion
}
