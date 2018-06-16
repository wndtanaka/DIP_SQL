﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG
{
    public class SpellButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private string spellName;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                HandScript.Instance.TakeMoveable(SpellBook.Instance.GetSpell(spellName));
            }
        }
    }
}