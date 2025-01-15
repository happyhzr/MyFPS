using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private string filename;
    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("data manager starting...");
        network = service;
        filename = Path.Combine(Application.persistentDataPath, "game.dat");
        status = ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        Dictionary<string, object> gameState = new Dictionary<string, object>();
        gameState.Add("inventory", Managers.Inventory.GetData());
        gameState.Add("health", Managers.Player.health);
        gameState.Add("maxHealth", Managers.Player.maxHealth);
        gameState.Add("curLevel", Managers.Mission.curLevel);
        gameState.Add("maxLevel", Managers.Mission.maxLevel);

        using (FileStream stream = File.Create(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, gameState);
        }
    }

    public void LoadGameState()
    {
        if (!File.Exists(filename))
        {
            Debug.Log("no saved game");
            return;
        }

        Dictionary<string, object> gameState;
        using(FileStream stream = File.Open(filename,FileMode.Open)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            gameState=formatter.Deserialize(stream) as Dictionary<string, object>;
        }

        Managers.Inventory.UpdateData((Dictionary<string, int>)gameState["inventory"]);
        Managers.Player.UpdateData((int)gameState["health"], (int)gameState["maxHealth"]);
        Managers.Mission.UpdateData((int)gameState["curLevel"], (int)gameState["maxLevel"]);
        Managers.Mission.RestartCurrent();
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
