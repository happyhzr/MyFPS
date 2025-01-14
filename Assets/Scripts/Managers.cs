using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImagesManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(MissionManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }
    public static WeatherManager Weather { get; private set; }
    public static ImagesManager Images { get; private set; }
    public static AudioManager Audio { get; private set; }
    public static MissionManager Mission { get; private set; }

    private List<IGameManager> startSequence;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        //Weather = GetComponent<WeatherManager>();
        Images = GetComponent<ImagesManager>();
        Audio = GetComponent<AudioManager>();
        Mission = GetComponent<MissionManager>();

        startSequence = new List<IGameManager>();
        startSequence.Add(Player);
        startSequence.Add(Inventory);
        //startSequence.Add(Weather);
        startSequence.Add(Images);
        startSequence.Add(Audio);
        startSequence.Add(Mission);
        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        NetworkService service = new NetworkService();
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup(service);
        }
        yield return null;

        int numModules = startSequence.Count;
        int numReady = 0;
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
            {
                Debug.Log($"progress: {numReady}/{numModules}");
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
            yield return null;
        }
        Debug.Log("all managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}
