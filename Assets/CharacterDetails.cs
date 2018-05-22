using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetails : MonoBehaviour
{
    public string[] charClass = new string[] { "Warrior", "Rogue", "Mage", "Priest" };

    int index = 0;
    public Text charClassText;

    void Start()
    { 
        charClassText.text = charClass[0];
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
    }
}
