using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private PlayerData _playerData;

    public Inventory Inventory { get; private set; }

    public Material LevelSkybox;

    public int StarsAmount
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerData.StartsAmount.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerData.StartsAmount.ToString(), value);
            EventsManager.Instance.StarsUpdate(StarsAmount);
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

    public int AmountOfCombosMade
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerData.AmountOfCombosMade.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerData.AmountOfCombosMade.ToString(), value);
            EventsManager.Instance.CombosUpdate(AmountOfCombosMade);
        }
    }

    public int AmountOfPowerUpsMade
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerData.AmountOfPowerUpsMade.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerData.AmountOfPowerUpsMade.ToString(), value);
        }
    }

    public int MaxProsperityMade
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerData.MaxProsperityMade.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerData.MaxProsperityMade.ToString(), value);
            EventsManager.Instance.MaxProsperityUpdate(value);
        }
    }

    private void Awake()
    {
        this.transform.parent = null;

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Initialize();
    }


    public void Initialize()
    {
        Inventory = new Inventory();

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
                    PlayerPrefs.SetInt(PlayerData.CristalsAmount.ToString(), 500);
                }
                break;
            case PlayerData.TimePlayed:
                if (!PlayerPrefs.HasKey(PlayerData.TimePlayed.ToString()))
                {
                    PlayerPrefs.SetFloat(PlayerData.TimePlayed.ToString(), 0);
                }
                break;
            case PlayerData.AmountOfCombosMade:
                if (!PlayerPrefs.HasKey(PlayerData.AmountOfCombosMade.ToString()))
                {
                    PlayerPrefs.SetInt(PlayerData.AmountOfCombosMade.ToString(), 0);
                }
                break;
            case PlayerData.AmountOfPowerUpsMade:
                if (!PlayerPrefs.HasKey(PlayerData.AmountOfPowerUpsMade.ToString()))
                {
                    PlayerPrefs.SetInt(PlayerData.AmountOfPowerUpsMade.ToString(), 0);
                }
                break;
            case PlayerData.MaxProsperityMade:
                if (!PlayerPrefs.HasKey(PlayerData.MaxProsperityMade.ToString()))
                {
                    PlayerPrefs.SetInt(PlayerData.MaxProsperityMade.ToString(), 0);
                }
                break;
            default:
                break;
        }
    }

    [ContextMenu("Log Values")]
    public void LogValues()
    {
        Debug.Log("Stars Amount: " + StarsAmount);
        Debug.Log("Cristals Amount: " + CristalsAmount);
        Debug.Log("Combos Amount: " + AmountOfCombosMade);
        Debug.Log("Maxprosperity Amount: " + MaxProsperityMade);
        Debug.Log("Amount Of Powerups Amount: " + AmountOfPowerUpsMade);
        Debug.Log("Time Played: " + TimePlayed);
    }

    public bool CanBuy(int neddedAmount)
    {
        return CristalsAmount > neddedAmount;
    }

    [ContextMenu("ResetPlayerValues")]
    private void ResetPlayerValues()
    {
        CristalsAmount = 500;
    }

}

public enum PlayerData
{
    StartsAmount,
    CristalsAmount,
    TimePlayed,
    AmountOfCombosMade,
    AmountOfPowerUpsMade,
    MaxProsperityMade
}


