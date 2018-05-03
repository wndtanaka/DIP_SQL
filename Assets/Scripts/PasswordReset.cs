using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PasswordReset : MonoBehaviour
{
    #region Variables
    private string m_Username;
    public string Username
    {
        get
        {
            if (m_Username == null)
            {
                m_Username = PasswordRecovery.username.text;
            }
            return m_Username;
        }
    }

    public InputField newPassword;
    public InputField confirmNewPassword;
    public Button passwordResetButton;
    public Text changePasswordNotify;

    private int currentInputField;
    private InputField[] inputFields;
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
        if (newPassword.text == "" || confirmNewPassword.text == "")
        {
            passwordResetButton.interactable = false;
            if (passwordResetButton.interactable == false)
            {
                passwordResetButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            passwordResetButton.interactable = true;
            passwordResetButton.GetComponent<EventTrigger>().enabled = true;
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
            if (EventSystem.current.currentSelectedGameObject == newPassword.gameObject)
            {
                currentInputField = 0;
            }
            if (EventSystem.current.currentSelectedGameObject == confirmNewPassword.gameObject)
            {
                currentInputField = 1;
            }
        }
    }
    // toggling inputfields
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
    #region PHP
    IEnumerator ChangingPassword()
    {
        string changePasswrordURL = "http://localhost/loginsystem/changepassword.php";
        WWWForm detailsForm = new WWWForm();
        detailsForm.AddField("username_Post", Username);
        detailsForm.AddField("password_Post", newPassword.text);
        WWW www = new WWW(changePasswrordURL, detailsForm);
        yield return www;
        Debug.Log(www.text);
    }
    #endregion

    // button click, checking if the new password matches the confirm password, then able to update password in SQL and also takes user back to the login screen after certain time.\
    // or else, let the user know that the confirm password does not match with the new password
    // TODO put password criteria
    public void ChangePasswordButton()
    {
        if (newPassword.text == confirmNewPassword.text)
        {
            StartCoroutine(ChangingPassword());
            StartCoroutine(LoginScreenDelay());
        }
        else
        {
            changePasswordNotify.text = "Password does not match";
            StartCoroutine(ChangePasswordNotification());
        }
    }
    // notify user of feedback
    IEnumerator ChangePasswordNotification()
    {
        changePasswordNotify.enabled = true;
        yield return new WaitForSeconds(2f);
        changePasswordNotify.enabled = false;
    }
    // take user back to Login scene in 5 seconds after password changed
    IEnumerator LoginScreenDelay()
    {
        CountDownTimer.canStartCount = true;
        changePasswordNotify.gameObject.SetActive(true);
        yield return new WaitForSeconds(CountDownTimer.countDownTimer);
        SceneManager.LoadScene("Login");
    }
    //take user back to Login scene without changing anything
    public void CancelButton()
    {
        SceneManager.LoadScene("Login");
    }
}
