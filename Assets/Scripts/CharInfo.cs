using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class CharInfo : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;

        [SerializeField]
        Text strengthText;
        [SerializeField]
        Text dexterityText;
        [SerializeField]
        Text intelligenceText;
        [SerializeField]
        Text wisdomText;
        [SerializeField]
        Text charismaText;
        [SerializeField]
        Text luckText;
        [SerializeField]
        Text bonusText;
        [SerializeField]
        Text levelText;

        public Button[] minusButtons;
        public Button[] plusButtons;

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

        public static int bonusPoint;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(GetCharacterInfo());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                OpenClose();
            }
            levelText.text = Player.currentLevel.ToString();
            bonusText.text = bonusPoint.ToString();
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
                else if (bonusPoint > 0)
                {
                    plusButton.interactable = true;
                }
            }
        }

        IEnumerator GetCharacterInfo()
        {
            // store characterSelectionURL as a string 
            string characterInfoURL = "http://localhost/loginsystem/CharacterInfo.php";
            // create a new WWWForm()
            WWWForm charInfoForm = new WWWForm();
            // match username_Post in PHP with loggedInUsername from GameManager
            charInfoForm.AddField("charName_Post", GameManager2.SelectedCharacterName);
            // create a new WWW() taking crteateCharURL and createCharForm
            WWW www = new WWW(characterInfoURL, charInfoForm);
            // return www
            yield return www;
            string[] stats = www.text.Split('|');
            strengthText.text = stats[0];
            strength = int.Parse(stats[0]);
            baseStrength = int.Parse(stats[0]);
            dexterityText.text = stats[1];
            dexterity = int.Parse(stats[1]);
            baseDexterity = int.Parse(stats[1]);
            intelligenceText.text = stats[2];
            intelligence = int.Parse(stats[2]);
            baseIntelligence = int.Parse(stats[2]);
            charismaText.text = stats[3];
            charisma = int.Parse(stats[3]);
            baseCharisma = int.Parse(stats[3]);
            wisdomText.text = stats[4];
            wisdom = int.Parse(stats[4]);
            baseWisdom = int.Parse(stats[4]);
            luckText.text = stats[5];
            luck = int.Parse(stats[5]);
            baseLuck = int.Parse(stats[5]);
        }

        void UpdateBonusStats()
        {

        }

        public void OpenClose()
        {
            canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
            canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
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
    }
}