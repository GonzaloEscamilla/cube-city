using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PowerUpButton : MonoBehaviour
{
    [SerializeField] private Image icon;

    [SerializeField] private PowerUpType _myPowerUp;
    public PowerUpType MyPowerUp
    {
        get
        {
            return _myPowerUp;
        }
        set
        {
            _myPowerUp = value;
        }
    }

    public bool hasBeingUsed;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SetPowerUp());
    }

    public void Init(Sprite icon, PowerUpType type)
    {
        _myPowerUp = type;
        this.icon.sprite = icon;
    }

    private void SetPowerUp()
    {
        if (hasBeingUsed)
            return;

        PowerUpsManager.Instance.PowerUpSelected(MyPowerUp, this);
    }
}
