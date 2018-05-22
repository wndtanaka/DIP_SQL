using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
    public static RadialMenuSpawner Instance;
    public RadialMenu menuPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnMenu(Interactable obj)
    {
        RadialMenu newMenu = Instantiate(menuPrefab);
        newMenu.transform.SetParent(transform, false);
        newMenu.transform.position = /*Input.mousePosition*/transform.position;
        newMenu.SpawnButtons(obj);
    }
}
