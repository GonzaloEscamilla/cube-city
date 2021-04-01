using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButtonUtility))]
public class PlayLevelButton : ButtonComponent
{
    [SerializeField] private PopupLevelSelection levelSelection;

    private SelectLevelHandler levelHandler;

    private void Awake()
    {
        levelHandler = FindObjectOfType<SelectLevelHandler>();
    }

    public override void Release()
    {
        Player.Instance.Inventory.powerUpsForLevel = levelSelection.GetPowerUpTypesForLevel();
        levelHandler.LoadSelectedLevel();
    }
}
