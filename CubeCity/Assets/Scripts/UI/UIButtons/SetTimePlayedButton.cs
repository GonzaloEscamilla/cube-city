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
        hourText.text = time.ToString();
    }
}
