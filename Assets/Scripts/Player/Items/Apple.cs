using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(fileName = "Apple", menuName = "Items/Apple", order = 2)]
    public class Apple : Item, IUseable
    {
        [SerializeField]
        int exp;
        public void Use()
        {
            Remove();
            Player.MyInstance.Exp.MyCurrentExp += exp;
        }
    }
}