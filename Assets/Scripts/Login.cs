using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Login : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public Text notify;

    #region String Text
    private bool canLogin = false;
    private string loginSuccess = "Login Success";
    private string incorrectUsername = "User Not Found";
    private string incorrectPassword = "Password Incorrect";
    #endregion

    private int currentInputField;
    private InputField[] inputFields;

    private void Start()
    {
        inputFields = transform.GetComponentsInChildren<InputField>();
        inputFields[0].Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(UserLogin());
            StartCoroutine(ShowNotification());
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglingInputFields(1);
        }

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

    public void LoginButton()
    {
        StartCoroutine(UserLogin());
        StartCoroutine(ShowNotification());
    }

    public void SignUpButton()
    {
        SceneManager.LoadScene("SignUp");
    }

    public void ForgotPassword()
    {
        SceneManager.LoadScene("ForgetLogin");
    }

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

    IEnumerator ShowNotification()
    {
        notify.enabled = true;
        yield return new WaitForSeconds(2f);
        notify.enabled = false;
    }
}
