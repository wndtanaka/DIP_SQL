using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class RadialMenuSpawner : MonoBehaviour
    {
        public static RadialMenuSpawner Instance;
        public RadialMenu menuPrefab;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            Instance = this;
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SpawnMenu(Interactable obj)
        {
            RadialMenu newMenu = Instantiate(menuPrefab);
            newMenu.transform.SetParent(transform, false);
            newMenu.transform.position = /*Input.mousePosition*/transform.position;
            newMenu.SpawnButtons(obj);
        }

        public void OpenClose()
        {
            canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
            canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
        }
    }
}