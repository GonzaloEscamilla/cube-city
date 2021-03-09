using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadingSceneSettings", menuName = "ScriptableObjects/UI/LoadingTipsSettings", order = 0)]
public class UILoadingTips : ScriptableObject
{
    public UITip[] tips;
}
