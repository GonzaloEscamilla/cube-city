using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpIcons", menuName = "ScriptableObjects/UI/PowerUpIcons", order = 2)]
public class PowerUpIcons : ScriptableObject
{
    [SerializeField] private PowerUpIcon[] powerUpIcons;

    [ContextMenu("Initialize")]
    public void Init()
    {
        powerUpIcons = new PowerUpIcon[System.Enum.GetNames(typeof(PowerUpType)).Length];

        for (int i = 0; i < powerUpIcons.Length; i++)
        {

            powerUpIcons[i] = new PowerUpIcon();
            powerUpIcons[i].type = (PowerUpType)i;
            powerUpIcons[i].Name = ((PowerUpType)i).ToString();
        }
    }

    public Sprite GetIconByType(PowerUpType type)
    {
        return powerUpIcons[(int)type].icon;
    }
}

[System.Serializable]
public class PowerUpIcon
{
    [HideInInspector]
    public string Name;
    public PowerUpType type;
    public Sprite icon;
}