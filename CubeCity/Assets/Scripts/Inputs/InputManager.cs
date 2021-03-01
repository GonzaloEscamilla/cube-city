using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// This class is used for managing all the inputs coming from the user and distributing them for their desired implementation.
/// </summary>
[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(RaycastSelectionHandler))]
public class InputManager : MonoBehaviour
{
    #region Dependencies

    [SerializeField] private PreviewCube previewCube;

    /// <summary>
    /// The camera controller reference.
    /// </summary>
    CameraController cameraController;

    /// <summary>
    /// The RaycastSelectionHandler controller reference.
    /// </summary>
    RaycastSelectionHandler raySelector;

    [SerializeField] Canvas mainCanvas;
    GraphicRaycaster raycaster;

    #endregion

    #region Global Fields

    /// <summary>
    /// Input touch used for storing all the usefull first input values needed.
    /// </summary>
    Touch firstTouch;

    /// <summary>
    /// Input touch used for storing all the usefull second input values needed.
    /// </summary>
    Touch secondTouch;

    /// <summary>
    /// Is the UI selected. Its true if the input position is above a UI element.
    /// </summary>
    [SerializeField] private bool isUISelected = false;

    private bool isTapEnabled {
        get
        {
            return !isUISelected &&
                !LevelManager.control.HasLevelEnded() &&
                !LevelManager.control.IsCubeMoving();
        }
    }

    #endregion

    #region Input Fields

    // ---------------------TAP---------------------------|

    [Header("Tap Settings")]
    [SerializeField] private float tapTimeThreshold = 0.5f;
    private Coroutine tapVerificationCoroutine;
    private float elapsedTime;
    private int tapFlag;
    private bool isTapping;
    private bool tapStartingPointVerified;

    // -------------------Swipe---------------------------|

    private bool isSwiping = false;

    // -------------------Panning-------------------------|

    [Header("Panning Settings")]
    private Vector3 initialPanningPosition;
    private Vector3 deltaPanningPosition;

    // ---------------------Zoom--------------------------|

    [Header("Zoom Settings")]
    [SerializeField] private float minScale = 2.0F;
    [SerializeField] private float maxScale = 5.0F;
    [SerializeField] private float minimumPinchSpeed = 5.0F;
    [SerializeField] private float varianceInDistances = 5.0F;
    private float deadZone = 0.1f;
    private bool isZooming;

    private float touchDelta = 0.0F;
    private Vector2 previousDistance = new Vector2(0, 0);
    private Vector2 currentDistance = new Vector2(0, 0);
    private float speedTouch0 = 0.0F;
    private float speedTouch1 = 0.0F;
    private bool previewCubeNotSelected;

    //----------------- Debugging ------------------------|
    
    private Vector2 previewMousePosition;
    private Vector2 currentMousePosition;
    private Vector2 deltaMousePosition;

    //----------------------------------------------------|

    #endregion

    #region Unity CallBacks

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
        raySelector = GetComponent<RaycastSelectionHandler>();
        raycaster = mainCanvas.GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        if (cameraController.GetCamera() == null)
        {
            return;
        }

        UISelectedVerification();

#if UNITY_EDITOR
        if (LevelManager.control.GameSettings.EditorMode)
        {

            if (Input.GetMouseButtonDown(0))
            {
                previewCube.TapStartedOnPreviewCube = raySelector.TapStartedOnPreviewCube(cameraController.GetCamera(), Input.mousePosition);

                Tap();

                // TODO: Hacer lo mismo pero en la parte de los inputs de del touch.

                if (previewCube.CanRotate)
                    previewCube._dragRotator.OnDragStarted();
            }
            if (Input.GetMouseButton(0))
            {
                Swipe();
            }
            if (Input.GetMouseButtonUp(0))
            {
                previewCube.TapStartedOnPreviewCube = false;

                if (previewCube._dragRotator.IsDragging)
                    previewCube._dragRotator.OnDragFinished();
            }
        }
#endif
        // Checks first of all if there is a UI element being selected.
        if (Input.touchCount > 0)
        {
            int id = firstTouch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
                isUISelected = true;
            else
                isUISelected = false;

        }

        if (isUISelected)
        {
            tapFlag = 0;
            //previewCube.IsSelected = false;
            return;
        }


        if (Input.touchCount == 0 && tapFlag == 1)
        {
            TouchEnded();
            previewCube.TapStartedOnPreviewCube = false;
            if (previewCube._dragRotator.IsDragging)
                previewCube._dragRotator.OnDragFinished();

            tapStartingPointVerified = false;
        }

        if (Input.touchCount == 1)
        {
            firstTouch = Input.GetTouch(0);

            if (!tapStartingPointVerified)
            {
                previewCube.TapStartedOnPreviewCube = raySelector.TapStartedOnPreviewCube(cameraController.GetCamera(), firstTouch.position);
                tapStartingPointVerified = true;
            }

            if (previewCube.CanRotate)
                previewCube._dragRotator.OnDragStarted();

            if (firstTouch.phase == TouchPhase.Began)
            {
                StartTapVerification();
                
                tapFlag = 1;
            }
            if (firstTouch.phase == TouchPhase.Moved)
            {
                tapFlag = 0;

                isSwiping = true;
                Swipe();
            }
            if (firstTouch.phase == TouchPhase.Ended)
            {
                isSwiping = false;
                tapFlag++;
            }
        }
        if (Input.touchCount == 2)
        {
            tapFlag = 0;
            firstTouch = Input.GetTouch(0);
            secondTouch = Input.GetTouch(1);

            /*
            if (secondTouch.phase == TouchPhase.Began)
            {
                StartPanning();
            }
            if (secondTouch.phase == TouchPhase.Moved)
            {
                Pan();
            }
            */

            if (secondTouch.phase == TouchPhase.Moved && firstTouch.phase == TouchPhase.Moved)
            {
                //tapFlag = 0;
                ZoomUpdateInputs();
                ZoomVerification();

                if (isZooming)
                    Zoom();
           
            }
            if (secondTouch.phase == TouchPhase.Ended)
            {
                isZooming = false;
                ZoomStop();
            }
        }
    }

 

    private void TouchEnded()
    {
        //throw new NotImplementedException();
    }

