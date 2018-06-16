using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    [System.Serializable]
    public class Spell : IUseable, IMoveable
    {
        [SerializeField]
        string name;
        [SerializeField]
        int damage;
        [SerializeField]
        Sprite icon;
        [SerializeField]
        float speed;
        [SerializeField]
        float castTime;
        [SerializeField]
        GameObject spellPrefab;
        [SerializeField]
        private Color barColor;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }
        }

        public Sprite Icon
        {
            get
            {
                return icon;
            }
        }

        public float Speed
        {
            get
            {
                return speed;
            }
        }

        public float CastTime
        {
            get
            {
                return castTime;
            }
        }

        public GameObject SpellPrefab
        {
            get
            {
                return spellPrefab;
            }
        }

        public Color BarColor
        {
            get
            {
                return barColor;
            }
        }

        public Sprite MyIcon
        {
            get
            {
                return icon;
            }
        }

        public void Use()
        {
            Player.MyInstance.CastSpell(Name);
        }
    }
}