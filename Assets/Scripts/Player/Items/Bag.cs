using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "Bag", menuName = "Items/Bag", order = 1)]
    public class Bag : Item, IUseable
    {
        int slots;
        [SerializeField]
        protected GameObject bagPrefab;

        public BagScript MyBagScript { get; set; }

        public BagButton MyBagButton { get; set; }

        public int Slots
        {
            get
            {
                return slots;
            }
        }

        public void Initialize(int slots)
        {
            this.slots = slots;
        }
        public void Use()
        {
            if (InventoryScript.Instance.canAddBag)
            {
                Remove();
                MyBagScript = Instantiate(bagPrefab, InventoryScript.Instance.transform).GetComponent<BagScript>();
                MyBagScript.AddSlots(slots);

                if (MyBagButton == null)
                {
                    InventoryScript.Instance.AddBag(this);

                }
                else
                {
                    InventoryScript.Instance.AddBag(this, MyBagButton);
                }
                MyBagScript.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            }
        }
    }
}