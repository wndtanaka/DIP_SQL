using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class SpellBook : MonoBehaviour
    {
        [SerializeField]
        Image castingBar;
        [SerializeField]
        Text spellName;
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

        public Spell CastSpell(int index)
        {
            castingBar.fillAmount = 0;
            castingBar.color = spells[index].BarColor;
            spellName.text = spells[index].Name;
            spellIcon.sprite = spells[index].Icon;
            spellRoutine = StartCoroutine(Progress(index));
            fadeRoutine = StartCoroutine(FadeBar());
            return spells[index];
        }

        private IEnumerator Progress(int index)
        {
            float timePassed = Time.deltaTime;
            float rate = 1f / spells[index].CastTime;
            float progress = 0f;
            while (progress <= 1)
            {
                castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
                progress += rate * Time.deltaTime;
                timePassed += Time.deltaTime;
                castTime.text = (spells[index].CastTime - timePassed).ToString("F2");
                if (spells[index].CastTime - timePassed < 0)
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
    }
}