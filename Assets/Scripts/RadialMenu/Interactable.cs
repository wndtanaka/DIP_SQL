using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class Interactable : MonoBehaviour
    {
        [System.Serializable]
        public class Action
        {
            //public Color color;
            public Sprite sprite;
            public string title;
        }

        public Action[] options;


        private void Start()
        {
            RadialMenuSpawner.Instance.SpawnMenu(this);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // tell the canvas to spawn a menu
                RadialMenuSpawner.Instance.OpenClose();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                // tell the canvas to spawn a menu
                RadialMenuSpawner.Instance.OpenClose();
            }
        }
    }
}