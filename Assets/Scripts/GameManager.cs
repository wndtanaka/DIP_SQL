using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public static string LoggedInUsername;

	void Awake () {
		if (Instance == null) 
		{
			Instance = this;
		}

	}
	void Start()
	{
		if (LoggedInUsername == null) {

		}
	}
}
