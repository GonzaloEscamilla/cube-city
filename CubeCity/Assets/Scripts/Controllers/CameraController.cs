using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

/// <summary>
/// Use this class to make changes on the camera position, rotation, and zooming values.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// The initial camera position.
    /// </summary>
    [SerializeField] private Vector3 cameraOriginalPosition;

    /// <summary>
    /// The target that the CM Camera will follow.
    /// </summary>
    [SerializeField] private Transform target;

    /// <summary>
    /// Rotation sensibility.
    /// </summary>
    [SerializeField] private float rotationSensibility = 100;

    /// <summary>
    /// The zoom of the FreeLook Camera, based on the initial FieldOfView.
    /// </summary>
    [SerializeField] private float cameraZoom = 40;

    /// <summary>
    /// Zoom Speed.
    /// </summary>
    private float zoomSpeed = 0.1f;

    /// <summary>
    /// Advance Settings.
    /// </summary>
    [SerializeField] private AdvanceSettings advanceSettings;
    
    /// <summary>
    /// CineMachine FreeLookCamera.
    /// </summary>
    [SerializeField] private CinemachineFreeLook CMCamera;

    /// <summary>
    /// Scene Main Camera.
    /// </summary>
    [SerializeField] private Camera mainCamera;

    private Coroutine transitionCoroutine;

    private bool isZooming;
    private Coroutine zoomingRoutine;
    private float zoomAmountAdded;
    private float zoomFactor = 0.25f;

    private TopRigRadius topRigRadius = new TopRigRadius();
    private MiddleRigRadius middleRigRadius = new MiddleRigRadius();
    private BottomRigRadius bottomRigRadius = new BottomRigRadius();

    private void Awake()
    {
        SetTarget();
    }

    private void Start()
    {
        EventsManager.Instance.onCubeBuilded += ReCenterCamera;
        EventsManager.Instance.onfaceSelected += PositionAndRorationTransition;
        mainCamera = FindObjectOfType<Camera>();
    }

 
    private void OnDestroy()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.onCubeBuilded -= ReCenterCamera;
            EventsManager.Instance.onfaceSelected -= PositionAndRorationTransition;

        }
    }

    /// <summary>
    /// Resets the camera to its original location.
    /// </summary>
    public void Reset()
    {
        // TODO: Resetea la camara a su posicion inicial.
    }

    /// <summary>
    /// Sets the target you want the camera to follow.
    /// </summary>
    private void SetTarget()
    {
        // TODO: Revisar si es necesario hacer una sobrecarga del metodo para resivir otro target. De ser asi revisar su documentacion.

        CMCamera.Follow = target;
        CMCamera.LookAt = target;
    }

    /// <summary>
    /// Returns the scene main camera.
    /// </summary>
    /// <returns></returns>
    public Camera GetCamera()
    {
        return Camera.main;
    }

    /// <summary>
    /// Returns the CineMachine FreeLook camera used in the scene.
    /// </summary>
    /// <returns></returns>
    public CinemachineFreeLook GetCMCamera()
    {
        return CMCamera;
    }
    
    /// <summary>
    /// Returns the current target the CM camera is following.
    /// </summary>
    /// <returns></returns>
    public Transform GetTarget()
    {
        return target;
    }


    /// <summary>
    /// Performs a Zoom on the camera filed of view.
    /// </summary>
    /// <param name="zoomValue"></param>
    public void Zoom(float zoomValue)
    {
        if (!isZooming)
        {
            isZooming = true;
            zoomAmountAdded = zoomValue * zoomFactor;
            zoomingRoutine = StartCoroutine(Zooming());
        }
        else
        {
            zoomAmountAdded += zoomValue * zoomFactor;
        }
        // TODO: Tanto el zoom como el rotate deberian ser corutinas, para poder lerpear.
        //CMCamera.m_Lens.FieldOfView = Mathf.Clamp(CMCamera.m_Lens.FieldOfView + (zoomValue * zoomSpeed), 15, 90);
    }

    public void ZoomStop()
    {
        Debug.Log("Stop zooming");
        isZooming = false;
    }

    private IEnumerator Zooming()
    {
        Debug.Log("Start Zooming");
        float zoomObjetiveTopRig = Mathf.Clamp((CMCamera.m_Orbits[0].m_Radius + zoomAmountAdded), topRigRadius.minDistance, topRigRadius.maxDistance);
        float zoomObjetiveMiddleRig = Mathf.Clamp((CMCamera.m_Orbits[1].m_Radius + zoomAmountAdded), middleRigRadius.minDistance, middleRigRadius.maxDistance);
        float zoomObjetiveBottomRig = Mathf.Clamp((CMCamera.m_Orbits[2].m_Radius + zoomAmountAdded), bottomRigRadius.minDistance, bottomRigRadius.maxDistance);

        /*
        while (isZooming || Mathf.Abs((zoomObjetiveMiddleRig - CMCamera.m_Orbits[1].m_Radius)) >= 1f)
        {
            zoomObjetiveTopRig = Mathf.Clamp((CMCamera.m_Orbits[0].m_Radius + zoomAmountAdded), topRigRadius.minDistance, topRigRadius.maxDistance);
            zoomObjetiveMiddleRig = Mathf.Clamp((CMCamera.m_Orbits[1].m_Radius + zoomAmountAdded), middleRigRadius.minDistance, middleRigRadius.maxDistance);
            zoomObjetiveBottomRig = Mathf.Clamp((CMCamera.m_Orbits[2].m_Radius + zoomAmountAdded), bottomRigRadius.minDistance, bottomRigRadius.maxDistance);

            CMCamera.m_Orbits[0].m_Radius = Mathf.Lerp(CMCamera.m_Orbits[0].m_Radius, zoomObjetiveTopRig, zoomSpeed);
            CMCamera.m_Orbits[1].m_Radius = Mathf.Lerp(CMCamera.m_Orbits[1].m_Radius, zoomObjetiveTopRig, zoomSpeed);
            CMCamera.m_Orbits[2].m_Radius = Mathf.Lerp(CMCamera.m_Orbits[2].m_Radius, zoomObjetiveTopRig, zoomSpeed);

            yield return null;
        }*/

        while (Mathf.Abs(zoomAmountAdded) > 0.1f || isZooming)
        {
            Debug.Log("Diference grater than: " + 1);
            zoomObjetiveTopRig = Mathf.Clamp((CMCamera.m_Orbits[0].m_Radius + zoomAmountAdded), topRigRadius.minDistance, topRigRadius.maxDistance);
            zoomObjetiveMiddleRig = Mathf.Clamp((CMCamera.m_Orbits[1].m_Radius + zoomAmountAdded), middleRigRadius.minDistance, middleRigRadius.maxDistance);
            zoomObjetiveBottomRig = Mathf.Clamp((CMCamera.m_Orbits[2].m_Radius + zoomAmountAdded), bottomRigRadius.minDistance, bottomRigRadius.maxDistance);

            CMCamera.m_Orbits[0].m_Radius = Mathf.Lerp(CMCamera.m_Orbits[0].m_Radius, zoomObjetiveTopRig, zoomSpeed);
            CMCamera.m_Orbits[1].m_Radius = Mathf.Lerp(CMCamera.m_Orbits[1].m_Radius, zoomObjetiveTopRig, zoomSpeed);
            CMCamera.m_Orbits[2].m_Radius = Mathf.Lerp(CMCamera.m_Orbits[2].m_Radius, zoomObjetiveTopRig, zoomSpeed);

            if (zoomAmountAdded > 0)
                zoomAmountAdded -= Time.deltaTime / 0.025f;
            else if(zoomAmountAdded < 0)
                zoomAmountAdded += Time.deltaTime / 0.025f;
            else
            {
                CMCamera.m_Orbits[0].m_Radius = zoomObjetiveTopRig;
                CMCamera.m_Orbits[1].m_Radius = zoomObjetiveMiddleRig;
                CMCamera.m_Orbits[2].m_Radius = zoomObjetiveBottomRig;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Performs a rotation arround the current target.
    /// </summary>
    /// <param name="delta"></param>
    public void Rotate(float delta)
    {
        CMCamera.m_XAxis.Value += (delta * rotationSensibility);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Rotate3D(float delta)
    {
        //TODO: Por lo pronto esto esta dividido, pero luego deberiamos setear los dos ejes al mismo tiempo.

        CMCamera.m_YAxis.Value -= (delta * rotationSensibility);
    }

    /// <summary>
    /// Moves the target to a new location giving the illusion of panning.
    /// </summary>
    /// <param name="newPosition"></param>
    public void Pan(Vector3 newPosition)
    {
        target.position += newPosition;
    }

    /// <summary>
    /// Makes the camera go the the desired position.
    /// </summary>
    /// <param name="newTransform"></param>
    public void PositionAndRorationTransition(Vector3 newTransform)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(Transition(newTransform, advanceSettings.transitionTime));
    }

    /// <summary>
    /// Makes the camera go the the desired position.
    /// </summary>
    /// <param name="newTransform"></param>
    public void PositionAndRorationTransition(Transform newTransform)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(Transition(newTransform, advanceSettings.transitionTime));
    }

    /// <summary>
    /// Makes the camera go the the desired position.
    /// </summary>
    /// <param name="newTransform"></param>
    public void PositionAndRorationTransition(CubeBehaviour newTransform)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(Transition(newTransform.transform, advanceSettings.transitionTime));
    }

    /// <summary>
    /// Makes the camera go the the desired position.
    /// </summary>
    /// <param name="newTransform"></param>
    public void PositionAndRorationTransition(Face selectedFace)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(Transition(selectedFace.GetFinalSpawnPosition(), advanceSettings.transitionTime));
    }

    /// <summary>
    /// Makes the camera go the the desired position.
    /// </summary>
    /// <param name="newTransform"></param>
    public void PositionAndRorationTransition(Transform newTransform, float transitionTime)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(Transition(newTransform, transitionTime));
    }

    private void ReCenterCamera(CubeBehaviour newCube)
    {
        CubeBehaviour[] cubes = LevelManager.control.GetCubesBuilded().ToArray();
        Vector3[] positions = new Vector3[cubes.Length];
        for (int i = 0; i < positions.Length; i++)
            positions[i] = cubes[i].transform.position;

        if (newCube == null || cubes.Length == 0)
        {
            transitionCoroutine = StartCoroutine(Transition(Vector3.zero, advanceSettings.transitionTime));
        }

        transitionCoroutine = StartCoroutine(Transition(MathUtils.GetGeometricCenter(positions), advanceSettings.transitionTime));
    }


    /// <summary>
    /// Makes a Linear interpolation between to positions of the cameras target.
    /// </summary>
    /// <param name="newTransform"></param>
    /// <returns></returns>
    private IEnumerator Transition(Transform newTransform, float transitionTime)
    {
        float elapsedTime = 0;

        Vector3 initialPosition = target.position;

        while (elapsedTime <= transitionTime)
        {
            target.position = Vector3.Lerp(initialPosition, newTransform.position, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.position = newTransform.position;
    }

    /// <summary>
    /// Makes a Linear interpolation between to positions of the cameras target.
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    private IEnumerator Transition(Vector3 newPosition, float transitionTime)
    {
        float elapsedTime = 0;

        Vector3 initialPosition = target.position;

        while (elapsedTime <= transitionTime)
        {
            target.position = Vector3.Lerp(initialPosition, newPosition, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.position = newPosition;
    }

    [System.Serializable]
    private class AdvanceSettings
    {
        /// <summary>
        /// Amount of time that takes to the camera to go from its current position to the desired one.
        /// </summary>
        public float transitionTime = 2;
    }

 
}


public class RigsRadius
{
    public float maxDistance;
    public float centerDistance;
    public float minDistance;
}

public class TopRigRadius : RigsRadius
{
    public TopRigRadius()
    {
        maxDistance = 145f;
        centerDistance = 45;
        minDistance = 15;
    }
}

public class MiddleRigRadius : RigsRadius
{
    public MiddleRigRadius()
    {
        maxDistance = 145f;
        centerDistance = 45;
        minDistance = 15;
    }
}

public class BottomRigRadius : RigsRadius
{
    public BottomRigRadius()
    {
        maxDistance = 145f;
        centerDistance = 45;
        minDistance = 15;
    }
}