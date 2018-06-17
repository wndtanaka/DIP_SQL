﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public delegate void ItemCountChanged(Item item);
    public class InventoryScript : MonoBehaviour
    {
        public event ItemCountChanged onItemCountChanged;

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

        private SlotScript fromSlot;

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

        public int MyEmptySlotCount
        {
            get
            {
                int count = 0;
                foreach (Bag bag in bags)
                {
                    count += bag.MyBagScript.MyEmptySlotCount;
                }
                return count;
            }
        }

        public int MyTotalSlotCount
        {
            get
            {
                int count = 0;
                foreach (Bag bag in bags)
                {
                    count += bag.MyBagScript.MySlots.Count;
                }
                return count;
            }
        }

        public int MyFullSlotCount
        {
            get
            {
                return MyTotalSlotCount - MyEmptySlotCount;
            }
        }

        public SlotScript FromSlot
        {
            get
            {
                return fromSlot;
            }

            set
            {
                fromSlot = value;
                if (value != null)
                {
                    fromSlot.MyIcon.color = Color.grey;
                }
                fromSlot = value;
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
            if (Input.GetKeyDown(KeyCode.J))
            {
                Bag bag = (Bag)Instantiate(items[0]);
                bag.Initialize(8);
                AddItem(bag);
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
                    bag.MyBagButton = bagButton;
                    break;
                }
            }
        }
        public void AddBag(Bag bag, BagButton bagButton)
        {
            bags.Add(bag);
            bagButton.MyBag = bag;
        }

        public void RemoveBag(Bag bag)
        {
            bags.Remove(bag);
            Destroy(bag.MyBagScript.gameObject);
        }

        public void SwapBags(Bag oldBag, Bag newBag)
        {
            int newSlotCount = (MyTotalSlotCount - oldBag.Slots) + newBag.Slots;

            if (newSlotCount - MyFullSlotCount >= 0)
            {
                //do swap
                List<Item> bagItems = oldBag.MyBagScript.GetItems();
                RemoveBag(oldBag);
                newBag.MyBagButton = oldBag.MyBagButton;
                newBag.Use();
                foreach (Item item in bagItems)
                {
                    if (item != newBag) // no duplicates
                    {
                        AddItem(item);
                    }
                }
                AddItem(oldBag);
                HandScript.MyInstance.Drop();
                Instance.fromSlot = null;
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
                    OnItemCountChanged(item);
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
                        OnItemCountChanged(item);
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
        public Stack<IUseable> GetUseables(IUseable type)
        {
            Stack<IUseable> useables = new Stack<IUseable>();
            foreach (Bag bag in bags)
            {
                foreach (SlotScript slot in bag.MyBagScript.MySlots)
                {
                    if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                    {
                        foreach (Item item in slot.MyItems)
                        {
                            useables.Push(item as IUseable);
                        }
                    }
                }
            }
            return useables;
        }

        public void OnItemCountChanged(Item item)
        {
            if (onItemCountChanged != null)
            {
                onItemCountChanged.Invoke(item);
            }
        }
    }
}