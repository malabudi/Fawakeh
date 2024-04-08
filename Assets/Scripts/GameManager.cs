using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Game manager must use the singleton pattern
    public static GameManager instance;

	public int CurrentScore { get; set; }

	[SerializeField] private TextMeshProUGUI scoreText;


	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void IncreaseScore(int amount)
	{
		CurrentScore += amount;
		scoreText.text = CurrentScore.ToString("0");
	}
}
