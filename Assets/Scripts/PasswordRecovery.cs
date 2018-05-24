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
    private int codeLength = 8;
    private string myCode;
    #endregion

    private void Start()
    {
        // get RegisteredUsername InputFields component that will be passed to PasswordReset Username variable
        username = GameObject.Find("RegisteredUsername").GetComponent<InputField>();
        // get the Transform Component in children gameObject
        inputFields = transform.GetComponentsInChildren<InputField>();
        // select the first inputFields

        inputFields[0].Select();
    }

    // Toggling InputFields using Tab key and keep in track of InputFields number when user click the InputField
    private void Update()
    {
        // if one of the inputfield is empty then disabled the SignUpButton
        if (username.text == "" || email.text == "")
        {
            // set sendCodeButton to not interactable
            sendCodeButton.interactable = false;
            // sendCodeButton.interactable is false
            if (sendCodeButton.interactable == false)
            {
                // disable sendCodeButton EventTrigger
                sendCodeButton.GetComponent<EventTrigger>().enabled = false;
            }
        }
        // otherwise
        else
        {
            // set sendCodeButton to interactable
            sendCodeButton.interactable = true;
            // enable sendCodeButton EventTrigger
            sendCodeButton.GetComponent<EventTrigger>().enabled = true;
        }

        // when you press tab then it will give 1 int to TogglingInputFields so then it will incrementing the selected tab to 1
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // call TogglingInputFields taking int 1
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
            // if EventSystem.current.currentSelectedGameObject equal username.gameObject
            if (EventSystem.current.currentSelectedGameObject == username.gameObject)
            {
                // set currentInputField to zero
                currentInputField = 0;
            }
            // if EventSystem.current.currentSelectedGameObject equal email.gameObject
            if (EventSystem.current.currentSelectedGameObject == email.gameObject)
            {
                // set currentInputField to one
                currentInputField = 1;
            }
        }
    }

    // toggling inputfields
    void TogglingInputFields(int direction)
    {
        // increment currentInputField with direction
        currentInputField += direction;
        // if currentInputField is more inputFields.Length - 1
        if (currentInputField > inputFields.Length - 1)
        {
            // set currentInputField to zero
            currentInputField = 0;
        }
        // if currentInputField is less than zero
        if (currentInputField < 0)
        {
            // select the last index of inputFields;
            inputFields[inputFields.Length - 1].Select();
        }
        // select the inputFields array according to index
        inputFields[currentInputField].Select();
    }
    // forget / reset button check all the code input by player
    public void ForgetButton()
    {
        // call CheckingDetails()
        StartCoroutine(CheckingDetails());
        // call CheckDetailsNotification()
        StartCoroutine(CheckDetailsNotification());
    }
    // cancel button will take user to the Login scene
    public void CancelButton()
    {
        // load Login scene
        SceneManager.LoadScene("Login");
    }

    #region PHP
    IEnumerator CheckingDetails()
    {
        // store passwordRecoveryURL to string
        string passwordRecoveryURL = "http://localhost/loginsystem/passwordrecovery.php";
        // create a new WWWForm
        WWWForm detailsForm = new WWWForm();
        // match username_Post in PHP with username.text
        detailsForm.AddField("username_Post", username.text);
        // match email_Post in PHP with email.text
        detailsForm.AddField("email_Post", email.text);
        // create a new WWW taking passwordRecoveryURL and detailsForm
        WWW www = new WWW(passwordRecoveryURL, detailsForm);
        // return www
        yield return www;
        Debug.Log(www.text);
        // if www.text equal to userChecked
        if (www.text == userChecked)
        {
            // set canReset to true
            canReset = true;
            // change checkDetailsNotify text
            checkDetailsNotify.text = "Changing Password";
        }
        // if www.text equal to incorrectEmail or userNotFound
        if (www.text == incorrectEmail || www.text == userNotFound)
        {
            // set canReset to false
            canReset = false;
            // change checkDetailsNotify text
            checkDetailsNotify.text = "Invalid user details";
        }
    }
    // this function taking care of sending email syntax
    IEnumerator SendingEmail()
    {
        // loop through codeLength
        for (int i = 0; i < codeLength; i++)
        {   
            // adding a character to myCode randomly from codeChar array
            myCode += codeChar[Random.Range(0, codeChar.Length)];
        }
        // wait for 0.5f seconds
        yield return new WaitForSeconds(0.5f);

        // if canReset is true
        if (canReset)
        {
            // activate codePanel
            codePanel.SetActive(true);
            // create a new MailMessage
            MailMessage mail = new MailMessage();
            // create a new MailAddress sender
            mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
            // set the recipient 
            mail.To.Add(email.text);
            // set mail subject
            mail.Subject = "Reset Password";
            // set mail body
            mail.Body = "Reset your password by entering the code below\n\n\n" + myCode;

            // Simple Main Transfer Protocol
            // create a new SmtpClient
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            // set smtpServer port
            smtpServer.Port = 587; // TODO change port between 25,465,587 if it does not work
            // set smtp credentials
            smtpServer.Credentials = new NetworkCredential(mail.From.ToString(), sqlpassword) as ICredentialsByHost;
            // set smtpServer SSL to true
            smtpServer.EnableSsl = true;

            // delegating ServerCertificateValidationCallback
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors) { return true; };

            // send mail using smtpServer
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
        // enable checkDetailsNotify
        checkDetailsNotify.enabled = true;
        // wait for 2 seconds
        yield return new WaitForSeconds(2f);
        // disable checkDetailsNotify
        checkDetailsNotify.enabled = false;
    }
    // notify check error feedback
    IEnumerator CodeCheckNotification()
    {
        // enable codeNotify
        codeNotify.enabled = true;
        // wait for 2 seconds
        yield return new WaitForSeconds(2f);
        // disable codeNotify
        codeNotify.enabled = false;
    }

    // reset password button will send an email to the input email by user and also start a countdown timer before user can send another email.
    public void ResetButton()
    {
        // call SendingEmail()
        StartCoroutine(SendingEmail());
        // set resendTimer to 60 seconds
        Timer.resendTimer = 60f;
    }
    // verify a code that sent to the user's email
    public void SubmitCodeButton()
    {
        // call CodeVerify()
        CodeVerify();
    }
    // check if the input code is the same as generated code, then open the ChangePassword scene ontop of this scene. also reset myCode to null for error check
    // if code does not match, send feedback to user
    void CodeVerify()
    {
        // if verifyCode equal to myCode
        if (verifyCode.text == myCode)
        {
            Debug.Log("Code Verified");
            // deactivate codePanel
            codePanel.SetActive(false);
            //checkDetails.gameObject.SetActive(false);
            // load ChangePassword scene on top of current scene
            SceneManager.LoadSceneAsync("ChangePassword");
            // reset myCode
            myCode = null;
        }
        else
        {
            // change codeNotify
            codeNotify.text = "Invalid Code";
            // call CodeCheckNotification()
            StartCoroutine(CodeCheckNotification());
        }
    }
    // cancel verify button will close the panel
    public void CancelVerify()
    {
        // deactivate codePanel
        codePanel.SetActive(false);
    }
}
