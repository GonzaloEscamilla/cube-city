using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUpSelectionButton : MonoBehaviour
{
    [SerializeField] private UILevelSelectionPowerUpHandler[] handlers;
    [SerializeField] private Image imageIcon;
    [SerializeField] private GameObject imageCheck;

    private PowerUpType powerUpType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => OpenSelector());
    }

    public void SetSelection(Image newIcon, PowerUpType powerUpType)
    {
        this.powerUpType = powerUpType;

        imageIcon.gameObject.SetActive(true);
        imageCheck.SetActive(true);
        
        imageIcon.sprite = newIcon.sprite;
    }

    public PowerUpType GetPowerUpType()
    {
        return powerUpType;
    }

    private void OpenSelector()
    {
        foreach (UILevelSelectionPowerUpHandler handler in handlers)
            handler.SetCurrentButton(this);
    }


}
