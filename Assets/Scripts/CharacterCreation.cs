using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RPG
{
    public class CharacterCreation : MonoBehaviour
    {
        #region Variables
        public InputField charName;
        public string[] charClass = new string[] { "Samurai", "Ninja", "Sensei", "Geisha" };
        public Text charClassText;
        public GameObject[] characters;
        public Transform customChar;

        private int index = 0;
        private int colorIndex = 0;
        private string characterClass;
        private const int newLevel = 1;

        public static string charId;

        #region Stats

        [Header("Default Stats")]
        public int strength;
        public int dexterity;
        public int intelligence;
        public int wisdom;
        public int charisma;
        public int luck;

        [Header("Base Stats")]
        public int baseStrength;
        public int baseDexterity;
        public int baseIntelligence;
        public int baseWisdom;
        public int baseCharisma;
        public int baseLuck;

        public int bonusPoint = 5;

        #endregion
        #region Stats Text
        [Header("Stats Text Component")]
        public Text[] statsText;
        public Text strengthText;
        public Text dexterityText;
        public Text intelligenceText;
        public Text wisdomText;
        public Text charismaText;
        public Text luckText;
        public Text bonusText;

        public Button[] plusButtons;
        public Button[] minusButtons;
        #endregion

        #endregion

        void Start()
        {
            charClassText.text = charClass[0];
            characterClass = charClass[0];
            DefaultStats(0);
            CharacterCustomisation();
            characters[0].SetActive(true);
            customChar = characters[0].transform.GetChild(0);
            charId = index.ToString() + colorIndex;
        }

        void Update()
        {
            strengthText.text = strength.ToString();
            dexterityText.text = dexterity.ToString();
            intelligenceText.text = intelligence.ToString();
            wisdomText.text = wisdom.ToString();
            charismaText.text = charisma.ToString();
            luckText.text = luck.ToString();
            bonusText.text = bonusPoint.ToString();

            if (strength <= baseStrength)
            {
                minusButtons[0].interactable = false;
            }
            else if (strength > baseStrength)
            {
                minusButtons[0].interactable = true;
            }
            if (dexterity <= baseDexterity)
            {
                minusButtons[1].interactable = false;
            }
            else if (dexterity > baseDexterity)
            {
                minusButtons[1].interactable = true;
            }
            if (intelligence <= baseIntelligence)
            {
                minusButtons[2].interactable = false;
            }
            else if (intelligence > baseIntelligence)
            {
                minusButtons[2].interactable = true;
            }
            if (wisdom <= baseWisdom)
            {
                minusButtons[3].interactable = false;
            }
            else if (wisdom > baseWisdom)
            {
                minusButtons[3].interactable = true;
            }
            if (charisma <= baseCharisma)
            {
                minusButtons[4].interactable = false;
            }
            else if (charisma > baseCharisma)
            {
                minusButtons[4].interactable = true;
            }
            if (luck <= baseLuck)
            {
                minusButtons[5].interactable = false;
            }
            else if (luck > baseLuck)
            {
                minusButtons[5].interactable = true;
            }
            foreach (Button plusButton in plusButtons)
            {
                if (bonusPoint <= 0)
                {
                    plusButton.interactable = false;
                }
                else
                {
                    plusButton.interactable = true;
                }
            }
        }

        public void DefaultStats(int index)
        {
            switch (charClass[index])
            {
                case "Samurai":
                    strength = 17;
                    dexterity = 8;
                    intelligence = 5;
                    wisdom = 11;
                    charisma = 9;
                    luck = 10;
                    break;
                case "Ninja":
                    strength = 9;
                    dexterity = 16;
                    intelligence = 7;
                    wisdom = 10;
                    charisma = 6;
                    luck = 12;
                    break;
                case "Sensei":
                    strength = 6;
                    dexterity = 6;
                    intelligence = 18;
                    wisdom = 10;
                    charisma = 12;
                    luck = 8;
                    break;
                case "Geisha":
                    strength = 4;
                    dexterity = 4;
                    intelligence = 16;
                    wisdom = 13;
                    charisma = 12;
                    luck = 11;
                    break;
                default:
                    strength = 17;
                    dexterity = 8;
                    intelligence = 5;
                    wisdom = 11;
                    charisma = 9;
                    luck = 10;
                    break;
            }
            baseStrength = strength;
            baseDexterity = dexterity;
            baseIntelligence = intelligence;
            baseWisdom = wisdom;
            baseCharisma = charisma;
            baseLuck = luck;
            bonusPoint = 5;
        }

        public void BonusPlusStats(string statType)
        {
            int amount = 1;
            switch (statType)
            {
                case "Strength":
                    strength += amount;
                    bonusPoint -= amount;
                    break;
                case "Dexterity":
                    dexterity += amount;
                    bonusPoint -= amount;
                    break;
                case "Intelligence":
                    intelligence += amount;
                    bonusPoint -= amount;
                    break;
                case "Wisdom":
                    wisdom += amount;
                    bonusPoint -= amount;
                    break;
                case "Charisma":
                    charisma += amount;
                    bonusPoint -= amount;
                    break;
                case "Luck":
                    luck += amount;
                    bonusPoint -= amount;
                    break;
            }
        }
        public void BonusMinusStats(string statType)
        {
            int amount = -1;
            switch (statType)
            {
                case "Strength":
                    strength += amount;
                    bonusPoint -= amount;
                    break;
                case "Dexterity":
                    dexterity += amount;
                    bonusPoint -= amount;
                    break;
                case "Intelligence":
                    intelligence += amount;
                    bonusPoint -= amount;
                    break;
                case "Wisdom":
                    wisdom += amount;
                    bonusPoint -= amount;
                    break;
                case "Charisma":
                    charisma += amount;
                    bonusPoint -= amount;
                    break;
                case "Luck":
                    luck += amount;
                    bonusPoint -= amount;
                    break;
            }
        }
        public void TogglingClass(int amount)
        {
            index += amount;
            if (index < 0)
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
            charId = index.ToString() + colorIndex;
            DefaultStats(index);
            CharacterCustomisation();
        }

        // this function is for create the customised character
        public void CreateCharacterButton()
        {
            // call CreateCharacter() passing loginUsername string
            StartCoroutine(CreateCharacter(GameManager2.LoggedInUsername));
        }

        IEnumerator CreateCharacter(string username)
        {
            if (bonusPoint == 0)
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
                // match charId_Post in PHP with charLevel
                createCharForm.AddField("charId_Post", charId);
                createCharForm.AddField("str_Post", strength);
                createCharForm.AddField("dex_Post", dexterity);
                createCharForm.AddField("int_Post", intelligence);
                createCharForm.AddField("wis_Post", wisdom);
                createCharForm.AddField("cha_Post", charisma);
                createCharForm.AddField("luc_Post", luck);
                // create a new WWW() taking crteateCharURL and createCharForm
                WWW www = new WWW(createCharURL, createCharForm);
                // return www
                yield return www;
                Debug.Log(www.text);
                // Load CharacterSelection Scene
                SceneManager.LoadScene("CharacterSelection");
            }
            else
            {
                Debug.Log("Still have bonus points left");
            }
        }

        public void CancelButton()
        {
            // Load CharacterSelection Scene
            SceneManager.LoadScene("CharacterSelection");
        }

        public void CharacterCustomisation()
        {
            for (int i = 0; i < charClass.Length; i++)
            {
                characters[i].SetActive(false);
                if (index == i)
                {
                    characters[i].SetActive(true);
                }
            }
        }

        public void CharacterColor(int number)
        {
            colorIndex += number;

            if (colorIndex < 0)
            {
                colorIndex = 2;
            }
            else if (colorIndex > 2)
            {
                colorIndex = 0;
            }
            for (int i = 0; i < charClass.Length; i++)
            {
                if (index == i)
                {
                    if (colorIndex == 0)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            customChar = characters[index].transform.GetChild(j);
                            customChar.gameObject.SetActive(false);
                        }
                        customChar = characters[index].transform.GetChild(colorIndex);
                        customChar.gameObject.SetActive(true);
                        charId = index.ToString() + colorIndex;
                    }
                    if (colorIndex == 1)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            customChar = characters[index].transform.GetChild(j);
                            customChar.gameObject.SetActive(false);
                        }
                        customChar = characters[index].transform.GetChild(colorIndex);
                        customChar.gameObject.SetActive(true);
                        charId = index.ToString() + colorIndex;
                    }
                    if (colorIndex == 2)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            customChar = characters[index].transform.GetChild(j);
                            customChar.gameObject.SetActive(false);
                        }
                        customChar = characters[index].transform.GetChild(colorIndex);
                        customChar.gameObject.SetActive(true);
                        charId = index.ToString() + colorIndex;
                    }
                }
            }
        }

        public void RandomiseCharacter()
        {
            charClassText.text = charClass[Random.Range(0, charClass.Length)];
            index = Random.Range(0, charClass.Length);
            colorIndex = Random.Range(0, charClass.Length);
            DefaultStats(index);
            characterClass = charClass[index];
            CharacterColor(index);
            CharacterCustomisation();
        }
    }
}