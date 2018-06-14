using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class Stat : MonoBehaviour
    {
        Image content;
        [SerializeField]
        Text statValue;

        float currentFill;

        [SerializeField]
        float lerpSpeed;

        public float MyMaxValue { get; set; }
        private float currentValue;
        public float MyCurrentValue
        {
            get
            {
                return currentValue;
            }

            set
            {
                if (value > MyMaxValue)
                {
                    currentValue = MyMaxValue;
                }
                else if (value < 0)
                {
                    currentValue = 0;
                }
                else
                {
                    currentValue = value;
                }
                currentFill = currentValue / MyMaxValue;
                if (statValue!= null)
                {
                    statValue.text = currentValue + " / " + MyMaxValue;
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            content = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentFill != content.fillAmount)
            {
                content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }
        public void Initialize(float currentValue, float maxValue)
        {
            if (content == null)
            {
                content = GetComponent<Image>();
            }
            MyMaxValue = maxValue;
            MyCurrentValue = currentValue;
            content.fillAmount = MyCurrentValue / MyMaxValue;
        }
    }
}