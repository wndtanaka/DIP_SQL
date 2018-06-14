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

        private void Start()
        {
            MyButton = GetComponent<Button>();
            MyButton.onClick.AddListener(OnClick);
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnClick()
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
        }
    }
}