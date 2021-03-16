using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButtonUtility))]
public class BuyItemButton : ButtonComponent
{
    [SerializeField] int itemPrice;

    public override void Release()
    {
        if (Player.Instance.CanBuy(itemPrice))
        {
            //TODO: Dar Item
            Player.Instance.CristalsAmount -= itemPrice;
        }
        else
        {
            Debug.Log("No Money");
        }
    }

}
