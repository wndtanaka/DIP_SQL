using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterCreation : MonoBehaviour
{
    #region Variables
    public InputField charName;

    private string loginUsername = "wendi";
    #endregion
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateCharacterButton()

    {

        StartCoroutine(CreateCharacter(loginUsername));

    }

    IEnumerator CreateCharacter(string username)
    {
        string loginURL = "http://localhost/loginsystem/CharacterCreation.php";
        WWWForm loginForm = new WWWForm();
        loginForm.AddField("username_Post", username);
        loginForm.AddField("charName_Post", charName.text);
        loginForm.AddField("charClass_Post", "Mage");
        WWW www = new WWW(loginURL, loginForm);
        yield return www;
        Debug.Log(www.text);
        // TODO let MySQL send userdata
    }
}