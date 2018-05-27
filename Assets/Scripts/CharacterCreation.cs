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

    // this function interact with Button
    public void CreateCharacterButton()
    {
        // call CreateCharacter() passing loginUsername string
        StartCoroutine(CreateCharacter(loginUsername));
    }

    IEnumerator CreateCharacter(string username)
    {
        // store createCharURL as a string 
        string createCharURL = "http://localhost/loginsystem/CharacterCreation.php";
        // create a new WWWForm()
        WWWForm createCharForm = new WWWForm();
        // match username_Post in PHP with username
        createCharForm.AddField("username_Post", username);
        // match charName_Post in PHP with charName.text
        createCharForm.AddField("charName_Post", charName.text);
        // match charClass_Post in PHP with charClass
        createCharForm.AddField("charClass_Post", "Mage");
        // create a new WWW() taking crteateCharURL and createCharForm
        WWW www = new WWW(createCharURL, createCharForm);
        // return www
        yield return www;
        Debug.Log(www.text);
        // TODO let MySQL send userdata
    }
}