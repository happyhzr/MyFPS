using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int curLevel { get; private set; }
    public int maxLevel { get; private set; }

    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("mission manager starting...");
        network = service;
        curLevel = 0;
        maxLevel = 1;
        status = ManagerStatus.Started;
    }

    public void GoToNext()
    {
        if (curLevel < maxLevel)
        {
            curLevel++;
            string name = $"Level{curLevel}";
            Debug.Log($"loading {name}");
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.Log("last level");
        }
    }

    public void ReachObjective()
    {
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }

    public void RestartCurrent()
    {
        string name = $"Level{curLevel}";
        Debug.Log($"loading {name}");
        SceneManager.LoadScene(name);
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
