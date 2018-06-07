using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public class Character
    {
        public string username
        {
            get;
            set;
        }
        public string charName
        {
            get;
            set;
        }
        public string charClass
        {
            get;
            set;
        }
        public int charLevel
        {
            get;
            set;
        }
        public Character(string charName, string charClass, int charLevel)
        {
            //this.username = username;
            this.charName = charName;
            this.charClass = charClass;
            this.charLevel = charLevel;
        }
    }

    public string characterName;
    public string characterClass;
    public int characterLevel;

    public string[] characterList;
    public bool isLoaded;
    public Dictionary<string, Character> character = new Dictionary<string, Character>();

    public Text characterCount;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetCharacterDetails());
    }

    // this is for create a new character
    public void CreateNewCharacterButton()
    {
        if (characterList.Length >= 5)
        {
            // TODO cant create anymore
        }
        else
        {
            SceneManager.LoadScene("CharacterCreation");
        }
    }
    public void BackToMainMenuButton()
    {
        SceneManager.LoadScene("Login");
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
        string[] characters = www.text.Split('#');
        characterList = new string[characters.Length - 1];
        for (int i = 0; i < characterList.Length; i++)
        {
            characterList[i] = characters[i];
            Debug.Log(characterList[i]);
        }
        CharacterCount();
        SetCharacters();
        isLoaded = true;
    }
    void CharacterCount()
    {
        characterCount.text = "Character Count: " + characterList.Length.ToString();
    }
    void SetCharacters()
    {
        for (int i = 0; i < characterList.Length; i++)
        {
            string[] current = characterList[i].Split('|');
            Character chars = new Character(current[0], current[1], int.Parse(current[2]));
            character.Add(chars.username, chars);
        }
        isLoaded = true;
    }
}
