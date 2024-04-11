using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
	// Game manager must use the singleton pattern
    public static GameManager instance;

	public int CurrentScore { get; set; }

	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private Image gameStartPanel;
	[SerializeField] private Image gameOverPanel;
	[SerializeField] private float fadeTimeBegin = 0.25f;
	[SerializeField] private float fadeTimeEnd = 2f;

	public float timeUntilGameOver = 1.5f;

	private void OnEnable()
	{
		SceneManager.sceneLoaded += FadeGame;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= FadeGame;
	}

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

	public void GameOver()
	{
		StartCoroutine(ResetGame());
		StartCoroutine(UpdateHighScore());
	}

	private IEnumerator UpdateHighScore()
	{
		string userID;

		// Check if the unique ID exists in PlayerPrefs (singleton)
		if (PlayerPrefs.HasKey("UserID"))
		{
			// Retrieve the unique ID
			userID = PlayerPrefs.GetString("UserID");
		}
		else
		{
			// Generate a new unique ID and save to player prefs
			userID = Guid.NewGuid().ToString();
			PlayerPrefs.SetString("UserID", userID);
		}

		// Use userID as the primary key to send a post request and update leaderboard (example code for now)
		using (UnityWebRequest www = UnityWebRequest.Post("https://www.my-server.com/myapi", 
			"{ \"field1\": 1, \"field2\": 2 }", 
			"application/json"))
		{
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError(www.error);
			}
			else
			{
				Debug.Log("Form upload complete!");
			}
		}
	}

	private IEnumerator ResetGame()
	{
		gameOverPanel.gameObject.SetActive(true);

		Color startColor = gameOverPanel.color;
		startColor.a = 0f;
		gameOverPanel.color = startColor;

		float elapsedTime = 0f;

		while (elapsedTime < fadeTimeEnd)
		{
			elapsedTime += Time.deltaTime;

			float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / fadeTimeEnd));
			startColor.a = newAlpha;
			gameOverPanel.color = startColor;

			yield return null;
		}

		SceneManager.LoadScene("MenuScene");
	}

	private void FadeGame(Scene scene, LoadSceneMode mode)
	{
		StartCoroutine(FadeGameIn());
	}

	private IEnumerator FadeGameIn()
	{
		gameStartPanel.gameObject.SetActive(true);
		Color startColor = gameStartPanel.color;
		startColor.a = 1f;
		gameStartPanel.color = startColor;

		float elapsedTime = 0f;

		while (elapsedTime < fadeTimeBegin)
		{
			elapsedTime += Time.deltaTime;
			float newAlpha = Mathf.Lerp(1f, 0f, (elapsedTime / fadeTimeBegin));
			startColor.a = newAlpha;
			gameStartPanel.color = startColor;

			yield return null;
		}

		gameStartPanel.gameObject.SetActive(false);
	}
}
