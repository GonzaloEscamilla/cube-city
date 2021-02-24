using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButtonUtility))]
public class PlayLevelButton : ButtonComponent
{
    [SerializeField]
    private SelectLevelHandler levelHandler;

    public override void Release()
    {
        levelHandler.LoadSelectedLevel();
    }
}
