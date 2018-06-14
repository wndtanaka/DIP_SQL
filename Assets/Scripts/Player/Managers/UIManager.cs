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
        private GameObject[] keybindButtons;

        private void Awake()
        {
            keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        }

        // Use this for initialization
        void Start()
        {
            healthStat = targetFrame.GetComponentInChildren<Stat>();
            SetUsable(actionButtons[0], SpellBook.Instance.GetSpell("Fireball"));
            SetUsable(actionButtons[1], SpellBook.Instance.GetSpell("Frostbolt"));
            SetUsable(actionButtons[2], SpellBook.Instance.GetSpell("Lightning Bolt"));
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
                OpenCloseMenu();
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
        public void OpenCloseMenu()
        {
            keybindMenu.alpha = keybindMenu.alpha > 0 ? 0 : 1;
            keybindMenu.blocksRaycasts = keybindMenu.blocksRaycasts == true ? false : true;
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
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

        public void SetUsable(ActionButton btn, IUseable useable)
        {
            btn.MyButton.image.sprite = useable.MyIcon;
            btn.MyButton.image.color = Color.white;
            btn.MyUseable = useable;
        }
    }
}