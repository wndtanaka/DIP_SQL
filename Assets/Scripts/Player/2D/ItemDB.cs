using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public Sprite[] sprites;

    public static List<Item> allItems = new List<Item>();
    public static List<Item> sortedItems = new List<Item>();
    // Use this for initialization
    void Awake()
    {
        // CREATE ITEM
        Item item_001 = gameObject.AddComponent<Item>();
        item_001.name = "Sword";
        item_001.itemType = Item.ItemType.Weapon;
        item_001.sprite = sprites[0];
        allItems.Add(item_001);

        // CREATE ITEM
        Item item_002 = gameObject.AddComponent<Item>();
        item_002.name = "Bow";
        item_002.itemType = Item.ItemType.Weapon;
        item_002.sprite = sprites[1];
        allItems.Add(item_002);

        // CREATE ITEM
        Item item_003 = gameObject.AddComponent<Item>();
        item_003.itemType = Item.ItemType.Armour;
        item_003.sprite = sprites[2];
        item_003.name = "Helmet";
        allItems.Add(item_003);

        // CREATE ITEM
        Item item_004 = gameObject.AddComponent<Item>();
        item_004.name = "Armour";
        item_004.itemType = Item.ItemType.Armour;
        item_004.sprite = sprites[3];
        allItems.Add(item_004);

        // CREATE ITEM
        Item item_005 = gameObject.AddComponent<Item>();
        item_005.name = "Health Potion";
        item_005.itemType = Item.ItemType.Consumables;
        item_005.sprite = sprites[4];
        allItems.Add(item_005);
        allItems.Add(item_005);
        allItems.Add(item_005);

        SortedAllItems();
    }

    public void SortedAllItems()
    {
        sortedItems.Clear();
        foreach (Item i in allItems)
        {
            sortedItems.Add(i);
        }
    }

    public void SortItemsByType(string type)
    {
        sortedItems.Clear();
        foreach (Item i in allItems)
        {
            if (i.itemType.ToString() == type)
            {
                sortedItems.Add(i);
            }
        }
    }
}
