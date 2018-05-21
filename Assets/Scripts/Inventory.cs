using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public string[] itemList;
    public bool isLoaded;

    public List<Item> item;
    public Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();

    private Vector2 scr;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadItemData());
    }

    void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        if (isLoaded)
        {
            GUI.Box(new Rect(scr.x * 0, scr.y * 0, scr.x * 16, scr.y), "Loading...");
        }
    }

    IEnumerator LoadItemData()
    {
        WWW itemDataURL = new WWW("localhost/loginsystem/ItemData.php");
        yield return itemDataURL;
        string textDataString = itemDataURL.text;
        Debug.Log(textDataString);
        string[] items = textDataString.Split('#');
        itemList = new string[items.Length - 1];
        for (int i = 0; i < itemList.Length; i++)
        {
            itemList[i] = items[i];
        }
        SetItems();
    }
    void SetItems()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            string[] current = itemList[i].Split('|');
            Weapon weapon = new Weapon(int.Parse(current[0]), current[1], int.Parse(current[2]), int.Parse(current[3]), float.Parse(current[4]), float.Parse(current[5]), current[6], current[7], current[8]);
            weapons.Add(weapon.id, weapon);
            Debug.Log(weapons[i].name);
        }
        isLoaded = true;
    }
}
