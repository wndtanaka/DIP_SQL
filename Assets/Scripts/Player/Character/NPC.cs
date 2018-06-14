using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public delegate void HealthChanged(float health);
    public delegate void CharacterRemoved();

    public class NPC : Character
    {
        public event HealthChanged onHealthChanged;
        public event CharacterRemoved onCharacterRemoved;

        [SerializeField]
        private Sprite potrait;
        public Sprite Potrait
        {
            get
            {
                return potrait;
            }
        }

        public virtual void DeSelect()
        {
            onHealthChanged -= new HealthChanged(UIManager.Instance.UpdateTargetFrame);
            onCharacterRemoved -= new CharacterRemoved(UIManager.Instance.HideTargetFrame);
        }

        public virtual Transform Select()
        {
            return hitBox;
        }

        public void OnHealthChanged(float health)
        {
            if (onHealthChanged != null)
            {
                onHealthChanged(health);
            }
        }
        public void OnCharacterRemoved()
        {
            if (onCharacterRemoved != null)
            {
                onCharacterRemoved.Invoke();
            }
            Destroy(gameObject);
        }
    }
}