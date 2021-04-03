using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : ParticlesHandler
{
    private void Start()
    {
        if (VFXEventManager.Instance != null)
        {
            VFXEventManager.Instance.onCubeBuildedEffect += onCubeBuilded;
        }
    }

    private void OnDisable()
    {
        if (VFXEventManager.Instance != null)
        { 
            VFXEventManager.Instance.onCubeBuildedEffect -= onCubeBuilded;
        }
    }

    private void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    private void onCubeBuilded(Vector3 finalPosition, Quaternion rotation)
    {
        Debug.Log("Explotion");
        SetPosition(finalPosition);
        this.transform.rotation = rotation;
        base.Play();
    }
}
