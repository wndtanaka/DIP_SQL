using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class PasswordRecovery : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField newPassword;
    public InputField confirmNewPassword;
    public InputField verifyCode;

    public Text checkDetailsNotify;
    public Text changePasswordNotify;

    public Image checkDetails;
    public Image changePassword;
    public GameObject codePanel;

    #region String Text
    private string userChecked = "User Checked";
    private string userNotFound = "User Not Found";
    private string incorrectEmail = "Incorrect Email";
    private string sqlpassword = "sqlpassword";

    private const string codeChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want
    private int codeLength = 6;
    private string myCode;
    #endregion

    private int currentInputField;
    private InputField[] inputFields;
    private bool canReset = false;


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
        StartCoroutine(CheckDetailsNotification());
    }
    public void ChangePasswordButton()
    {
        if (newPassword.text == confirmNewPassword.text)
        {
            changePasswordNotify.text = "Password changed";
            StartCoroutine(ChangingPassword());
            StartCoroutine(ChangePasswordNotification());
        }
        else
        {
            changePasswordNotify.text = "Password does not match";
            StartCoroutine(ChangePasswordNotification());
        }
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
            canReset = true;
            checkDetailsNotify.text = "Changing Password";
        }
        if (www.text == incorrectEmail || www.text == userNotFound)
        {
            canReset = false;
            checkDetailsNotify.text = "Invalid user details";
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

    IEnumerator CheckDetailsNotification()
    {
        checkDetailsNotify.enabled = true;
        yield return new WaitForSeconds(2f);
        checkDetailsNotify.enabled = false;
    }
    IEnumerator ChangePasswordNotification()
    {
        checkDetailsNotify.enabled = true;
        yield return new WaitForSeconds(2f);
        checkDetailsNotify.enabled = false;
    }

    public void ResetButton()
    {
        StartCoroutine(SendingEmail());
    }

    IEnumerator SendingEmail()
    {
        for (int i = 0; i < codeLength; i++)
        {
            myCode += codeChar[Random.Range(0, codeChar.Length)];
        }
        codePanel.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        if (canReset)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
            mail.To.Add(email.text); // TODO, email from user
            mail.Subject = "Reset Password";
            mail.Body = "Reset you password by entering the code below\n\n\n" + myCode;

            // Simple Main Transfer Protocol
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 25;
            smtpServer.Credentials = new NetworkCredential(mail.From.ToString(), sqlpassword) as ICredentialsByHost;
            smtpServer.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors) { return true; };

            smtpServer.Send(mail);

            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Nope");
        }
    }
    public void SubmitCodeButton()
    {
        CodeVerify();
    }

    void CodeVerify()
    {
        if (verifyCode.text == myCode)
        {
            Debug.Log("Code Verified");
            codePanel.SetActive(false);
            checkDetails.gameObject.SetActive(false);
            changePassword.gameObject.SetActive(true);
            myCode = null;
        }
        else
        {
            Debug.Log("Wrong Code");
        }
    }
}

/*
TODO

*/