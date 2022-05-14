using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(UIButtonUtility))]
public class SetTimePlayedButton : ButtonComponent
{
    [SerializeField]
    private TextMeshProUGUI hourText;

    public override void Release()
    {
        TimeSpan time = TimeSpan.FromSeconds(Player.Instance.TimePlayed);
        hourText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }
}
