using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Stack<Item> items;
    public Text stackText;
    public Sprite slotEmpty;
    public Sprite slotHightlighted;

    void Start()
    {
        items = new Stack<Item>();
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform textRect = GetComponent<RectTransform>();

        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.60f);
        stackText.resizeTextMaxSize = textScaleFactor;
        stackText.resizeTextMinSize = textScaleFactor;

        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
    }

    void Update()
    {

    }

    public void AddItem(Item item)
    {
        items.Push(item);
        if (items.Count > 1)
        {
            stackText.text = items.Count.ToString();
        }
    }

    private void ChangeSprite(Sprite neutral, Sprite highlight)
    {
        GetComponent<Image>().sprite = neutral;
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        GetComponent<Button>().spriteState = st;
    }
}


