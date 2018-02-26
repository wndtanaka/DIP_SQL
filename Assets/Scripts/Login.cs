using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public static List<string> usernameList = new List<string>();
    public static List<string> passwordList = new List<string>();

    void Start()
    {
        usernameList.Add("admin");
        passwordList.Add("admin");
    }

    public void LoginInput()
    {
        for (int i = 0; i < usernameList.Count; i++)
        {
            if (username.text == "")
            {
                Debug.Log("Please put your username in the box");
            }
            else if (username.text == usernameList[i])
            {
                PasswordInput();
            }
            else
            {
                Debug.Log("Invalid username / password");
            }
        }
    }

    void PasswordInput()
    {
        for (int i = 0; i < passwordList.Count; i++)
        {
            if (password.text == "")
            {
                Debug.Log("Please put your password in the box");
            }
            else if (password.text == passwordList[i])
            {
                Debug.Log("Password Correct");
            }
            else
            {
                Debug.Log("Invalid username / password");
            }
        }
    }

    public void SignUp()
    {
        SceneManager.LoadScene("SignUp");
    }
}
