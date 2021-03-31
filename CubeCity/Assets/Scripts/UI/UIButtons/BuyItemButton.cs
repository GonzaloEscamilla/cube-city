using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButtonUtility))]
public class BuyItemButton : ButtonComponent
{
    [SerializeField] private int itemPrice;

    [SerializeField] private int amountOfItems;

    [SerializeField] private PowerUpType powerUpType;

    public override void Release()
    {
        if (Player.Instance.CanBuy(itemPrice))
        {
            Inventory.Instance.AddPowerUpToInventory(powerUpType, amountOfItems);
            Player.Instance.CristalsAmount -= itemPrice;
            SaveLoadController.instance.Save();
        }
        else
        {
            Debug.Log("No Money");
        }
    }
}
