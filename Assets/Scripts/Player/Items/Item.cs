using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField]
        Sprite icon;
        [SerializeField]
        int stacksSize;
        private SlotScript slot;

        public Sprite MyIcon
        {
            get
            {
                return icon;
            }
        }

        public int MyStackSize
        {
            get
            {
                return stacksSize;
            }
        }

        public SlotScript MySlot
        {
            get
            {
                return slot;
            }

            set
            {
                slot = value;
            }
        }
        public void Remove()
        {
            if (MySlot!= null)
            {
                MySlot.RemoveItem(this);
            }
        }
    }
}