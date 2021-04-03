using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public static PowerUpsManager Instance;

    [SerializeField] private InputManager _inputManager;

    private PowerUpType currentPowerUp;
    private PowerUpButton currentPresedButton;

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

        Debug.LogWarning("awake on powerup manager");
    }

    public void Init(InputManager input)
    {
        _inputManager = input;
    }

    private void Start()
    {
        Debug.Log("Start in PowerUpsManger");
        if (!_inputManager)
        {
            Debug.LogWarning("There is not an input manager assigned to this object. It will not work.", this.gameObject);
        }
    }

    public void PowerUpSelected(PowerUpType selectedPowerUp, PowerUpButton currentPresedButton)
    {
        currentPowerUp = selectedPowerUp;

        if (selectedPowerUp != PowerUpType.Builder)
        {
            _inputManager.IsOnPowerUpMode = true;
            this.currentPresedButton = currentPresedButton;
        }
        else
        {
            LevelManager.control.GetCubeSpawner().ActivateExtraCubes();
           
            currentPowerUp = PowerUpType.None;

            if (currentPresedButton != null)
            {
                currentPresedButton.HasBeingUsed = true;
                currentPresedButton = null;
            }

            Player.Instance.Inventory.UsePowerUpFromInventory(selectedPowerUp);
        }
    }

    public void DoPowerUp()
    {
        DoPowerUp(null);
    }

    public void DoPowerUp(Face selectedFace)
    {
        if (selectedFace == null)
        {
            Debug.LogWarning("You can't do a power up if there is not a face selected.");
            return;
        }

        switch (currentPowerUp)
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Demolition:
                selectedFace.Demolition();
                break;
            case PowerUpType.Builder:
                break;
            case PowerUpType.ReformResidentialArea:
                selectedFace.ReformFace(FaceTypes.ResidentialArea);
                break;
            case PowerUpType.ReformParkArea:
                selectedFace.ReformFace(FaceTypes.ParkArera);
                break;
            case PowerUpType.ReformComercial:
                selectedFace.ReformFace(FaceTypes.CommercialArea);
                break;
            case PowerUpType.ReformAgricultural:
                selectedFace.ReformFace(FaceTypes.FarmArea);
                break;
            case PowerUpType.ReformBusinesArea:
                selectedFace.ReformFace(FaceTypes.BusinessArea);
                break;
            case PowerUpType.ReformIndustrialArea:
                selectedFace.ReformFace(FaceTypes.IndustrialArea);
                break;
            default:
                break;
        }

        currentPowerUp = PowerUpType.None;

        if (currentPresedButton != null)
        {
            currentPresedButton.HasBeingUsed = true;
            currentPresedButton = null;
        }
        Player.Instance.Inventory.UsePowerUpFromInventory(currentPowerUp);
    }



}
