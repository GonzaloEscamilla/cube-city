using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXEventManager : MonoBehaviour
{
    public static VFXEventManager Instance;

    public Action<Vector3,Quaternion> onCubeBuildedEffect;

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
        StartCoroutine(CallWithDealy(finalPosition,rotation));
    }

    private void OnCubeBuildedEffect(CubeBehaviour newCube)
    {
        if (LevelManager.control.CurrentSelectedFace != null)
        {
            //onCubeBuildedEffect?.Invoke(LevelManager.control.CurrentSelectedFace);
        }
    }

    private IEnumerator CallWithDealy(Vector3 finalPosition, Quaternion rotation)
    {
        yield return new WaitForSeconds(settings.explotionParticleDelay);
        onCubeBuildedEffect?.Invoke(finalPosition, rotation);
    }
}
