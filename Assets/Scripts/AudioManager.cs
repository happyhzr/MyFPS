using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource music1Source;
    [SerializeField] private AudioSource music2Source;
    [SerializeField] private string introBgmMusic;
    [SerializeField] private string levelBgmMusic;
    [SerializeField] private float crossFadeRate = 1.5f;

    public ManagerStatus status { get; private set; }

    private AudioSource activeMusic;
    private AudioSource inactiveMusic;
    private bool crossFading;

    public float musicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            if (music1Source != null && !crossFading)
            {
                music1Source.volume = value;
                music2Source.volume = value;
            }
        }
    }

    private NetworkService network;
    private float _musicVolume;

    public bool musicMute
    {
        get
        {
            if (music1Source != null)
            {
                return music1Source.mute;
            }
            return false;
        }
        set
        {
            if (music1Source != null)
            {
                music1Source.mute = value;
                music2Source.mute = value;
            }
        }
    }

    public void Startup(NetworkService service)
    {
        Debug.Log("audio manager starting...");
        network = service;
        status = ManagerStatus.Started;
        soundVolume = 1f;
        musicVolume = 1f;
        music1Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;
        music2Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerPause = true;
        activeMusic = music1Source;
        inactiveMusic = music2Source;
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

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load<AudioClip>($"Music/{introBgmMusic}"));
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load<AudioClip>($"Music/{levelBgmMusic}"));
    }

    public void StopMusic()
    {
        music1Source.Stop();
        music2Source.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayMusic(AudioClip clip)
    {
        if (crossFading)
        {
            return;
        }
        StartCoroutine(CrossFadeMusic(clip));
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        crossFading = true;
        inactiveMusic.clip = clip;
        inactiveMusic.volume = 0;
        inactiveMusic.Play();
        float scaledRate = crossFadeRate * _musicVolume;
        while (activeMusic.volume > 0)
        {
            activeMusic.volume -= scaledRate * Time.deltaTime;
            inactiveMusic.volume += scaledRate * Time.deltaTime;
            Debug.Log($"active: {activeMusic.volume}");
            Debug.Log($"inactive: {inactiveMusic.volume}");
            yield return null;
        }

        AudioSource temp = activeMusic;
        activeMusic = inactiveMusic;
        inactiveMusic = temp;
        activeMusic.volume = musicVolume;
        Debug.Log($"music volume: {musicVolume}");
        inactiveMusic.Stop();
        crossFading = false;
    }
}
