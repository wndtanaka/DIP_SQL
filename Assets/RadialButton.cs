using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image circle;
    public Image icon;
    public string title;
    public RadialMenu myMenu;

    Color defaultColor;
    Vector2 defaultScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        myMenu.selected = this;
        defaultScale = transform.localScale;
        defaultColor = circle.color;
        circle.color = Color.white;
        transform.localScale = new Vector2(2, 2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myMenu.selected = null;
        circle.color = defaultColor;
        transform.localScale = defaultScale;
    }

}
