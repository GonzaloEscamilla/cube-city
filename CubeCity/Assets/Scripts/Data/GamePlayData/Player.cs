using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private PlayerData _playerData;

    public int StarsAmount
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerData.StartsAmount.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerData.StartsAmount.ToString(), value);
        }
    }

    public float TimePlayed
    {
        get
        {
            return PlayerPrefs.GetFloat(PlayerData.TimePlayed.ToString());
        }
        set
        {
            PlayerPrefs.SetFloat(PlayerData.TimePlayed.ToString(), value);
        }
    }

    public int CristalsAmount
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerData.CristalsAmount.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerData.CristalsAmount.ToString(), value);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Initialize();
    }

    public void Initialize()
    {
        switch (_playerData)
        {
            case PlayerData.StartsAmount:
                if (!PlayerPrefs.HasKey(PlayerData.StartsAmount.ToString()))
                {
                    PlayerPrefs.SetInt(PlayerData.StartsAmount.ToString(), 0);
                }
                break;
            case PlayerData.CristalsAmount:
                if (!PlayerPrefs.HasKey(PlayerData.CristalsAmount.ToString()))
                {
                    PlayerPrefs.SetInt(PlayerData.CristalsAmount.ToString(), 0);
                }
                break;
            case PlayerData.TimePlayed:
                if (!PlayerPrefs.HasKey(PlayerData.TimePlayed.ToString()))
                {
                    PlayerPrefs.SetFloat(PlayerData.TimePlayed.ToString(), 0);
                }
                break;
            default:
                break;
        }
    }

    [ContextMenu("teST")]
    public void Test()
    {
        Debug.Log(StarsAmount);
        StarsAmount = 1;
        Debug.Log(StarsAmount);
    }
}

public enum PlayerData
{
    StartsAmount,
    CristalsAmount,
    TimePlayed
}
