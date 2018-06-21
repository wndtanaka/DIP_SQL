using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static string LoggedInUsername;
    public static string SelectedCharacterName;
    public static string CharID;
    public static string sceneName;
    public GameObject[] characters;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "RPG")
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].SetActive(false);
                if (CharID == characters[i].name)
                {
                    characters[i].SetActive(true);
                    Camera.main.transform.SetParent(characters[i].transform);
                }
            }
        }
    }
}
