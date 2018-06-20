using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    [SerializeField]
    CharacterID[] characterId;

    #region Characters Container
    private string characterOneName;
    private string characterOneClass;
    private string characterOneLevel;
    private string characterTwoName;
    private string characterTwoClass;
    private string characterTwoLevel;
    private string characterThreeName;
    private string characterThreeClass;
    private string characterThreeLevel;
    private string characterFourName;
    private string characterFourClass;
    private string characterFourLevel;
    private string characterFiveName;
    private string characterFiveClass;
    private string characterFiveLevel;
    private string characterOneId;
    private string characterTwoId;
    private string characterThreeId;
    private string characterFourId;
    private string characterFiveId;
    #endregion
    [SerializeField]
    GameObject[] characterRenderer;
    [SerializeField]
    RenderTexture[] rend;
    [SerializeField]
    RawImage charTexture;

    string selectedCharacterName;
    string selectedCharacterClass;
    string selectedCharacterLevel;

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
        string[] characters = www.text.Split('#');
        characterList = new string[characters.Length - 1];
        for (int i = 0; i < characterList.Length; i++)
        {
            characterList[i] = characters[i];
        }
        CharacterCount();
        SetCharacters();
        ShowCharacter();
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
                characterOneId = charOne[3];
                characterOneName = charName.text;
                characterOneClass = charClass.text;
                characterOneLevel = charLevel.text;

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
                characterTwoId = charTwo[3];
                characterTwoName = charName.text;
                characterTwoClass = charClass.text;
                characterTwoLevel = charLevel.text;

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
                characterThreeId = charThree[3];
                characterThreeName = charName.text;
                characterThreeClass = charClass.text;
                characterThreeLevel = charLevel.text;

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
                characterFourId = charFour[3];
                characterFourName = charName.text;
                characterFourClass = charClass.text;
                characterFourLevel = charLevel.text;

            }
            else if (i == 4)
            {
                if (characterList[i] == null)
                {
                    return;
                }
                string[] charFive = characterList[i].Split('|');
                Text charName = characterButton[i].FindChild("Character Name").GetComponent<Text>();
                Text charClass = characterButton[i].FindChild("Character Class").GetComponent<Text>();
                Text charLevel = characterButton[i].FindChild("Character Level").GetComponent<Text>();
                charName.text = charFive[0];
                charClass.text = charFive[1];
                charLevel.text = "Level: " + charFive[2];
                characterFiveId = charFive[3];
                characterFiveName = charName.text;
                characterFiveClass = charClass.text;
                characterFiveLevel = charLevel.text;
            }
        }
    }

    public void GetCharacters()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        switch (go.name)
        {
            case "Character Button 1":
                selectedCharacterName = characterOneName;
                selectedCharacterClass = characterOneClass;
                selectedCharacterLevel = characterOneLevel;
                charTexture.texture = rend[0];
                break;
            case "Character Button 2":
                selectedCharacterName = characterTwoName;
                selectedCharacterClass = characterTwoClass;
                selectedCharacterLevel = characterTwoLevel;
                charTexture.texture = rend[1];
                break;
            case "Character Button 3":
                selectedCharacterName = characterThreeName;
                selectedCharacterClass = characterThreeClass;
                selectedCharacterLevel = characterThreeLevel;
                charTexture.texture = rend[2];
                break;
            case "Character Button 4":
                selectedCharacterName = characterFourName;
                selectedCharacterClass = characterFourClass;
                selectedCharacterLevel = characterFourLevel;
                charTexture.texture = rend[3];
                break;
            case "Character Button 5":
                selectedCharacterName = characterFiveName;
                selectedCharacterClass = characterFiveClass;
                selectedCharacterLevel = characterFiveLevel;
                charTexture.texture = rend[4];
                break;
            default:
                break;
        }
        CharacterSnapShot(go.name);
    }
    public void SelectCharacter()
    {
        //Debug.Log(selectedCharacterName);
        GameManager.SelectedCharacterName = selectedCharacterName;
        if (selectedCharacterName != null)
        {
            SceneManager.LoadScene("RPG");
        }
        else
        {
            Debug.Log("Select Character before play");
        }
    }

    public void ShowCharacter()
    {
        if (characterOneId != null)
        {
            CharacterID idOne = System.Array.Find(characterId, c => c.CharId == characterOneId);
            GameObject goOne = Instantiate(idOne.CharacterPrefab);
            goOne.transform.SetParent(GameObject.Find("Character One").transform);
            goOne.transform.position = GameObject.Find("Character One").transform.position;
        }

        if (characterTwoId != null)
        {
            CharacterID idTwo = System.Array.Find(characterId, c => c.CharId == characterTwoId);
            GameObject goTwo = Instantiate(idTwo.CharacterPrefab);
            goTwo.transform.SetParent(GameObject.Find("Character Two").transform);
            goTwo.transform.position = GameObject.Find("Character Two").transform.position;
        }
        if (characterThreeId != null)
        {
            CharacterID idThree = System.Array.Find(characterId, c => c.CharId == characterThreeId);
            GameObject goThree = Instantiate(idThree.CharacterPrefab);
            goThree.transform.SetParent(GameObject.Find("Character Three").transform);
            goThree.transform.position = GameObject.Find("Character Three").transform.position;
        }
        if (characterFourId != null)
        {
            CharacterID idFour = System.Array.Find(characterId, c => c.CharId == characterFourId);
            GameObject goFour = Instantiate(idFour.CharacterPrefab);
            goFour.transform.SetParent(GameObject.Find("Character Four").transform);
            goFour.transform.position = GameObject.Find("Character Four").transform.position;
        }
        if (characterFiveId != null)
        {
            CharacterID idFive = System.Array.Find(characterId, c => c.CharId == characterFiveId);
            GameObject goFive = Instantiate(idFive.CharacterPrefab);
            goFive.transform.SetParent(GameObject.Find("Character Five").transform);
            goFive.transform.position = GameObject.Find("Character Five").transform.position;
        }
    }

    public void CharacterSnapShot(string name)
    {
        for (int i = 0; i < characterRenderer.Length; i++)
        {
            characterRenderer[i].SetActive(false);
        }
        switch (name)
        {
            case "Character Button 1":
                characterRenderer[0].SetActive(true);
                break;
            case "Character Button 2":
                characterRenderer[1].SetActive(true);
                break;
            case "Character Button 3":
                characterRenderer[2].SetActive(true);
                break;
            case "Character Button 4":
                characterRenderer[3].SetActive(true);
                break;
            case "Character Button 5":
                characterRenderer[4].SetActive(true);
                break;
            default:
                break;
        }

    }

    [System.Serializable]
    public class CharacterID
    {
        [SerializeField]
        string charId;
        [SerializeField]
        GameObject characterPrefab;

        public string CharId
        {
            get
            {
                return charId;
            }
        }

        public GameObject CharacterPrefab
        {
            get
            {
                return characterPrefab;
            }
        }
    }
}
