using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon, Armour, Consumables, Misc
    }
    public ItemType itemType;
    public string name;

    public int amount = 1;

    public Sprite sprite;

    private void OnMouseEnter()
    {
        transform.parent.parent.GetComponent<InventoryController>().selectedItem = this.transform;
    }

    void OnMouseExit()
    {
        if (transform.parent.parent.GetComponent<InventoryController>() == null)
        {
            return;
        }
        if (transform.parent.parent.GetComponent<InventoryController>().canDrag == false)
        {
            transform.parent.parent.GetComponent<InventoryController>().selectedItem = null;
        }
    }
    public void IncreaseAmount(int amount)
    {
        this.amount += amount;
        transform.Find("Amount Text").GetComponent<Text>().text = this.amount.ToString();
    }
}
