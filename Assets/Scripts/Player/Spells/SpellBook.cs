using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class SpellBook : MonoBehaviour
    {
        private static SpellBook instance;
        public static SpellBook Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SpellBook>();
                }
                return instance;
            }
        }

        [SerializeField]
        Image castingBar;
        [SerializeField]
        Text currentSpell;
        [SerializeField]
        Image spellIcon;
        [SerializeField]
        Text castTime;
        [SerializeField]
        CanvasGroup canvasGroup;
        [SerializeField]
        Spell[] spells;

        Coroutine spellRoutine;
        Coroutine fadeRoutine;

        public Spell CastSpell(string spellName)
        {
            Spell spell = Array.Find(spells, x => x.Name == spellName);

            castingBar.fillAmount = 0;
            castingBar.color = spell.BarColor;
            currentSpell.text = spell.Name;
            spellIcon.sprite = spell.Icon;
            spellRoutine = StartCoroutine(Progress(spell));
            fadeRoutine = StartCoroutine(FadeBar());
            return spell;
        }

        private IEnumerator Progress(Spell spell)
        {
            float timePassed = Time.deltaTime;
            float rate = 1f / spell.CastTime;
            float progress = 0f;
            while (progress <= 1)
            {
                castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
                progress += rate * Time.deltaTime;
                timePassed += Time.deltaTime;
                castTime.text = (spell.CastTime - timePassed).ToString("F2");
                if (spell.CastTime - timePassed < 0)
                {
                    castTime.text = "0.00";
                }
                yield return null;
            }
            StopCasting();
        }

        private IEnumerator FadeBar()
        {
            float rate = 1f / 0.5f;
            float progress = 0f;
            while (progress <= 1)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
        }

        public void StopCasting()
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(FadeBar());
                canvasGroup.alpha = 0;
                fadeRoutine = null;
            }

            if (spellRoutine != null)
            {
                StopCoroutine(spellRoutine);
                spellRoutine = null;
            }
        }
        public Spell GetSpell(string spellName)
        {
            Spell spell = Array.Find(spells, x => x.Name == spellName);
            return spell;
        }

    }
}