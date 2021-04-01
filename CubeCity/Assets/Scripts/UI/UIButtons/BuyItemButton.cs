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
        Debug.Log("Player Money: " + Player.Instance.CristalsAmount);

        if (Player.Instance.CanBuy(itemPrice))
        {
            Inventory.Instance.AddPowerUpToInventory(powerUpType, amountOfItems);
            Player.Instance.CristalsAmount -= itemPrice;
            SaveLoadController.instance.Save();
            EventsManager.Instance.Buy();
        }
        else
        {
            Debug.Log("No Money");
        }
    }
}
