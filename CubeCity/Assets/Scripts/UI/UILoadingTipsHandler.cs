using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingTipsHandler : MonoBehaviour
{
    [SerializeField] private UILoadingTips _UILoadingTips;
    [SerializeField] private TextMeshProUGUI textTip;

    private void Awake() => LoadTip();

    private void LoadTip()
    {
        textTip.text = _UILoadingTips.tips[UnityEngine.Random.Range(0, _UILoadingTips.tips.Length)].tipDescription;
    }
}
