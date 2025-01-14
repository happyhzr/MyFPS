using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagerProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagerStarted);
    }

    private void OnDisable()
    {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagerProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagerStarted);
    }

    private void OnManagerProgress(int numReady, int numModules)
    {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }


    private void OnManagerStarted()
    {
        Managers.Mission.GoToNext();
    }
}
