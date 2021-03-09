using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public static SaveLoadController instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public SaveData saveData;

    public void Save()
    {
        string data = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("GameData", data);
    }

    public void Load()
    {
        string data = PlayerPrefs.GetString("GameData");
        saveData = JsonUtility.FromJson<SaveData>(data);
    }

    private void OnEnable()
    {
        Load();
    }

    private void OnDisable()
    {
        Save();
    }
}