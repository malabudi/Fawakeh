using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
			StartCoroutine(CreateUser(userID));
		}
	}

	private IEnumerator CreateUser(string playerId)
	{
		string url = $"http://localhost:5000/create";
		string jsonData = $"{{ \"_id\": ${playerId}, \"username\": ${playerId}}}";

		Debug.Log("Sending JSON: " + jsonData); // Log the JSON being sent
		using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
		{
			byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
			request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");

			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError(request.error);
			}
			else
			{
				Debug.Log("Response: " + request.downloadHandler.text);
			}
		}
	}
}
