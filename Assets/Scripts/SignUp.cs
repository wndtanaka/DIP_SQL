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

    //UserDetails user;

    //public void NewSignUp()
    //{
    //    if (user.usernameList.Contains(newUsername.text))
    //    {
    //        Debug.Log("Nah Mate");
    //    }
    //}
    public void Cancel()
    {
        SceneManager.LoadScene("Login");
    }
}
