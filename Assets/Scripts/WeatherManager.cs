using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public float cloudValue { get; private set; }

    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("weather manager starting...");
        network = service;
        TextAsset jsonAsset = Resources.Load<TextAsset>("Keys/keys");
        if (jsonAsset == null)
        {
            Debug.LogError("JSON file not found at path: Keys/keys/json");
            return;
        }
        string jsonString = jsonAsset.text;
        Keys data = new Keys();
        JsonUtility.FromJsonOverwrite(jsonString, data);
        network.jsonApi = "https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&appid=" + data.appId;

        StartCoroutine(network.GetWeatherJson(OnJsonDataLoaded));
        status = ManagerStatus.Initializing;
    }

    public void OnJsonDataLoaded(string data)
    {
        status = ManagerStatus.Started;
        WeatherData wd = JsonConvert.DeserializeObject<WeatherData>(data);
        cloudValue = wd.clouds.all / 100f;
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
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
