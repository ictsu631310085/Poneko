using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject[] windows;
    public Vector2 dayHourRange;

    public GameObject[] cats;

    public GameObject[] preloads;

    [field: SerializeField]
    private GameObject _gameOverPanel;

    //
    private DateTime _lastPlayedTime;
    public float awaySeconds;

    public int chosenCat;

    void Awake()
    {
#if UNITY_EDITOR
        Debug.Log(gameObject.name + ": Awake");
#endif

        Instance = this;

        DateTime timeNow = DateTime.Now;

        Debug.Log(timeNow.Hour);

        if (PlayerPrefs.HasKey("savedTime"))
        {
            string timeAsString = PlayerPrefs.GetString("savedTime");
            _lastPlayedTime = DateTime.Parse(timeAsString);

            Debug.Log("LastPlayedTime: " + _lastPlayedTime);

            TimeSpan awayTime = timeNow - _lastPlayedTime;

            awaySeconds = (float) awayTime.TotalSeconds;
        }

        // Spawn Window
        if (timeNow.Hour >= dayHourRange.x && timeNow.Hour < dayHourRange.y)
        {
            Instantiate(windows[0]);
        }
        else
        {
            Instantiate(windows[1]);
        }

        // Spawn Cat
        Instantiate(cats[chosenCat]);

        // Enable Preloads
        foreach (GameObject item in preloads)
        {
            item.SetActive(true);
        }
    }

    void OnApplicationPause()
    {
#if UNITY_EDITOR
        Debug.Log(gameObject.name + ": OnApplicationPause");
#endif

        SaveData();
    }

    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        Debug.Log(gameObject.name + ": OnApplicationQuit");
#endif

        SaveData();
    }

    private void SaveData()
    {
        DateTime savedTime = DateTime.Now;

#if UNITY_EDITOR
        Debug.Log("savedTime: " + savedTime);
#endif

        PlayerPrefs.SetString("savedTime", savedTime.ToString());
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
        PlayerPrefs.DeleteAll();
    }

    public void RetryButon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
