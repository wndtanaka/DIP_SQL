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
        if (newPassword.text == "" || confirmNewPassword.text == "")
        {
            // set the button interactable is false
            passwordResetButton.interactable = false;
            // if the button interactable is false
            if (passwordResetButton.interactable == false)
            {
                // disabled passwordResetButton EventTrigger component
                passwordResetButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            // set the passwordResetButtin interactable to true
            passwordResetButton.interactable = true;
            // enabled passwordResetButton EventTrigger component
            passwordResetButton.GetComponent<EventTrigger>().enabled = true;
        }
        // when you press tab then it will give 1 int to TogglingInputFields so then it will incrementing the selected tab to 1
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // call TogglingInputFields taking integer 1
            TogglingInputFields(1);
        }
        // checking the inputfield position when clicked with mouse
        if (Input.GetMouseButtonDown(0))
        {
            // if EventSystem.current.currentSelectedGameObject is null
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                // return
                return;
            }
            // if EventSystem.current.currentSelectedGameObject equal newPassword.gameObject
            if (EventSystem.current.currentSelectedGameObject == newPassword.gameObject)
            {
                // set currentInputField to zero
                currentInputField = 0;
            }
            // if EventSystem.current.currentSelectedGameObject is confirmNewPassword.gameObject
            if (EventSystem.current.currentSelectedGameObject == confirmNewPassword.gameObject)
            {
                // set currentInputField to one
                currentInputField = 1;
            }
        }
    }
    // toggling inputfields
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
    #region PHP
    IEnumerator ChangingPassword()
    {
        // storing changePassworldURL as string
        string changePasswordURL = "http://localhost/loginsystem/changepassword.php";
        // create a new WWWForm
        WWWForm detailsForm = new WWWForm();
        // match username_Post in PHP with username.text
        detailsForm.AddField("username_Post", Username);
        // match password_Post in PHP with newPassword.text
        detailsForm.AddField("password_Post", newPassword.text);
        // create a new WWW taking changePasswordURL and detailsForm
        WWW www = new WWW(changePasswordURL, detailsForm);
        // return www
        yield return www;
        Debug.Log(www.text);
    }
    #endregion

    // button click, checking if the new password matches the confirm password, then able to update password in SQL and also takes user back to the login screen after certain time.\
    // or else, let the user know that the confirm password does not match with the new password
    // TODO put password criteria
    public void ChangePasswordButton()
    {
        // if new password equal to confirm new password
        if (newPassword.text == confirmNewPassword.text)
        {
            // call ChangingPassword()
            StartCoroutine(ChangingPassword());
            // call LoginScreenDelay()
            StartCoroutine(LoginScreenDelay());
        }
        else
        {
            // change notify text
            changePasswordNotify.text = "Password does not match";
            // call ChangePasswordNotification()
            StartCoroutine(ChangePasswordNotification());
        }
    }
    // notify user of feedback
    IEnumerator ChangePasswordNotification()
    {
        // enable changePasswordNotify
        changePasswordNotify.enabled = true;
        // wait for 2 seconds
        yield return new WaitForSeconds(2f);
        // disable changePasswordNotify
        changePasswordNotify.enabled = false;
    }
    // take user back to Login scene in 5 seconds after password changed
    IEnumerator LoginScreenDelay()
    {
        // set canStartCount from CountDownTimer to true
        CountDownTimer.canStartCount = true;
        // activate changePasswordNotify gameObject
        changePasswordNotify.gameObject.SetActive(true);
        // wait for CountDownTimer.countDownTimer
        yield return new WaitForSeconds(CountDownTimer.countDownTimer);
        // load Login scene
        SceneManager.LoadScene("Login");
    }
    //take user back to Login scene without changing anything
    public void CancelButton()
    {
        // load Login scene
        SceneManager.LoadScene("Login");
    }
}
