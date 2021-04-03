﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXEventManager : MonoBehaviour
{
    public static VFXEventManager Instance;

    public Action<Vector3,Quaternion> onCubeBuildedEffect;
    public Action<Vector3, Quaternion> OnFaceReformedOrDemolished;

    [SerializeField] private GameSettingsSO settings;

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
        EventsManager.Instance.OnCubeMovingToPosition += OnCubeMovingEffect;
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnCubeMovingToPosition -= OnCubeMovingEffect;
        }
    }

    public void FaceReformedOrDemolished(Vector3 position, Quaternion rotation)
    {
        OnFaceReformedOrDemolished?.Invoke(position, rotation);
    }

    private void OnCubeMovingEffect(Vector3 finalPosition, Quaternion rotation)
    {
        StartCoroutine(CallWithDealy(finalPosition,rotation));
    }

    private IEnumerator CallWithDealy(Vector3 finalPosition, Quaternion rotation)
    {
        yield return new WaitForSeconds(settings.explotionParticleDelay);
        onCubeBuildedEffect?.Invoke(finalPosition, rotation);
    }
}
