using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                }
                return instance;
            }
        }

        [SerializeField]
        ActionButton[] actionButtons;

        [SerializeField]
        private GameObject targetFrame;
        private Stat healthStat;
        [SerializeField]
        private Image potraitFrame;
        [SerializeField]
        private CanvasGroup keybindMenu;
        [SerializeField]
        private CanvasGroup spellBook;

        private GameObject[] keybindButtons;

        private void Awake()
        {
            keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        }

        // Use this for initialization
        void Start()
        {
            healthStat = targetFrame.GetComponentInChildren<Stat>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                InventoryScript.Instance.OpenClose();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenClose(keybindMenu);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                OpenClose(spellBook);
            }
        }

        public void ShowTargetFrame(NPC target)
        {
            targetFrame.SetActive(true);
            healthStat.Initialize(target.Health.MyCurrentValue, target.Health.MyMaxValue);
            potraitFrame.sprite = target.Potrait;
            target.onHealthChanged += new HealthChanged(UpdateTargetFrame);
            target.onCharacterRemoved += new CharacterRemoved(HideTargetFrame);
        }
        public void HideTargetFrame()
        {
            targetFrame.SetActive(false);
        }

        public void UpdateTargetFrame(float health)
        {
            healthStat.MyCurrentValue = health;
        }
        public void UpdateStacksSize(IClickable clickable)
        {
            if (clickable.MyCount > 1)
            {
                clickable.MyStackText.text = clickable.MyCount.ToString();
                clickable.MyStackText.color = Color.white;
                clickable.MyIcon.color = Color.white;
            }
            else
            {
                clickable.MyStackText.color = new Color(0, 0, 0, 0);
            }
            if (clickable.MyCount == 0)
            {
                clickable.MyIcon.color = new Color(0, 0, 0, 0);
                clickable.MyStackText.color = new Color(0, 0, 0, 0);
            }
        }

        public void UpdateKeyText(string key, KeyCode code)
        {
            Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
            tmp.text = code.ToString();
        }

        public void ClickActionButton(string buttonName)
        {
            Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
        }

        public void OpenClose(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
            canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        }
    }
}