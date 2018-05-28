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
    public string[] charClass = new string[] { "Warrior", "Rogue", "Mage", "Priest" };
    public Text charClassText;

    private int index = 0;
    private string characterClass;
    private const int newLevel = 1;
    #endregion

    void Start()
    {
        charClassText.text = charClass[0];
        characterClass = charClass[0];
    }

    public void TogglingClass(int amount)
    {
        index += amount;
        if (index <= 0)
        {
            index = charClass.Length - 1;
            charClassText.text = charClass[index];
        }
        else if (index < charClass.Length)
        {
            charClassText.text = charClass[index];
        }
        else if (index >= charClass.Length)
        {
            index = 0;
            charClassText.text = charClass[index];
        }
        characterClass = charClass[index];
    }

    // this function is for create the customised character
    public void CreateCharacterButton()
    {
        // call CreateCharacter() passing loginUsername string
        StartCoroutine(CreateCharacter(GameManager.LoggedInUsername));
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
        createCharForm.AddField("charClass_Post", characterClass);
        // match charLevel_Post in PHP with charLevel
        createCharForm.AddField("charLevel_Post", newLevel);
        // create a new WWW() taking crteateCharURL and createCharForm
        WWW www = new WWW(createCharURL, createCharForm);
        // return www
        yield return www;
        Debug.Log(www.text);
        // TODO let MySQL send userdata
    }
}