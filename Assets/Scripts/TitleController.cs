using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public GameObject gameController;

    public GameObject debugController;

    [SerializeField]
    [Header("Info")]
    private bool _isDebug;

    // Start is called before the first frame update
    void Start()
    {
        Input.backButtonLeavesApp = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameController.SetActive(true);

        if (!_isDebug)
        {
            Destroy(debugController);
        }
        else
        {
            debugController.SetActive(true);
        }

        Destroy(gameObject);
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetDebug(bool value)
    {
        _isDebug = value;
    }
}
