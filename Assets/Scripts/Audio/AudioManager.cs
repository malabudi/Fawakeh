using UnityEngine;

// Audio manager must be a singleton for the program
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
	public bool isSoundEffectMuted = false;

	private AudioSource audioSrc;

	/* Indices - Audio
	 * 0 - Tap Sound Effect
	 * 1 - Drop sound effect
	 */
	[SerializeField] AudioClip[] sounds;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		audioSrc = GetComponent<AudioSource>();
	}

	public void PlayAudio(string audio, float volume)
	{
		if (!isSoundEffectMuted)
		{
			switch (audio)
			{
				case "tap":
					audioSrc.PlayOneShot(sounds[0], volume);
					break;
				case "drop":
					audioSrc.PlayOneShot(sounds[1], volume);
					break;
				default:
					Debug.LogError("There is no Audio with name: " + audio);
					break;
			}
			
		}
	}
}
