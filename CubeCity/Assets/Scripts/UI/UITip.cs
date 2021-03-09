using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTip", menuName = "ScriptableObjects/UI/Tip", order = 0)]
public class UITip : ScriptableObject
{
    [TextArea]
    public string tipDescription;
}
