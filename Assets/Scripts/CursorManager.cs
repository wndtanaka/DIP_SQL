using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom cursor manager
public class CursorManager : MonoBehaviour
{
    [SerializeField]
    Texture2D defaultCursor;
    [SerializeField]
    Texture2D targetCursor;
    [SerializeField]
    Texture2D pickupCursor;
    [SerializeField]
    Vector2 cursorHotspot = new Vector2(5, 5);
    [SerializeField]
    LayerMask layerMask;

    void Update()
    {
        // racaysting from camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // also filter by layermask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Pickup"))
            {
                Cursor.SetCursor(pickupCursor, cursorHotspot, CursorMode.Auto);
            }
        }
        // by default set to the default cursor
        else
        {
            Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
        }
    }
}
