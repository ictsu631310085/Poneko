using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public static DebugController Instance { get; private set; }

    [SerializeField]
    private GUISkin _GUISkin;

    [field: SerializeField]
    public bool ShowDebug { get; private set; }

    private Cat _cat;

    void Awake()
    {
        _GUISkin.button.fontSize = Screen.height / 32;
        _GUISkin.label.fontSize = Screen.height / 32;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!ShowDebug)
        { return; }

        _cat = FindObjectOfType<Cat>();
    }

    void OnGUI()
    {
        GUI.skin = _GUISkin;

        // Toggle Debug buton
        if (GUI.Button(new Rect(0f, Screen.height * 0.95f, Screen.width * 0.5f, Screen.height * 0.05f), "Toggle Debug"))
        {
            ToggleDebug();
        }

        // Quit Button
        if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.95f, Screen.width * 0.2f, Screen.height * 0.05f), "Quit"))
        {
            Application.Quit();
        }

        if (!ShowDebug)
        { return; }

        #region Stats Label
        // Top Background
        GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height * 0.05f), "");

        // Health Label
        GUI.Label(new Rect(Screen.width * 0.02f, Screen.height * 0.005f, Screen.width, Screen.height * 0.04f),
            "Health: " + _cat.CurrentHealth);

        // Hunger Label
        GUI.Label(new Rect(Screen.width * 0.52f, Screen.height * 0.005f, Screen.width, Screen.height * 0.04f),
            "Hunger: " + _cat.CurrentHunger);
        #endregion

        #region Health Button
        // Full Health Button
        if (GUI.Button(new Rect(0f, Screen.height * 0.07f, Screen.width * 0.2f, Screen.height * 0.05f), "Full"))
        {
            _cat.UpdateHealth(_cat.maxHealth);
        }

        // Half Health Button
        if (GUI.Button(new Rect(0f, Screen.height * 0.13f, Screen.width * 0.2f, Screen.height * 0.05f), "Half"))
        {
            _cat.UpdateHealth(-_cat.maxHealth);
            _cat.UpdateHealth(_cat.maxHealth / 2);
        }

        // Zero Health Button
        if (GUI.Button(new Rect(0f, Screen.height * 0.19f, Screen.width * 0.2f, Screen.height * 0.05f), "Zero"))
        {
            _cat.UpdateHealth(-_cat.maxHealth);
        }
        #endregion

        #region Hunger Button
        // Full Hunger Button
        if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.07f, Screen.width * 0.2f, Screen.height * 0.05f), "Full"))
        {
            _cat.UpdateHunger(_cat.maxHunger);
        }

        // Half Hunger Button
        if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.13f, Screen.width * 0.2f, Screen.height * 0.05f), "Half"))
        {
            _cat.UpdateHunger(-_cat.maxHunger);
            _cat.UpdateHunger(_cat.maxHunger / 2);
        }

        // Zero Hunger Button
        if (GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.19f, Screen.width * 0.2f, Screen.height * 0.05f), "Zero"))
        {
            _cat.UpdateHunger(-_cat.maxHunger);
        }
        #endregion

        // Cure Sick Button
        if (GUI.Button(new Rect(0f, Screen.height * 0.31f, Screen.width * 0.2f, Screen.height * 0.05f), "Cure"))
        {
            _cat.isSick = false;
        }
    }

    public void ToggleDebug()
    {
        if (_cat == null)
        {
            _cat = FindObjectOfType<Cat>();
        }

        ShowDebug = !ShowDebug;
    }
}
