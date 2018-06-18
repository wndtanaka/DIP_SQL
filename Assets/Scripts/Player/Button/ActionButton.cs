using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace RPG
{
    public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable
    {
        private static ActionButton instance;
        public static ActionButton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ActionButton>();
                }
                return instance;
            }
        }

        public IUseable MyUseable { get; set; }

        [SerializeField]
        private Text stackSize;

        private Stack<IUseable> useables = new Stack<IUseable>();

        private int count;

        public Button MyButton { get; private set; }

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
                return count;
            }
        }

        public Text MyStackText
        {
            get
            {
                return stackSize;
            }
        }

        [SerializeField]
        private Image icon;

        private void Start()
        {
            MyButton = GetComponent<Button>();
            MyButton.onClick.AddListener(OnClick);
            InventoryScript.Instance.onItemCountChanged += new ItemCountChanged(UpdateItemCount);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
                {
                    SetUsable(HandScript.MyInstance.MyMoveable as IUseable);
                }
            }
        }

        public void OnClick()
        {
            if (HandScript.MyInstance.MyMoveable == null)
            {
                if (MyUseable != null)
                {
                    MyUseable.Use();
                }
                if (useables != null && useables.Count > 0)
                {
                    useables.Peek().Use();
                }
            }
        }

        public void SetUsable(IUseable useable)
        {
            if (useable is Item)
            {
                useables = InventoryScript.Instance.GetUseables(useable);
                count = useables.Count;
                InventoryScript.Instance.FromSlot.MyIcon.color = Color.white;
                InventoryScript.Instance.FromSlot = null;
            }
            else
            {
                this.MyUseable = useable;
            }
            UpdateVisual();
        }

        public void UpdateVisual()
        {
            MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
            MyIcon.color = Color.white;

            if (count > 1)
            {
                UIManager.Instance.UpdateStackSize(this);
            }
        }

        public void UpdateItemCount(Item item)
        {
            // if item is the same as we have one this button
            if (item is IUseable && useables.Count > 0)
            {
                if (useables.Peek().GetType() == item.GetType())
                {
                    useables = InventoryScript.Instance.GetUseables(item as IUseable);
                    count = useables.Count;
                    UIManager.Instance.UpdateStackSize(this);
                }
            }
        }
    }
}