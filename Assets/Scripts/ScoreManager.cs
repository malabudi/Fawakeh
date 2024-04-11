using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public void UpdatePlayerScore(string playerId, int score)
    {
        StartCoroutine(SendScoreUpdate(playerId, score));
    }

    private IEnumerator SendScoreUpdate(string playerId, int score)
    {

        string url = $"http://localhost:5000/update/{playerId}";
        //string jsonData = JsonUtility.ToJson(new { score = 65 });
        string jsonData = "Hisham";
        jsonData = "{ \"score\": 65 }";

        Debug.Log("Sending JSON: " + jsonData); // Log the JSON being sent
        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
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
