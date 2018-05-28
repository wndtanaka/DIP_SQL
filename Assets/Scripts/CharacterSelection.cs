using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public string characterName;
    public string characterClass;
    public int characterLevel;

    public string[] characterList;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetCharacterDetails());
    }

    // this is for create a new character
    public void CreateNewCharacterButton()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    IEnumerator GetCharacterDetails()
    {
        // store characterSelectionURL as a string 
        string characterSelectionURL = "http://localhost/loginsystem/CharacterSelection.php";
        // create a new WWWForm()
        WWWForm createCharForm = new WWWForm();
        // match username_Post in PHP with loggedInUsername from GameManager
        createCharForm.AddField("username_Post", GameManager.LoggedInUsername);
        // create a new WWW() taking crteateCharURL and createCharForm
        WWW www = new WWW(characterSelectionURL, createCharForm);
        // return www
        yield return www;
        Debug.Log(www.text);
        // TODO let MySQL send userdata
    }
}
