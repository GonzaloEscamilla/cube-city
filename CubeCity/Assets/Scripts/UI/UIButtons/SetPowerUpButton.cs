using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SetPowerUpButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountText;

    [SerializeField]
    private PowerUpType powerUpType;


    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnBuy += UpdateValues;
        }
    }
    private void Start() => Init();

    public void Init()
    {
        EventsManager.Instance.OnBuy += UpdateValues;
        amountText.text = "X" + Inventory.Instance.GetPowerUpFromInventory(powerUpType);
    }

    private void UpdateValues()
    {
         amountText.text = "X" + Inventory.Instance.GetPowerUpFromInventory(powerUpType);
    }

    
}
