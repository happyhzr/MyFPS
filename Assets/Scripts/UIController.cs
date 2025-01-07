using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] SettingsPopup settingsPopup;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        settingsPopup.Close();
        //scoreLabel.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isShowing = !settingsPopup.gameObject.activeSelf;
            settingsPopup.gameObject.SetActive(isShowing);
            if (isShowing)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void OnOpenSettings()
    {
        settingsPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnEnemyHit()
    {
        score++;
        scoreLabel.text = score.ToString();
    }
}
