using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PowerUpButton : MonoBehaviour
{
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

    private void SetPowerUp()
    {
        if (hasBeingUsed)
            return;

        PowerUpsManager.Instance.PowerUpSelected(MyPowerUp, this);
    }
}
