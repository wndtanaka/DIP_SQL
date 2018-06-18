using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class RadialMenu : MonoBehaviour
    {
        public RadialButton buttonPrefab;
        public RadialButton selected;
        RadialButton newButton;

        public void SpawnButtons(Interactable obj)
        {
            for (int i = 0; i < obj.options.Length; i++)
            {
                newButton = Instantiate(buttonPrefab);
                newButton.transform.SetParent(transform, false);
                float theta = (2 * Mathf.PI / obj.options.Length) * i;
                float xPos = Mathf.Sin(theta);
                float yPos = Mathf.Cos(theta);
                newButton.transform.localPosition = new Vector3(xPos, yPos, 0) * 100f;
                newButton.icon.sprite = obj.options[i].sprite;
                newButton.title = obj.options[i].title;
                newButton.icon.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
                newButton.myMenu = this;
            }

        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (selected)
                {
                    // TODO do selected
                }
                RadialMenuSpawner.Instance.OpenClose();
            }
        }
    }
}