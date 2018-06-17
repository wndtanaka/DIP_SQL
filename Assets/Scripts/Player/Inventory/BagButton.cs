using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG
{
    public class BagButton : MonoBehaviour, IPointerClickHandler
    {
        Bag bag;

        [SerializeField]
        Sprite full, empty;

        public Bag MyBag
        {
            get
            {
                return bag;
            }

            set
            {
                if (value != null)
                {
                    GetComponent<Image>().sprite = full;
                }
                else
                {
                    GetComponent<Image>().sprite = empty;
                }
                bag = value;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (InventoryScript.Instance.FromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
                {
                    if (MyBag != null)
                    {
                        InventoryScript.Instance.SwapBags(MyBag, HandScript.MyInstance.MyMoveable as Bag);
                    }
                    else
                    {
                        Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
                        tmp.MyBagButton = this;
                        tmp.Use();
                        MyBag = tmp;
                        HandScript.MyInstance.Drop();
                        InventoryScript.Instance.FromSlot = null;
                    }
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    HandScript.MyInstance.TakeMoveable(MyBag);
                }

                else if (bag != null)
                {
                    bag.MyBagScript.OpenClose();
                }
            }
        }

        public void RemoveBag()
        {
            InventoryScript.Instance.RemoveBag(MyBag);
            MyBag.MyBagButton = null;

            foreach (Item item in MyBag.MyBagScript.GetItems())
            {
                InventoryScript.Instance.AddItem(item);
            }
            MyBag = null;
        }
    }
}