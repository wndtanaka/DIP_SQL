using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { MANA, HEALTH };

public class ItemList : MonoBehaviour
{
    public ItemType type;
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;

    public int maxSize;

    public void Use()
    {
        switch (type)
        {
            case ItemType.MANA:
                Debug.Log("Mana Potion Used.");
                break;
            case ItemType.HEALTH:
                Debug.Log("Health Potion Used.");
                break;
            default:
                break;
        }
    }
}
