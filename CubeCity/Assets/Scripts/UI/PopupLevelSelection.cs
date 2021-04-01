using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupLevelSelection : MonoBehaviour
{
    /// <summary>
    /// Reference to the level name text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI levelName;

    /// <summary>
    /// Reference to the level score text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI levelScore;

    /// <summary>
    /// Reference to the level objectives texts.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI[] levelObjectives;

    [SerializeField] private PowerUpSelectionButton[] selectedPowerUps;
    private PowerUpType[] powerUpTypes;

    public PowerUpType[] GetPowerUpTypesForLevel()
    {
        powerUpTypes = new PowerUpType[3];
        for (int i = 0; i < selectedPowerUps.Length; i++)
            powerUpTypes[i] = selectedPowerUps[i].GetPowerUpType();

        return powerUpTypes;
    }
}
