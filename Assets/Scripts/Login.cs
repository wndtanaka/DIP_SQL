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

    private void Start()
    {
        inputFields = transform.GetComponentsInChildren<InputField>();
        inputFields[0].Select();
    }

    // Toggling InputFields using Tab key and keep in track of InputFields number when user click the InputField
    private void Update()
    {
        // if one of the inputfield is empty then disabled the SignUpButton
        if (username.text == "" || password.text == "")
        {
            loginButton.interactable = false;
            if (loginButton.interactable == false)
            {
                loginButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            loginButton.interactable = true;
            loginButton.GetComponent<EventTrigger>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(UserLogin());
            StartCoroutine(ShowNotification());
        }
        // when you press tab then it will give 1 int to TogglingInputFields so then it will incrementing the selected tab to 1
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglingInputFields(1);
        }
        // checking the inputfield position when clicked with mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                return;
            }
            if (EventSystem.current.currentSelectedGameObject == username.gameObject)
            {
                currentInputField = 0;
            }
            if (EventSystem.current.currentSelectedGameObject == password.gameObject)
            {
                currentInputField = 1;
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
    // will check user logion details and show feedback accordingly, if correct then will go to MainMenu
    public void LoginButton()
    {
        StartCoroutine(UserLogin());
        StartCoroutine(ShowNotification());
    }
    // take user to SignUp Scene
    public void SignUpButton()
    {
        SceneManager.LoadScene("SignUp");
    }
    // take user to ForgetLogin scene
    public void ForgotPassword()
    {
        SceneManager.LoadScene("ForgetLogin");
    }
    #region PHP
    IEnumerator UserLogin()
    {
        string loginURL = "http://localhost/loginsystem/login.php";
        WWWForm loginForm = new WWWForm();
        loginForm.AddField("username_Post", username.text);
        loginForm.AddField("password_Post", password.text);
        WWW www = new WWW(loginURL, loginForm);
        yield return www;
        //Debug.Log(www.text);
        if (www.text == loginSuccess)
        {
            notify.text = "Logging in...";
            //canLogin = !canLogin;
            SceneManager.LoadScene("MainMenu");
        }
        else if (www.text == incorrectUsername)
        {
            notify.text = "Username not found, please sign up";
        }
        else if (www.text == incorrectPassword)
        {
            notify.text = "Invalid password";
        }
    }
    #endregion
    // feedback notification
    IEnumerator ShowNotification()
    {
        notify.enabled = true;
        yield return new WaitForSeconds(2f);
        notify.enabled = false;
    }
}
