using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class PowerUpButton : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation buttonsAnimation;
    [SerializeField] private DOTweenAnimation usePowerupButtonsAnimation;
    [SerializeField] private Image icon;

    [Space]

    [SerializeField] private Image buttonBackground; 
    [SerializeField] private Sprite deactivatedBackground;
    [SerializeField] private Color deactivatedColor;

    [SerializeField] private PowerUpType _myPowerUp;
    public PowerUpType MyPowerUp
    {
        get
        {
            return _myPowerUp;
        }
        set
        {
            _myPowerUp = value;
        }
    }

    private bool hasBeingUsed;
    public bool HasBeingUsed
    {
        get
        {
            return hasBeingUsed;
        }
        set
        {
            hasBeingUsed = value;
            if (hasBeingUsed)
            {
                buttonsAnimation.DOPlayBackwards();
                usePowerupButtonsAnimation.DOPlayBackwards();

                DisableGraphics();
            }
        }
    }

    private void DisableGraphics()
    {
        buttonBackground.sprite = deactivatedBackground;
        buttonBackground.color = deactivatedColor;
        icon.color = deactivatedColor;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SetPowerUp());
    }

    public void Init(Sprite icon, PowerUpType type)
    {
        _myPowerUp = type;
        this.icon.sprite = icon;
    }

    private void SetPowerUp()
    {
        if (hasBeingUsed || _myPowerUp == PowerUpType.None)
        {
            Debug.LogWarning("The power ups is TYpe: " + PowerUpType.None.ToString());
            Debug.LogWarning("Has Being Used");
            return;
        }

        buttonsAnimation.DOPlayForward();
        usePowerupButtonsAnimation.DOPlayForward();

        PowerUpsManager.Instance.PowerUpSelected(MyPowerUp, this);
    }
}
