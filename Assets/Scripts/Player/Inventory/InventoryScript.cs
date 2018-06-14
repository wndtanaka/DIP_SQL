using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class InventoryScript : MonoBehaviour
    {
        private static InventoryScript instance;
        public static InventoryScript Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<InventoryScript>();
                }
                return instance;
            }
        }
        List<Bag> bags = new List<Bag>();

        [SerializeField]
        BagButton[] bagButtons;
        [SerializeField]
        Item[] items;

        public bool canAddBag
        {
            get
            {
                return bags.Count < 5;
            }
        }

        private void Awake()
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            bag.Use();
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Bag bag = (Bag)Instantiate(items[0]);
                bag.Initialize(16);
                bag.Use();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Bag bag = (Bag)Instantiate(items[0]);
                bag.Initialize(16);
                AddItem(bag);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                HealthPotion potion = (HealthPotion)Instantiate(items[1]);
                AddItem(potion);
            }
        }
        public void AddBag(Bag bag)
        {
            foreach (BagButton bagButton in bagButtons)
            {
                if (bagButton.MyBag == null)
                {
                    bagButton.MyBag = bag;
                    bags.Add(bag);
                    break;
                }
            }
        }

        public void AddItem(Item item)
        {
            if (item.MyStackSize > 0)
            {
                if (PlaceInStack(item))
                {
                    return;
                }
            }
            PlaceInEmptySlot(item);
        }

        private void PlaceInEmptySlot(Item item)
        {
            foreach (Bag bag in bags)
            {
                if (bag.MyBagScript.AddItem(item))
                {
                    return;
                }
            }
        }

        private bool PlaceInStack(Item item)
        {
            foreach (Bag bag in bags)
            {
                foreach (SlotScript slots in bag.MyBagScript.MySlots)
                {
                    if (slots.StackItem(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void OpenClose()
        {
            bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);
            foreach (Bag bag in bags)
            {
                // if all bag is close then open all bags
                // if all bag is open then close all bags
                if (bag.MyBagScript.IsOpen != closedBag)
                {
                    bag.MyBagScript.OpenClose();
                }
            }
        }
    }
}