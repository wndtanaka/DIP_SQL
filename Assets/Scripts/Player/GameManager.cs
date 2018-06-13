using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Player player;
        [SerializeField]
        LayerMask layerMask;

        private void Update()
        {
            ClickTarget();
        }

        private void ClickTarget()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        player.MyTarget = hit.transform.GetChild(0);
                    }
                }
                else
                {
                    player.MyTarget = null;
                }
            }
        }
    }
}