using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXEventManager : Singleton<VFXEventManager>
{
    private void OnEnable()
    {
        EventsManager.control.onCubeBuilded += OnCubeBuildedEffect;
    }

    private void OnDisable()
    {
        EventsManager.control.onCubeBuilded -= OnCubeBuildedEffect;
    }

    public Action<Face> onCubeBuildedEffect;

    private void OnCubeBuildedEffect(CubeBehaviour newCube)
    {
        if (LevelManager.control.CurrentSelectedFace != null)
        {
            onCubeBuildedEffect?.Invoke(LevelManager.control.CurrentSelectedFace);
        }
    }
}
