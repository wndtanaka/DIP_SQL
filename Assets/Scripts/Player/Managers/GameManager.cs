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

        NPC currentTarget;

        private void Update()
        {
            ClickTarget();
        }

        private void ClickTarget()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);
                // select target
                if (hit.collider != null)
                {
                    if (currentTarget != null)
                    {
                        currentTarget.DeSelect();
                    }
                    currentTarget = hit.collider.GetComponent<NPC>();
                    player.MyTarget = currentTarget.Select();
                    UIManager.Instance.ShowTargetFrame(currentTarget);
                }
                // deselect target
                else
                {
                    UIManager.Instance.HideTargetFrame();
                    if (currentTarget != null)
                    {
                        currentTarget.DeSelect();
                    }
                    currentTarget = null;
                    player.MyTarget = null;
                }
            }
        }
    }
}