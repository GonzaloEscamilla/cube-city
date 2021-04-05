using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UICubeCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtCubeAmount;

    private int cubeAmount;
    private int CubeAmount
    {
        get
        {
            return cubeAmount;
        }
        set
        {
            cubeAmount = value;

            if (cubeAmount < 0)
                cubeAmount = 0;

            txtCubeAmount.text = cubeAmount.ToString();
        }
    }

    public void Start() => Init();

    private void Init()
    {
        EventsManager.Instance.OnSetCubesAmount += UpdateValue;
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
            EventsManager.Instance.OnSetCubesAmount -= UpdateValue;
    }

    private void UpdateValue(int value)
    {
        CubeAmount = value;
    }
}
