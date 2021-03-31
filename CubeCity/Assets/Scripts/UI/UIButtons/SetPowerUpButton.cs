using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetPowerUpButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountText;

    [SerializeField]
    private PowerUpType powerUpType;

    private void Start()
    {
        amountText.text = "X" + Inventory.Instance.GetPowerUpFromInventory(powerUpType);
    }
}
