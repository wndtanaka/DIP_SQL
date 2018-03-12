using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    public InputField newUsername;
    public InputField newEmail;
    public InputField newPassword;

    public Text notify;

    void Start()
    {
        notify.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CreateAccount(newUsername.text, newEmail.text, newPassword.text));
            StartCoroutine(ShowNotification());
        }
    }

    public void CreateAccountButton()
    {
        StartCoroutine(CreateAccount(newUsername.text, newEmail.text, newPassword.text));
        StartCoroutine(ShowNotification());
    }

    IEnumerator CreateAccount(string username, string email, string password)
    {
        string createUserURL = "http://localhost/loginsystem/insertuser.php";
        WWWForm user = new WWWForm();
        user.AddField("username_Post", username);
        user.AddField("email_Post", email);
        user.AddField("password_Post", password);
        WWW www = new WWW(createUserURL, user);
        yield return www;
        if (www.text == "Create First User" || www.text == username + "Created User")
        {
            notify.text = "Your account is created";
        }
        else if (www.text == username + "User Already Exists")
        {
            notify.text = "Username is taken, please choose another username";
        }
        else if (www.text == username + "Email Already Exists")
        {
            notify.text = email + " has been registered.";
        }
    }

    public void Cancel()
    {
        SceneManager.LoadScene("Login");
    }

    IEnumerator ShowNotification()
    {
        notify.enabled = true;
        yield return new WaitForSeconds(2f);
        notify.enabled = false;

    }
}
