using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInitializer : MonoBehaviour
{
    [SerializeField] private PowerUpIcons powerUpIcons;
    [SerializeField] private PowerUpButton[] levelPowerups;

    private void Start() => Init();

    public void Init()
    {
        // Se presupone que siempre son 3 la cantidad de powerups que podes usar en el nivel.
        for (int i = 0; i < Player.Instance.Inventory.powerUpsForLevel.Length; i++)
        {
            levelPowerups[i].Init(powerUpIcons.GetIconByType(Player.Instance.Inventory.powerUpsForLevel[i]), Player.Instance.Inventory.powerUpsForLevel[i]);
        }
    }
}
