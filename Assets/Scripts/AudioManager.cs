using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource soundSource;

    public ManagerStatus status { get; private set; }

    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("audio manager starting...");
        network = service;
        status = ManagerStatus.Started;
        soundVolume = 1f;
    }

    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
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
