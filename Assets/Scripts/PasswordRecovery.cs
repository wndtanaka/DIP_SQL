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
    #region Variables
    public static InputField username;
    public InputField email;
    public InputField verifyCode;

    public Button sendCodeButton;
    public Text checkDetailsNotify;
    public Text codeNotify;

    //public Image checkDetails;
    public GameObject codePanel;

    private int currentInputField;
    private InputField[] inputFields;
    private bool canReset = false;
    #endregion

    #region String Text
    private string userChecked = "User Checked";
    private string userNotFound = "User Not Found";
    private string incorrectEmail = "Incorrect Email";
    private string sqlpassword = "sqlpassword";

    private const string codeChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want
    private int codeLength = 6;
    private string myCode;
    #endregion

    private void Start()
    {
        // get RegisteredUsername InputFields component that will be passed to PasswordReset Username variable
        username = GameObject.Find("RegisteredUsername").GetComponent<InputField>();
        inputFields = transform.GetComponentsInChildren<InputField>();
        inputFields[0].Select();
    }

    // Toggling InputFields using Tab key and keep in track of InputFields number when user click the InputField
    private void Update()
    {
        if (username.text == "" || email.text == "")
        {
            sendCodeButton.interactable = false;
            if (sendCodeButton.interactable == false)
            {
                sendCodeButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        else
        {
            sendCodeButton.interactable = true;
            sendCodeButton.GetComponent<EventTrigger>().enabled = true;
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
            if (EventSystem.current.currentSelectedGameObject == email.gameObject)
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
    // forget / reset button check all the code input by player
    public void ForgetButton()
    {
        StartCoroutine(CheckingDetails());
        StartCoroutine(CheckDetailsNotification());
    }
    // cancel button will take user to the Login scene
    public void CancelButton()
    {
        SceneManager.LoadScene("Login");
    }

    #region PHP
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

    IEnumerator SendingEmail()
    {
        for (int i = 0; i < codeLength; i++)
        {
            myCode += codeChar[Random.Range(0, codeChar.Length)];
        }


        yield return new WaitForSeconds(0.5f);

        if (canReset)
        {
            codePanel.SetActive(true);
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
    #endregion

    // notify check details feedback
    IEnumerator CheckDetailsNotification()
    {
        checkDetailsNotify.enabled = true;
        yield return new WaitForSeconds(2f);
        checkDetailsNotify.enabled = false;
    }
    // notify check error feedback
    IEnumerator CodeCheckNotification()
    {
        codeNotify.enabled = true;
        yield return new WaitForSeconds(2f);
        codeNotify.enabled = false;
    }

    // reset password button will send an email to the input email by user and also start a countdown timer before user can send another email.
    public void ResetButton()
    {
        StartCoroutine(SendingEmail());
        Timer.resendTimer = 60f;
    }
    // verify a code that sent to the user's email
    public void SubmitCodeButton()
    {
        CodeVerify();
    }
    // check if the input code is the same as generated code, then open the ChangePassword scene ontop of this scene. also reset myCode to null for error check
    // if code does not match, send feedback to user
    void CodeVerify()
    {
        if (verifyCode.text == myCode)
        {
            Debug.Log("Code Verified");
            codePanel.SetActive(false);
            //checkDetails.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("ChangePassword");
            myCode = null;
        }
        else
        {
            codeNotify.text = "Invalid Code";
            StartCoroutine(CodeCheckNotification());
        }
    }
    // cancel verify button will close the panel
    public void CancelVerify()
    {
        codePanel.SetActive(false);
    }
}

/*
TODO

*/
