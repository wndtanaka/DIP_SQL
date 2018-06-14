using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
    public class HealthPotion : Item, IUseable
    {
        [SerializeField]
        int health;
        public void Use()
        {
            if (Player.MyInstance.Health.MyCurrentValue < Player.MyInstance.Health.MyMaxValue)
            {
                Remove();
                Player.MyInstance.Health.MyCurrentValue += health;
            }
        }
    }
}