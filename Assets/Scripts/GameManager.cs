using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static string LoggedInUsername;
    public static string SelectedCharacterName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (LoggedInUsername != null)
        {
            Debug.Log(LoggedInUsername);
        }
        Debug.Log(SelectedCharacterName);
    }
}
