using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory
{
    private PowerUpType powerUpType;

    public PowerUpType[] powerUpsForLevel;

    public Inventory()
    {
        Initialize();
    }

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

    public int GetPowerUpFromInventory(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Demolition:
                return Demolition;
            case PowerUpType.Builder:
                return Builder;
            case PowerUpType.ReformResidentialArea:
                return ReformResidentialArea;
            case PowerUpType.ReformParkArea:
                return ReformParkArea;
            case PowerUpType.ReformComercial:
                return ReformComercial;
            case PowerUpType.ReformAgricultural:
                return ReformAgricultural;
            case PowerUpType.ReformBusinesArea:
                return ReformBusinesArea;
            case PowerUpType.ReformIndustrialArea:
                return ReformIndustrialArea;
        }

        return 0;
    }

    public void UsePowerUpFromInventory(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Demolition:
                Demolition--;
                break;
            case PowerUpType.Builder:
                Builder--;
                break;
            case PowerUpType.ReformResidentialArea:
                ReformResidentialArea--;
                break;
            case PowerUpType.ReformParkArea:
                ReformParkArea--;
                break;
            case PowerUpType.ReformComercial:
                ReformComercial--;
                break;
            case PowerUpType.ReformAgricultural:
                ReformAgricultural--;
                break;
            case PowerUpType.ReformBusinesArea:
                ReformBusinesArea--;
                break;
            case PowerUpType.ReformIndustrialArea:
                ReformIndustrialArea--;
                break;
            default:
                break;
        }
    }
}
