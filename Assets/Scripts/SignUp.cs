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
    public InputField confirmPassword;

    public void NewSignUp()
    {
        if (Login.usernameList.Contains(newUsername.text))
        {
            Debug.Log("Nah Mate");
        }
    }
    public void Cancel()
    {
        SceneManager.LoadScene("Login");
    }
}
