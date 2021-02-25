using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXEventManager : Singleton<VFXEventManager>
{
    private void Awake()
    {
        this.transform.parent = null;
        DontDestroyOnLoad(Instance);
    }

    private void OnEnable()
    {
        EventsManager.Instance.onCubeBuilded += OnCubeBuildedEffect;
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.onCubeBuilded -= OnCubeBuildedEffect;
        }
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
