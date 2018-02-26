using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public UserDetails<string> detail = new UserDetails<string>();
    public List<string> passwordList = new List<string>();

    void Start()
    {
        detail.Add("admin");
    }

    public void LoginInput()
    {
        for (int i = 0; i < detail.amount; i++)
        {
            if (username.text == "")
            {
                Debug.Log("Please put your username in the box");
            }
            else if (username.text == detail.data[i])
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
