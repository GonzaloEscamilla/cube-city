using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILevelSelectionPowerUpHandler : MonoBehaviour
{
    [SerializeField] private SetPowerUpButton powerUpButton;
    [SerializeField] private Image powerUpImage;
    [SerializeField] private DOTweenAnimation dimAnimation;
    [SerializeField] private DOTweenAnimation powerupsAnimation;

    private PowerUpSelectionButton currentButton;

    private void Start() => Init();

    private void Init()
    {
        GetComponent<Button>().onClick.AddListener(() => AssignPowerUpToButton());
    }

    private void AssignPowerUpToButton()
    {
        if (Player.Instance.Inventory.GetPowerUpFromInventory(powerUpButton.GetPowerUpType()) > 0)
        {
            currentButton.SetSelection(powerUpImage, powerUpButton.GetPowerUpType());
            dimAnimation.DOPlayBackwards();
            powerupsAnimation.DOPlayBackwards();
        }
    }

    internal void SetCurrentButton(PowerUpSelectionButton powerUpSelectionButton)
    {
        currentButton = powerUpSelectionButton;
    }
}
