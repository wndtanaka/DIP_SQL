using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PasswordRecovery : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField newPassword;
    public InputField confirmNewPassword;

    public Text notify;

    public Image checkDetails;
    public Image changePassword;

    #region String Text
    private string userChecked = "User Checked";
    private string userNotFound = "User Not Found";
    private string incorrectEmail = "Incorrect Email";
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
            if (EventSystem.current.currentSelectedGameObject == email.gameObject)
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

    public void ForgetButton()
    {
        StartCoroutine(CheckingDetails());
        StartCoroutine(ShowNotification());
    }
    public void ChangePasswordButton()
    {
        if (newPassword.text == confirmNewPassword.text)
        {
            StartCoroutine(ChangingPassword());
        }
        else
        {
            // TODO notify
        }
        //StartCoroutine(ShowNotification());
    }

    public void CancelButton()
    {
        SceneManager.LoadScene("Login");
    }

    IEnumerator CheckingDetails()
    {
        string passwordRecoveryURL = "http://localhost/loginsystem/passwordrecovery.php";
        WWWForm detailsForm = new WWWForm();
        detailsForm.AddField("username_Post", username.text);
        detailsForm.AddField("email_Post", email.text);
        WWW www = new WWW(passwordRecoveryURL, detailsForm);
        yield return www;
        Debug.Log(www.text);
        if (www.text == userChecked)
        {
            notify.text = "Changing Password";
            checkDetails.gameObject.SetActive(false);
            changePassword.gameObject.SetActive(true);
        }
        if (www.text == incorrectEmail)
        {
            notify.text = "Incorrect email";
        }
        if (www.text == userNotFound)
        {
            notify.text = "User not found";
        }
    }

    IEnumerator ChangingPassword()
    {
        string changePasswrordURL = "http://localhost/loginsystem/changepassword.php";
        WWWForm detailsForm = new WWWForm();
        detailsForm.AddField("username_Post", username.text);
        detailsForm.AddField("password_Post", newPassword.text);
        //detailsForm.AddField("newpassword_Post", confirmNewPassword.text);
        WWW www = new WWW(changePasswrordURL, detailsForm);
        yield return www;
        Debug.Log(www.text);
    }

    IEnumerator ShowNotification()
    {
        notify.enabled = true;
        yield return new WaitForSeconds(2f);
        notify.enabled = false;
    }
}