#endregion

#region Custom Methods

    /// <summary>
    /// Saves the initiali position of the inputs.
    /// </summary>
    private void StartPanning()
    {
        initialPanningPosition = GetWorldPosition();
    }

    /// <summary>
    /// Handles the panning functionality.
    /// </summary>
    private void Pan()
    {
        deltaPanningPosition = initialPanningPosition - GetWorldPosition();
        cameraController.Pan(deltaPanningPosition);
    }

    /// <summary>
    /// Validates if the Zooming cand be done. Depending on the DeadZone.
    /// </summary>
    private void ZoomVerification()
    {
        if (Mathf.Abs(touchDelta) >= deadZone)
            isZooming = true;
    }

    /// <summary>
    /// Handles the input updates for the zooming funciotnality. Saves the distances between the two touches.
    /// </summary>
    private void ZoomUpdateInputs()
    {
        currentDistance = firstTouch.position - secondTouch.position; //current distance between finger touches
        previousDistance = ((firstTouch.position - firstTouch.deltaPosition) - (secondTouch.position - secondTouch.deltaPosition));

        touchDelta = currentDistance.magnitude - previousDistance.magnitude;
        speedTouch0 = firstTouch.deltaPosition.magnitude / firstTouch.deltaTime;
        speedTouch1 = secondTouch.deltaPosition.magnitude / secondTouch.deltaTime;
    }

    /// <summary>
    /// Call the camera to perform a zoom.
    /// </summary>
    private void Zoom()
    {
        if ((touchDelta + varianceInDistances <= 1) && (speedTouch0 > minimumPinchSpeed) && (speedTouch1 > minimumPinchSpeed))
            cameraController.Zoom(-touchDelta);
        
        if ((touchDelta + varianceInDistances > 1) && (speedTouch0 > minimumPinchSpeed) && (speedTouch1 > minimumPinchSpeed))
            cameraController.Zoom(-touchDelta);
    }

    private void ZoomStop()
    {
        cameraController.ZoomStop();
    }

    /// <summary>
    /// Gets the world position of the input relative to the camera's front angle.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetWorldPosition()
    {
        if (Input.touchCount > 1)
        {
            Ray rayFromTouch = cameraController.GetCamera().ScreenPointToRay(Input.GetTouch(1).position);
            Plane ground = new Plane(cameraController.GetCamera().transform.forward, cameraController.GetTarget().position);
            float distance;
            ground.Raycast(rayFromTouch, out distance);
            return rayFromTouch.GetPoint(distance);
        }
        return Vector3.zero;
    }

    /// <summary>
    /// Calls the camera controller so it can do a rotation.
    /// </summary>
    private void Swipe()
    {
        if (!previewCube.IsSelected)
        {
            if (LevelManager.control.GameSettings.EditorMode)
            {
                cameraController.Rotate(Input.GetAxis("Mouse X") / 25);
                cameraController.Rotate3D(Input.GetAxis("Mouse Y") / 2500);
            }
            else
            {
                cameraController.Rotate(firstTouch.deltaPosition.x / 500);
                cameraController.Rotate3D(firstTouch.deltaPosition.y / 100000);
            }
        }
    }

    /// <summary>
    /// Makes a tap.
    /// </summary>
    private void Tap()
    {
        Transform selectable;

        // UISelectedVerification();

        if (isTapEnabled)
        {
#if UNITY_EDITOR

            selectable = raySelector.Select(cameraController.GetCamera(), Input.mousePosition);
#else
            selectable = raySelector.Select(cameraController.GetCamera(), Input.GetTouch(0).position);
#endif
        }

        isTapping = false;
    }

    /// <summary>
    /// Validates if the tap was done in the specific time lapse.
    /// </summary>
    private void StartTapVerification()
    {
        if (tapVerificationCoroutine != null)
            StopCoroutine(tapVerificationCoroutine);

        tapVerificationCoroutine = StartCoroutine(TapVerification());
    }

    public void UISelectedVerification()
    {
        //TODO: Aparentemente esto solo servia para Mouse. Igualmente estaria bueno investigar sobre esta opcion.

        //Set up the new Pointer Event
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        pointerData.position = Input.mousePosition;
        this.raycaster.Raycast(pointerData, results);

        SetUISelected(results.Count > 0);
    }

    public void SetUISelected(bool isSelected)
    {
        isUISelected = isSelected;
    }

    /// <summary>
    /// Validates if the tap was done in the specific time lapse.
    /// </summary>
    IEnumerator TapVerification()
    {
        elapsedTime = 0f;

        while (elapsedTime <= tapTimeThreshold && !isSwiping)
        {
            if (tapFlag >= 2)
            {
                isTapping = true;
                Tap();
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        tapFlag = 0;
        isTapping = false;
    }

#endregion
}