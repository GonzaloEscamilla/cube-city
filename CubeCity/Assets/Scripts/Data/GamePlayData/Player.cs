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
            EventsManager.Instance.OnStartsUpdate(PlayerPrefs.GetInt(PlayerData.StartsAmount.ToString()));
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
            EventsManager.Instance.OnCombosUpdate(PlayerPrefs.GetInt(PlayerData.AmountOfCombosMade.ToString()));
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
        DontDestroyOnLoad(this);
        Initialize();
    }

    public void Initialize()
    {
        switch (_playerData)
        {
            case PlayerData.StartsAmount:
                    PlayerPrefs.SetInt(PlayerData.StartsAmount.ToString(), 0);
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

    public bool CanBuy(int neddedAmount)
    {
        return CristalsAmount > neddedAmount;
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

public class Inventory : Singleton<Inventory>
{
    private PowerUpType powerUpType;

    public int Demolition
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.Demolition.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.Demolition.ToString(), value);
        }
    }

    public int Builder
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.Builder.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.Builder.ToString(), value);
        }
    }

    public int ReformResidentialArea
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.ReformResidentialArea.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.ReformResidentialArea.ToString(), value);
        }
    }

    public int ReformParkArea
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.ReformParkArea.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.ReformParkArea.ToString(), value);
        }
    }

    public int ReformComercial
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.ReformComercial.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.ReformComercial.ToString(), value);
        }
    }

    public int ReformAgricultural
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.ReformAgricultural.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.ReformAgricultural.ToString(), value);
        }
    }

    public int ReformBusinesArea
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.ReformBusinesArea.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.ReformBusinesArea.ToString(), value);
        }
    }

    public int ReformIndustrialArea
    {
        get
        {
            return PlayerPrefs.GetInt(PowerUpType.ReformIndustrialArea.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PowerUpType.ReformIndustrialArea.ToString(), value);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Initialize();
    }

    public void Initialize()
    {
        switch (powerUpType)
        {
            case PowerUpType.Demolition:
                if (!PlayerPrefs.HasKey(PowerUpType.Demolition.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.Demolition.ToString(), 0);
                break;
            case PowerUpType.Builder:
                if (!PlayerPrefs.HasKey(PowerUpType.Builder.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.Builder.ToString(), 0);
                break;
            case PowerUpType.ReformResidentialArea:
                if (!PlayerPrefs.HasKey(PowerUpType.ReformResidentialArea.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.ReformResidentialArea.ToString(), 0);
                break;
            case PowerUpType.ReformParkArea:
                if (!PlayerPrefs.HasKey(PowerUpType.ReformParkArea.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.ReformParkArea.ToString(), 0);
                break;
            case PowerUpType.ReformComercial:
                if (!PlayerPrefs.HasKey(PowerUpType.ReformComercial.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.ReformComercial.ToString(), 0);
                break;
            case PowerUpType.ReformAgricultural:
                if (!PlayerPrefs.HasKey(PowerUpType.ReformAgricultural.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.ReformAgricultural.ToString(), 0);
                break;
            case PowerUpType.ReformBusinesArea:
                if (!PlayerPrefs.HasKey(PowerUpType.ReformBusinesArea.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.ReformBusinesArea.ToString(), 0);
                break;
            case PowerUpType.ReformIndustrialArea:
                if (!PlayerPrefs.HasKey(PowerUpType.ReformIndustrialArea.ToString()))
                    PlayerPrefs.SetInt(PowerUpType.ReformIndustrialArea.ToString(), 0);
                break;
            default:
                break;
        }
    }

    public void AddPowerUpToInventory(PowerUpType type, int powerUpAmount)
    {
        switch (type)
        {
            case PowerUpType.Demolition:
                Demolition += powerUpAmount;
                break;
            case PowerUpType.Builder:
                Builder += powerUpAmount;
                break;
            case PowerUpType.ReformResidentialArea:
                ReformResidentialArea += powerUpAmount;
                break;
            case PowerUpType.ReformParkArea:
                ReformParkArea += powerUpAmount;
                break;
            case PowerUpType.ReformComercial:
                ReformComercial += powerUpAmount;
                break;
            case PowerUpType.ReformAgricultural:
                ReformAgricultural += powerUpAmount;
                break;
            case PowerUpType.ReformBusinesArea:
                ReformBusinesArea += powerUpAmount;
                break;
            case PowerUpType.ReformIndustrialArea:
                ReformIndustrialArea += powerUpAmount;
                break;
            default:
                break;
        }
    }
}
