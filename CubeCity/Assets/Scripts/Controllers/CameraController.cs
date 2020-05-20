using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    [SerializeField] private int zoomSpeed = 1;

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
    private Camera mainCamera;

    private Coroutine transitionCoroutine;

    private void Awake()
    {
        mainCamera = Camera.main;
        SetTarget();
    }

    private void Start()
    {
        EventsManager.control.onCubeBuilded += PositionAndRorationTransition;
    }

    private void OnDestroy()
    {
        EventsManager.control.onCubeBuilded -= PositionAndRorationTransition;
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
        // TODO: Tanto el zoom como el rotate deberian ser corutinas, para poder lerpear.

        CMCamera.m_Lens.FieldOfView = Mathf.Clamp(CMCamera.m_Lens.FieldOfView + (zoomValue * zoomSpeed), 15, 90);
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
    public void PositionAndRorationTransition(Transform newTransform, float transitionTime)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(Transition(newTransform, transitionTime));
    }

    /// <summary>
    /// Makes a Linear interpolation between to positions of the cameras target.
    /// </summary>
    /// <param name="newTransform"></param>
    /// <returns></returns>
    private IEnumerator Transition(Transform newTransform, float transitionTime)
    {
        float elapsedTime = 0;

        while (elapsedTime <= transitionTime)
        {
            target.position = Vector3.Lerp(target.position, newTransform.position, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.position = newTransform.position;
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
