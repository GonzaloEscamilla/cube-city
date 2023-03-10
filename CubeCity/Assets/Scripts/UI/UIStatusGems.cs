using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIStatusGems : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;

    private void Start() => Init();

    private void Init()
    {
        EventsManager.Instance.OnSceneLoaded += UpdateValues;
        EventsManager.Instance.OnBuy += UpdateValues;
        EventsManager.Instance.OnAchievementRedimed += UpdateValues;
        UpdateValues();
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnSceneLoaded -= UpdateValues;
        EventsManager.Instance.OnBuy -= UpdateValues;
        EventsManager.Instance.OnAchievementRedimed -= UpdateValues;
    }

    public void UpdateValues()
    {
        UpdateValues(GameScenes.Level);
    }

    public void UpdateValues(GameScenes scene)
    {
        amountText.text = Player.Instance.CristalsAmount.ToString();
    }
}
