using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : Singleton<PowerUpsManager>
{
    [SerializeField] private InputManager _inputManager;

    private PowerUpType currentPowerUp;
    private PowerUpButton currentPresedButton;

    private void Start()
    {
        if (!_inputManager)
        {
            Debug.LogWarning("There is not an input manager assigned to this object. It will not work.", this.gameObject);
        }
    }

    public void PowerUpSelected(PowerUpType selectedPowerUp, PowerUpButton currentPresedButton)
    {
        currentPowerUp = selectedPowerUp;
        _inputManager.IsOnPowerUpMode = true;
        this.currentPresedButton = currentPresedButton;
    }

    public void DoPowerUp()
    {
        DoPowerUp(null);
    }

    public void DoPowerUp(Face selectedFace)
    {
        switch (currentPowerUp)
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Demolition:
                selectedFace.Demolition();
                break;
            case PowerUpType.Builder:
                LevelManager.control.GetCubeSpawner().ActivateExtraCubes();
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
            currentPresedButton.hasBeingUsed = true;
            currentPresedButton = null;
        }
    }

   

}
