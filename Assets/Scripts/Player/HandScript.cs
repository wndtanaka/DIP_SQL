using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class HandScript : MonoBehaviour
    {
        private static HandScript instance;
        public static HandScript Instance
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

        [SerializeField]
        Vector3 offset;

        public IMoveable MyMoveable { get; set; }

        private Image icon;

        public void TakeMoveable(IMoveable moveable)
        {
            this.MyMoveable = moveable;
            icon.sprite = moveable.MyIcon;
            icon.color = Color.white;
        }
        private void Start()
        {
            icon = GetComponent<Image>();
        }

        private void Update()
        {
            icon.transform.position = Input.mousePosition + offset;
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
    }
}