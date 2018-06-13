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
    public Transform[] characterButton;

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
        //Debug.Log(www.text);
        string[] characters = www.text.Split('#');
        characterList = new string[characters.Length - 1];
        for (int i = 0; i < characterList.Length; i++)
        {
            characterList[i] = characters[i];
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
            if (i == 0)
            {
                if (characterList[i] == null)
                {
                    return;
                }
                string[] charOne = characterList[i].Split('|');

                Text charName = characterButton[i].FindChild("Character Name").GetComponent<Text>();
                Text charClass = characterButton[i].FindChild("Character Class").GetComponent<Text>();
                Text charLevel = characterButton[i].FindChild("Character Level").GetComponent<Text>();
                charName.text = charOne[0];
                charClass.text = charOne[1];
                charLevel.text = "Level: " + charOne[2];
            }
            else if (i == 1)
            {
                if (characterList[i] == null)
                {
                    return;
                }
                string[] charTwo = characterList[i].Split('|');
                Text charName = characterButton[i].FindChild("Character Name").GetComponent<Text>();
                Text charClass = characterButton[i].FindChild("Character Class").GetComponent<Text>();
                Text charLevel = characterButton[i].FindChild("Character Level").GetComponent<Text>();
                charName.text = charTwo[0];
                charClass.text = charTwo[1];
                charLevel.text = "Level: " + charTwo[2];
            }
            else if (i == 2)
            {
                if (characterList[i] == null)
                {
                    return;
                }
                string[] charThree = characterList[i].Split('|');
                Text charName = characterButton[i].FindChild("Character Name").GetComponent<Text>();
                Text charClass = characterButton[i].FindChild("Character Class").GetComponent<Text>();
                Text charLevel = characterButton[i].FindChild("Character Level").GetComponent<Text>();
                charName.text = charThree[0];
                charClass.text = charThree[1];
                charLevel.text = "Level: " + charThree[2];
            }
            else if (i == 3)
            {
                if (characterList[i] == null)
                {
                    return;
                }
                string[] charFour = characterList[i].Split('|');
                Text charName = characterButton[i].FindChild("Character Name").GetComponent<Text>();
                Text charClass = characterButton[i].FindChild("Character Class").GetComponent<Text>();
                Text charLevel = characterButton[i].FindChild("Character Level").GetComponent<Text>();
                charName.text = charFour[0];
                charClass.text = charFour[1];
                charLevel.text = "Level: " + charFour[2];
            }
            else if (i == 4)
            {
                if (characterList[i] == null)
                {
                    Debug.Log("ok");
                    return;
                }
                string[] charFive = characterList[i].Split('|');
                Text charName = characterButton[i].FindChild("Character Name").GetComponent<Text>();
                Text charClass = characterButton[i].FindChild("Character Class").GetComponent<Text>();
                Text charLevel = characterButton[i].FindChild("Character Level").GetComponent<Text>();
                charName.text = charFive[0];
                charClass.text = charFive[1];
                charLevel.text = "Level: " + charFive[2];
            }
        }





    }
}
