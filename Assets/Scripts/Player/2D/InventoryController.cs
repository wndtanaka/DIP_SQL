using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Transform selectedItem, selectedSlot, originSlot, backGround;

    public GameObject slotPrefab, itemPrefab;
    public Vector2 inventorySize = new Vector2(4, 6);
    public float slotSize;
    public Vector2 windowSize;

    public bool canDrag;

    public static List<Item> itemList = new List<Item>();

    // Use this for initialization
    void Start()
    {
        CreateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedItem != null)
        {
            canDrag = true;
            originSlot = selectedItem.parent;
            selectedItem.GetComponent<Collider>().enabled = false;
            SetItemColliders(false);
        }

        if (Input.GetMouseButton(0) && selectedItem != null && canDrag)
        {
            selectedItem.position = Input.mousePosition;
            selectedItem.transform.SetParent(backGround);
        }
        else if (Input.GetMouseButtonUp(0) && selectedItem != null)
        {
            canDrag = false;
            SetItemColliders(true);
            if (selectedSlot == null)
            {
                selectedItem.SetParent(originSlot);
            }
            else if (selectedSlot.childCount > 0)
            {
                // stack items
                if (selectedItem.name == selectedSlot.GetChild(0).name && (selectedItem.GetComponent<Item>().itemType == Item.ItemType.Consumables || selectedItem.GetComponent<Item>().itemType == Item.ItemType.Misc))
                {
                    selectedItem.GetComponent<Item>().IncreaseAmount(selectedSlot.GetChild(0).GetComponent<Item>().amount);
                    Destroy(selectedSlot.GetChild(0).gameObject);
                }
                // swap items
                else
                {
                    selectedSlot.GetChild(0).SetParent(originSlot);
                    foreach (Transform t in originSlot)
                    {
                        t.localPosition = Vector3.zero;
                    }
                    //originSlot.GetChild(0).localPosition = Vector3.zero;
                }
                selectedItem.SetParent(selectedSlot);
            }
            else if (selectedSlot.childCount == 0)
            {
                selectedItem.SetParent(selectedSlot);
            }
            selectedItem.localPosition = Vector3.zero;
            selectedItem.GetComponent<Collider>().enabled = true;
            selectedItem = null;
        }
    }

    void SetItemColliders(bool b)
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            item.GetComponent<Collider>().enabled = b;
        }
    }

    public void CreateInventory()
    {
        foreach (Transform t in this.transform)
        {
            Destroy(t.gameObject);
        }
        for (int i = 1; i <= inventorySize.x; i++)
        {
            for (int j = 1; j <= inventorySize.y; j++)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.transform.SetParent(this.transform);
                slot.name = "Slot_" + i + "+" + j;
                slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(windowSize.x / (inventorySize.x + 1) * i, windowSize.y / (inventorySize.y + 1) * -j, 0);
                slot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                if ((i + (j - 1) * 4) <= ItemDB.sortedItems.Count)
                {
                    GameObject item = Instantiate(itemPrefab);
                    item.transform.SetParent(slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    Item _item = item.GetComponent<Item>();

                    _item.name = ItemDB.sortedItems[(i + (j - 1) * 4) - 1].name;
                    _item.itemType = ItemDB.sortedItems[(i + (j - 1) * 4) - 1].itemType;
                    _item.sprite = ItemDB.sortedItems[(i + (j - 1) * 4) - 1].sprite;

                    item.name = _item.name;
                    item.GetComponent<Image>().sprite = _item.sprite;
                }
            }
        }

    }
}
