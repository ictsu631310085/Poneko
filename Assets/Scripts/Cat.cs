using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public float maxHealth;
    public float healthDropRate;

    public float healthFromPetting;

    public float angryHealthDrop;
    public float sickHealthDropRate;

    [field: SerializeField]
    public Vector2 SleepDurationRange { get; private set; }

    public float maxHunger;
    public float hungerRate;

    [Range(0f, 1f)]
    public float eatThreshold;

    [field: SerializeField]
    public Vector2 EatDurationRange { get; private set; }

    [field: SerializeField]
    public Vector2 WaitDurationRange { get; private set; }

    //
    public float idleChance;

    public float walkChance;
    public Vector2 boundaryX;
    public Vector2 boundaryY;

    public float angryChance;
    public Vector2 angryDurationRange;

    public float sickChanceFactor;

    //
    [field: SerializeField]
    public float CurrentHealth { get; private set; }

    [field: SerializeField]
    public float CurrentHunger { get; private set; }

    public float sickChance;
    public bool isSick;

    public Slider HealthBar { get; set; }
    public Slider HungerBar { get; set; }

    public GameObject target;

    // load
    private float loadedHealth;
    private float loadedHunger;
    private string loadedIsSick;
    private bool isDataLoaded;

    //
    [HideInInspector]
    public Draggable draggable;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Debug.Log(gameObject.name + ": Start");
#endif

        draggable = GetComponent<Draggable>();

        loadedHealth = PlayerPrefs.GetFloat("Health", maxHealth);
        float awayHealthDecrease = (GameController.Instance.awaySeconds * healthDropRate);
        float initHealth = loadedHealth - awayHealthDecrease;
        InitializeHealth(initHealth);

        loadedHunger = PlayerPrefs.GetFloat("Hunger", maxHunger);
        float awayHungerDecrease = (GameController.Instance.awaySeconds * hungerRate);
        float initHunger = loadedHunger - awayHungerDecrease;
        InitializeHunger(initHunger);

        loadedIsSick = PlayerPrefs.GetString("isSick", "False");
        isSick = loadedIsSick == "True";

        isDataLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth > 0.0f)
        {
            if (!isSick)
            {
                UpdateHealth(-healthDropRate * Time.deltaTime);
            }
            else
            {
                UpdateHealth(-sickHealthDropRate * Time.deltaTime);
            }
        }

        if (CurrentHunger > 0.0f)
        {
            UpdateHunger(-hungerRate * Time.deltaTime);
        }

        sickChance = (1f - (CurrentHealth / maxHealth)) * sickChanceFactor;
    }

    void OnApplicationPause()
    {
#if UNITY_EDITOR
        Debug.Log(gameObject.name + ": OnApplicationPause");
#endif

        if (!isDataLoaded)
        { return; }

        SaveData();
    }

    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        Debug.Log(gameObject.name + ": OnApplicationQuit");
#endif

        SaveData();
    }

    private void InitializeHunger(float amount)
    {
        CurrentHunger = amount;
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0.0f, maxHunger);
    }

    public void UpdateHunger(float amount)
    {
        CurrentHunger += amount;
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0.0f, maxHunger);

        if (HungerBar)
        {
            HungerBar.value = CurrentHunger;
        }
    }

    private void InitializeHealth(float amount)
    {
        CurrentHealth = amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0.0f, maxHealth);
    }

    public void UpdateHealth(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0.0f, maxHealth);

        if (HealthBar)
        {
            HealthBar.value = CurrentHealth;
        }

        if (CurrentHealth == 0f && GameController.Instance)
        {
            GameController.Instance.GameOver();
        }
    }

    public void SaveData()
    {
#if UNITY_EDITOR
        Debug.Log("CurrentHealth: " + CurrentHealth + "   CurrentHunger: " + CurrentHunger + "    isSick: " + isSick);
#endif
        
        PlayerPrefs.SetFloat("Health", CurrentHealth);
        PlayerPrefs.SetFloat("Hunger", CurrentHunger);
        PlayerPrefs.SetString("isSick", isSick.ToString());
    }
}
