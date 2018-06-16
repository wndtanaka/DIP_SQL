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

        public bool IsEmpty
        {
            get
            {
                return items.Count == 0;
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
                    return items.Peek();
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
                return items.Count;
            }
        }

        public Text MyStackText
        {
            get
            {
                return stackSize;
            }
        }

        private void Awake()
        {
            items.OnPop += new UpdateStackEvent(UpdateSlot);
            items.OnPush += new UpdateStackEvent(UpdateSlot);
            items.OnClear += new UpdateStackEvent(UpdateSlot);
        }

        public bool AddItem(Item item)
        {
            items.Push(item);
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
                items.Pop();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // if we dont have something
                if (InventoryScript.Instance.FromSlot == null && !IsEmpty)
                {
                    HandScript.Instance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.Instance.FromSlot = this;
                }
                // if we have something
                else if (InventoryScript.Instance.FromSlot != null)
                {
                    if (PutItemBack() || SwapItems(InventoryScript.Instance.FromSlot) || AddItems(InventoryScript.Instance.FromSlot.items))
                    {
                        HandScript.Instance.Drop();
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
            if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
            {
                items.Push(item);
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
                ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);
                // clear slot A
                from.items.Clear();
                // all items from slot b and copy them into a
                from.AddItems(items);
                // clear B
                items.Clear();
                // move items from A to B
                AddItems(tmpFrom);
                return true;
            }
            return false;
        }
        void UpdateSlot()
        {
            UIManager.Instance.UpdateStacksSize(this);
        }
    }
}