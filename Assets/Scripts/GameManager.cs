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
    private bool isResetting = false;

    public int CurrentScore { get; set; }

	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private Image gameStartPanel;
	[SerializeField] private Image gameOverPanel;
	[SerializeField] private float fadeTimeBegin = 0.25f;
	[SerializeField] private float fadeTimeEnd = 2f;

	public float timeUntilGameOver = 1.5f;

    // Example ID and score
    private string playerId = "65fca0a60f2500503b805f11";
    private int playerScore = 68;

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
        if (isResetting) yield break; // Prevent multiple calls
        isResetting = true;

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

        // Update the player score before changing the scene
        ScoreManager scoreManager = gameObject.AddComponent<ScoreManager>();
        scoreManager.UpdatePlayerScore(playerId, playerScore);

        SceneManager.LoadScene("MenuScene");

        isResetting = false; // Reset the flag in case of returning to this scene
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
