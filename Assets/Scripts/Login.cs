using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Login : MonoBehaviour
{
    #region Variables
    public InputField username;
    public InputField password;
    public Button loginButton;
    public Text notify;
    private int currentInputField;
    private InputField[] inputFields;
    #endregion

    #region String Text
    private string loginSuccess = "Login Success";
    private string incorrectUsername = "User Not Found";
    private string incorrectPassword = "Password Incorrect";
    #endregion
    // by default selecting the top inputfield as default selection
    private void Start()
    {
        // get the Transform Component in children gameObject
        inputFields = transform.GetComponentsInChildren<InputField>();
        // select the first inputField
        inputFields[0].Select();
    }

    // Toggling InputFields using Tab key and keep in track of InputFields number when user click the InputField
    private void Update()
    {
        // if one of the inputfield is empty then disabled the SignUpButton
        if (username.text == "" || password.text == "")
        {
            // set loginButton.interactable to false
            loginButton.interactable = false;
            // if loginButton.interactable to false
            if (loginButton.interactable == false)
            {
                // disable loginButton EventTrigger 
                loginButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            // set loginButton.interactable to true
            loginButton.interactable = true;
            // enable loginButton EventTrigger
            loginButton.GetComponent<EventTrigger>().enabled = true;
        }
        // if enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // call UserLogin() coroutine
            StartCoroutine(UserLogin());
            // call ShowNotification() coroutine
            StartCoroutine(ShowNotification());
        }
        // when you press tab then it will give 1 int to TogglingInputFields so then it will incrementing the selected tab to 1
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // calling TogglingInputFields function and inserting 1 for toggling purposes
            TogglingInputFields(1);
        }
        // checking the inputfield position when clicked with mouse
        if (Input.GetMouseButtonDown(0))
        {
            // if EventSystem.current.currentSelectedGameObject is null
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                return;
            }
            // if EventSystem.current.currentSelectedGameObject is username.gameObject
            if (EventSystem.current.currentSelectedGameObject == username.gameObject)
            {
                // set currentInputField to zero
                currentInputField = 0;
            }
            // if EventSystem.current.currentSelectedGameObject == password.gameObject
            if (EventSystem.current.currentSelectedGameObject == password.gameObject)
            {
                // set currentInputField to one
                currentInputField = 1;
            }
        }
    }
    // toggling and counter inputfields and taking integer
    void TogglingInputFields(int direction)
    {
        // currentInputField += direction
        currentInputField += direction;
        // currentInputField  is greater than inputFields.Length - 1
        if (currentInputField > inputFields.Length - 1)
        {
            // set currentInputField to zero
            currentInputField = 0;
        }
        // currentInputField is less than 0
        if (currentInputField < 0)
        {
            // select the latest inputFields gameObject
            inputFields[inputFields.Length - 1].Select();
        }
        // select the inputFields active by index
        inputFields[currentInputField].Select();
    }
    // will check user logion details and show feedback accordingly, if correct then will go to MainMenu
    public void LoginButton()
    {
        // call UserLogin()
        StartCoroutine(UserLogin());
        // call ShowNotification()
        StartCoroutine(ShowNotification());
    }
    // take user to SignUp Scene
    public void SignUpButton()
    {
        // load SignUp scene
        SceneManager.LoadScene("SignUp");
    }
    // take user to ForgetLogin scene
    public void ForgotPassword()
    {
        // load ForgetLogin scene
        SceneManager.LoadScene("ForgetLogin");
    }
    #region PHP
    IEnumerator UserLogin()
    {
        // store loginURL as a string
        string loginURL = "http://localhost/loginsystem/login.php";
        // create a new WWWForm()
        WWWForm loginForm = new WWWForm();
        // match username_Post in PHP with username.text
        loginForm.AddField("username_Post", username.text);
        // match password_Post in PHP with password.text
        loginForm.AddField("password_Post", password.text);
        // create new WWW taking loginURL and loginForm
        WWW www = new WWW(loginURL, loginForm);
        // return www
        yield return www;
        // if www.text equal to loginSuccess
        if (www.text == loginSuccess)
        {
            // change notify.text
            notify.text = "Logging in...";
			// set LoggedInUsername to username.text
			GameManager.LoggedInUsername = username.text;
            // load MainMenu scene
            SceneManager.LoadScene("CharacterSelection");
        }
        // else if www.text equal to incorrectUsername
        else if (www.text == incorrectUsername)
        {
            // change notify text
            notify.text = "Username not found, please sign up";
        }
        // else if www.text equal to incorrectPassword
        else if (www.text == incorrectPassword)
        {
            // change notify text
            notify.text = "Invalid password";
        }
    }
    #endregion
    // feedback notification
    IEnumerator ShowNotification()
    {
        // enable notify
        notify.enabled = true;
        // return wait time for 2 seconds
        yield return new WaitForSeconds(2f);
        // disable notify
        notify.enabled = false;
    }
}
