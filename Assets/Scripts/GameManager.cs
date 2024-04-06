using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Game manager must use the singleton pattern
    public static GameManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
}
