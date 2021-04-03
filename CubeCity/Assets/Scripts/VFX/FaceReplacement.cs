using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceReplacement : ParticlesHandler
{
    private void Start()
    {
        if (VFXEventManager.Instance != null)
        {
            VFXEventManager.Instance.OnFaceReformedOrDemolished += ReplacementEffect;
        }
    }

    private void OnDisable()
    {
        if (VFXEventManager.Instance != null)
        {
            VFXEventManager.Instance.OnFaceReformedOrDemolished -= ReplacementEffect;
        }
    }

    private void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    private void ReplacementEffect(Vector3 finalPosition, Quaternion rotation)
    {
        SetPosition(finalPosition);
        this.transform.rotation = rotation;
        base.Play();
    }
}
