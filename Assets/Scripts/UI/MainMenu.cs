using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onBtnClick()
	{
        AudioManager.Instance.PlayAudio("tap", 1f);
	}

    public void MuteSoundEffect()
	{
        AudioManager.Instance.isSoundEffectMuted = !AudioManager.Instance.isSoundEffectMuted;
	}

    public void MuteMusic()
    {
        GameMusicManager.Instance.ToggleGameAudio();
    }
}
