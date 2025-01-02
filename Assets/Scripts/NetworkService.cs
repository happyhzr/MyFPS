using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    public string jsonApi { get; set; }

    public IEnumerator GetWeatherJson(Action<string> callback)
    {
        return CallApi(jsonApi, callback);
    }

    private IEnumerator CallApi(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"response error: {request.error}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }
}
