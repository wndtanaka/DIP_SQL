using UnityEngine.UI;
using UnityEngine;

namespace RPG
{
    public class RadialButton : MonoBehaviour
    {
        public Image circle;
        public Image icon;
        public string title;
        public RadialMenu myMenu;

        //Color defaultColor;
        Vector2 defaultScale;

        public void OnPointerEnter()
        {
            myMenu.selected = this;
            defaultScale = transform.localScale;
            //defaultColor = circle.color;
            //circle.color = Color.white;
            transform.localScale = new Vector2(1.5f, 1.5f);
        }

        public void OnPointerExit()
        {
            myMenu.selected = null;
            //circle.color = defaultColor;
            transform.localScale = defaultScale;
        }

    }
}