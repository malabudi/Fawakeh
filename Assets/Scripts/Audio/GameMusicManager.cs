using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
	public static GameMusicManager Instance;

	private bool isMusicMuted = false;
	private AudioSource audioSrc;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
		if (musicObj.Length > 1)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		audioSrc = GetComponent<AudioSource>();
	}

	public void ToggleGameAudio()
	{
		isMusicMuted = !isMusicMuted;
		audioSrc.volume = isMusicMuted ? 0f : 1f;
	}
}
