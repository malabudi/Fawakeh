using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
	public static PlayerInformation Instance;
	public string userID;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		GenerateUserID();
	}

	private void GenerateUserID()
	{
		// Grab the player ID if it does not already exist then make a new one
		if (PlayerPrefs.HasKey("UserID"))
		{
			// Retrieve the unique ID
			userID = PlayerPrefs.GetString("UserID");
		}
		else
		{
			// Generate a new unique ID and save to player prefs
			userID = System.Guid.NewGuid().ToString();
			PlayerPrefs.SetString("UserID", userID);
		}
	}
}
