using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG
{
    [System.Serializable]
    public class Spell
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
    }
}