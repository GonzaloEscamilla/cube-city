using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButtonUtility))]
public class TimeOnQuitButton : ButtonComponent
{
    public override void Release()
    {
        Player.Instance.TimePlayed += LevelManager.control.GetLevelStatistics().ElapsedTime;
        SaveLoadController.instance.Save();
    }
}
