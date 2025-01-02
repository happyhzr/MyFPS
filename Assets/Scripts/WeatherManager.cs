using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("weather manager starting...");
        network = service;
        string jsonPath = Path.Combine(Application.dataPath, "Resources", "Keys", "keys.json");
        if (!File.Exists(jsonPath))
        {
            Debug.LogError("JSON file not found at path: " + jsonPath);
            return;
        }
        string jsonString = File.ReadAllText(jsonPath);
        Keys data = new Keys();
        JsonUtility.FromJsonOverwrite(jsonString, data);
        network.jsonApi = "https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mod=xml&appid=" + data.appId;

        StartCoroutine(network.GetWeatherJson(OnJsonDataLoaded));
        status = ManagerStatus.Initializing;
    }

    public void OnJsonDataLoaded(string data)
    {
        Debug.Log(data);
        status = ManagerStatus.Started;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
