using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXEventManager : MonoBehaviour
{
    public static VFXEventManager Instance;

    public Action<Vector3,Quaternion> onCubeBuildedEffect;

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
        //EventsManager.Instance.onCubeBuilded += OnCubeBuildedEffect;
        EventsManager.Instance.OnCubeMovingToPosition += OnCubeMovingEffect;
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
           // EventsManager.Instance.onCubeBuilded -= OnCubeBuildedEffect;
            EventsManager.Instance.OnCubeMovingToPosition -= OnCubeMovingEffect;

        }
    }


    private void OnCubeMovingEffect(Vector3 finalPosition, Quaternion rotation)
    {
        Debug.Log("Effectito");
        onCubeBuildedEffect?.Invoke(finalPosition, rotation);
    }

    private void OnCubeBuildedEffect(CubeBehaviour newCube)
    {
        if (LevelManager.control.CurrentSelectedFace != null)
        {
            //onCubeBuildedEffect?.Invoke(LevelManager.control.CurrentSelectedFace);
        }
    }
}
