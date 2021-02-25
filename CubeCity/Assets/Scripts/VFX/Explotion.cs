using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : ParticlesHandler
{
    private void OnEnable()
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

    private void onCubeBuilded(Face currentSelectedFace)
    {
        SetPosition(currentSelectedFace.transform.position);
        this.transform.rotation = currentSelectedFace.transform.rotation;
        base.Play();
    }
}
