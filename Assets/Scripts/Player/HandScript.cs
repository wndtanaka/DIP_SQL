﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG
{
    public class HandScript : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the handscript
        /// </summary>
        private static HandScript instance;

        public static HandScript MyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<HandScript>();
                }

                return instance;
            }
        }

        /// <summary>
        /// The current moveable
        /// </summary>
        public IMoveable MyMoveable { get; set; }

        /// <summary>
        /// The icon of the item, that we acre moving around atm.
        /// </summary>
        private Image icon;

        /// <summary>
        /// An offset to move the icon away from the mouse
        /// </summary>
        [SerializeField]
        private Vector3 offset;

        // Use this for initialization
        void Start()
        {
            //Creates a reference to the image on the hand
            icon = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            //Makes sure that the icon follows the hand
            icon.transform.position = Input.mousePosition + offset;

            DeleteItem();
        }

        /// <summary>
        /// Take a moveable in the hand, so that we can move it around
        /// </summary>
        /// <param name="moveable">The moveable to pick up</param>
        public void TakeMoveable(IMoveable moveable)
        {
            this.MyMoveable = moveable;
            icon.sprite = moveable.MyIcon;
            icon.color = Color.white;
        }

        public IMoveable Put()
        {
            IMoveable tmp = MyMoveable;
            MyMoveable = null;
            icon.color = new Color(0, 0, 0, 0);
            return tmp;
        }

        public void Drop()
        {
            MyMoveable = null;
            icon.color = new Color(0, 0, 0, 0);
        }

        /// <summary>
        /// Deletes an item from the inventory
        /// </summary>
        private void DeleteItem()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
            {
                if (MyMoveable is Item && InventoryScript.Instance.FromSlot != null)
                {
                    (MyMoveable as Item).MySlot.Clear();
                }

                Drop();

                InventoryScript.Instance.FromSlot = null;
            }
        }
    }
}