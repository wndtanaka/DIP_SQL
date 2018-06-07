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

    #region Stats
    [Header("Stats")]
    public int strength;
    public int dexterity;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public int luck;

    private int bonusPoint = 5;

    #endregion
    #region Stats Text
    [Header("Stats Text Component")]
    public Text strengthText;
    public Text dexterityText;
    public Text intelligenceText;
    public Text wisdomText;
    public Text charismaText;
    public Text luckText;
    #endregion

    #endregion

    void Start()
    {
        charClassText.text = charClass[0];
        characterClass = charClass[0];
        DefaultStats(0);
    }

    void Update()
    {
        strengthText.text = strength.ToString();
        dexterityText.text = dexterity.ToString();
        intelligenceText.text = intelligence.ToString();
        wisdomText.text = wisdom.ToString();
        charismaText.text = charisma.ToString();
        luckText.text = luck.ToString();
    }

    public void DefaultStats(int index)
    {
        switch (charClass[index])
        {
            case "Warrior":
                strength = 17;
                dexterity = 8;
                intelligence = 5;
                wisdom = 11;
                charisma = 9;
                luck = 10;
                break;
            case "Rogue":
                strength = 9;
                dexterity = 16;
                intelligence = 7;
                wisdom = 10;
                charisma = 6;
                luck = 12;
                break;
            case "Mage":
                strength = 6;
                dexterity = 6;
                intelligence = 18;
                wisdom = 10;
                charisma = 12;
                luck = 8;
                break;
            case "Priest":
                strength = 4;
                dexterity = 4;
                intelligence = 16;
                wisdom = 13;
                charisma = 12;
                luck = 11;
                break;
        }
    }

    public void BonusStats(int i)
    {
        bonusPoint--;
        if (bonusPoint <= 0)
        {
            // TODO 
        }
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
        DefaultStats(index);
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

    public void CancelButton()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}