using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

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
        else
        {
            Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
        }
    }
}
