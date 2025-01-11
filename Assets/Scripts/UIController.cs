using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    //[SerializeField] TMP_Text scoreLabel;
    //[SerializeField] SettingsPopup settingsPopup;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private InventoryPopup popup;

    //private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //settingsPopup.Close();
        //scoreLabel.text = score.ToString();
        OnHealthUpdated();
        popup.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    bool isShowing = !settingsPopup.gameObject.activeSelf;
        //    settingsPopup.gameObject.SetActive(isShowing);
        //    if (isShowing)
        //    {
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;
        //    }
        //    else
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = false;
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
    }

    //public void OnOpenSettings()
    //{
    //    settingsPopup.Open();
    //}

    //public void OnPointerDown()
    //{
    //    Debug.Log("pointer down");
    //}

    private void OnEnable()
    {
        //Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
    }

    private void OnDisable()
    {
        //Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
    }

    //private void OnEnemyHit()
    //{
    //    score++;
    //    scoreLabel.text = score.ToString();
    //}

    private void OnHealthUpdated()
    {
        string message = $"Health: {Managers.Player.health}/{Managers.Player.maxHealth}";
        healthLabel.text = message;
    }
}
