using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXEventManager : MonoBehaviour
{
    public static VFXEventManager Instance;

    private void Awake()
    {
        this.transform.parent = null;

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
