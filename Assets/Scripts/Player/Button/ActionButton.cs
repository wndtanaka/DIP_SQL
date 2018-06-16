using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RPG
{
    public class ActionButton : MonoBehaviour, IPointerClickHandler
    {
        public IUseable MyUseable { get; set; }

        public Button MyButton { get; private set; }

        public Image MyIcon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
            }
        }

        [SerializeField]
        private Image icon;

        private void Start()
        {
            MyButton = GetComponent<Button>();
            MyButton.onClick.AddListener(OnClick);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (HandScript.Instance.MyMoveable != null && HandScript.Instance.MyMoveable is IUseable)
                {
                    SetUsable(HandScript.Instance.MyMoveable as IUseable);
                }
            }
        }

        public void OnClick()
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
        }

        public void SetUsable(IUseable useable)
        {
            this.MyUseable = useable;
            UpdateVisual();
        }

        public void UpdateVisual()
        {
            MyIcon.sprite = HandScript.Instance.Put().MyIcon;
            MyIcon.color = Color.white;
        }
    }
}