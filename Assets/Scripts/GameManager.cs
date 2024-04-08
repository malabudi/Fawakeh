using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// Game manager must use the singleton pattern
    public static GameManager instance;

	public int CurrentScore { get; set; }

	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private Image gameOverPanel;
	[SerializeField] private float fadeTime = 2f;

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
	}

	private IEnumerator ResetGame()
	{
		gameOverPanel.gameObject.SetActive(true);

		Color startColor = gameOverPanel.color;
		startColor.a = 0f;
		gameOverPanel.color = startColor;

		float elapsedTime = 0f;

		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;

			float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / fadeTime));
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
		gameOverPanel.gameObject.SetActive(true);
		Color startColor = gameOverPanel.color;
		startColor.a = 1f;
		gameOverPanel.color = startColor;

		float elapsedTime = 0f;

		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;
			float newAlpha = Mathf.Lerp(1f, 0f, (elapsedTime / fadeTime));
			startColor.a = newAlpha;
			gameOverPanel.color = startColor;

			yield return null;
		}

		gameOverPanel.gameObject.SetActive(false);
	}
}
