using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButtonUtility))]
public class PlayLevelButton : ButtonComponent
{
    private SelectLevelHandler levelHandler;

    private void Awake()
    {
        levelHandler = FindObjectOfType<SelectLevelHandler>();
    }

    public override void Release()
    {
        levelHandler.LoadSelectedLevel();
    }
}
