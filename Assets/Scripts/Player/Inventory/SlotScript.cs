using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RPG
{
    public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable
    {
        private ObservableStack<Item> items = new ObservableStack<Item>();
        [SerializeField]
        Image icon;
        [SerializeField]
        private Text stackSize;

        // reference which bag that this slot belong to
        public BagScript MyBag { get; set; }

        public bool IsEmpty
        {
            get
            {
                return MyItems.Count == 0;
            }
        }

        public bool IsFull
        {
            get
            {
                if (IsEmpty || MyCount < MyItem.MyStackSize)
                {
                    return false;
                }
                return true;
            }
        }

        public Item MyItem
        {
            get
            {
                if (!IsEmpty)
                {
                    return MyItems.Peek();
                }
                return null;
            }
        }

        public Image MyIcon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
            }
        }

        public int MyCount
        {
            get
            {
                return MyItems.Count;
            }
        }

        public Text MyStackText
        {
            get
            {
                return stackSize;
            }
        }

        public ObservableStack<Item> MyItems
        {
            get
            {
                return items;
            }
        }

        private void Awake()
        {
            MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
            MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
            MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
        }

        public bool AddItem(Item item)
        {
            MyItems.Push(item);
            icon.sprite = item.MyIcon;
            icon.color = Color.white;
            item.MySlot = this;
            return true;
        }

        public bool AddItems(ObservableStack<Item> newItems)
        {
            if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
            {
                int count = newItems.Count;

                for (int i = 0; i < count; i++)
                {
                    if (IsFull)
                    {
                        return false;
                    }
                    AddItem(newItems.Pop());
                }
                return true;
            }
            return false;
        }

        public void RemoveItem(Item item)
        {
            if (!IsEmpty)
            {
                InventoryScript.Instance.OnItemCountChanged(MyItems.Pop());
            }
        }

        public void Clear()
        {
            if (MyItems.Count > 0)
            {
                InventoryScript.Instance.OnItemCountChanged(MyItems.Pop());
                MyItems.Clear();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // if we dont have something
                if (InventoryScript.Instance.FromSlot == null && !IsEmpty)
                {
                    if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
                    {
                        if (MyItem is Bag)
                        {
                            InventoryScript.Instance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        }
                    }
                    else
                    {
                        HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                        InventoryScript.Instance.FromSlot = this;
                    }
                }
                else if (InventoryScript.Instance.FromSlot == null && IsEmpty && (HandScript.MyInstance.MyMoveable is Bag))
                {
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;
                    if (bag.MyBagScript != MyBag && InventoryScript.Instance.MyEmptySlotCount - bag.Slots > 0)
                    {
                        AddItem(bag);
                        bag.MyBagButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                    }
                }
                // if we have something
                else if (InventoryScript.Instance.FromSlot != null)
                {
                    if (PutItemBack() || MergeItems(InventoryScript.Instance.FromSlot) || SwapItems(InventoryScript.Instance.FromSlot) || AddItems(InventoryScript.Instance.FromSlot.MyItems))
                    {
                        HandScript.MyInstance.Drop();
                        InventoryScript.Instance.FromSlot = null;
                    }
                }
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                UseItem();
            }
        }
        public void UseItem()
        {
            if (MyItem is IUseable)
            {
                (MyItem as IUseable).Use();
            }
        }
        public bool StackItem(Item item)
        {
            if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
            {
                MyItems.Push(item);
                item.MySlot = this;
                return true;
            }
            return false;
        }
        public bool PutItemBack()
        {
            if (InventoryScript.Instance.FromSlot == this)
            {
                InventoryScript.Instance.FromSlot.MyIcon.color = Color.white;
                return true;
            }
            return false;
        }
        private bool SwapItems(SlotScript from)
        {
            if (IsEmpty)
            {
                return false;
            }
            if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
            {
                // copy all the items we need to swap from A
                ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);
                // clear slot A
                from.MyItems.Clear();
                // all items from slot b and copy them into a
                from.AddItems(MyItems);
                // clear B
                MyItems.Clear();
                // move items from A to B
                AddItems(tmpFrom);
                return true;
            }
            return false;
        }

        private bool MergeItems(SlotScript from)
        {
            if (IsEmpty)
            {
                return false;
            }
            if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
            {
                // how many free slots
                int free = MyItem.MyStackSize - MyCount;

                for (int i = 0; i < free; i++)
                {
                    AddItem(from.MyItems.Pop());
                }
                return true;
            }
            return false;
        }

        void UpdateSlot()
        {
            UIManager.Instance.UpdateStackSize(this);
        }
    }
}