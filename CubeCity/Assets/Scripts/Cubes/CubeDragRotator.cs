using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CubeDragRotator : MonoBehaviour
{

	float rotationSpeed = 0.16f;
    public bool IsDragging;

	[SerializeField] Vector3 cubeNormal;
	private PreviewCube _previewCube;

	public enum NormalType
	{
		Forward,
		Back,
		Right,
		Left,
		Up,
		Down
	}
	[SerializeField] private NormalType _normalType;

	public Transform CurrentWorldCube;

	private void Awake()
	{
		_previewCube = GetComponent<PreviewCube>();
	}

	private void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began && _previewCube.CanRotate)
			{
				Debug.Log("Input del Touch");
				OnDragStarted();
			}
			if (touch.phase == TouchPhase.Ended && _previewCube.CanRotate)
			{
				OnDragFinished();

			}
		}

		/*
		if (Input.GetMouseButtonDown(0) && _previewCube.CanRotate)
		{
			Debug.Log("Input del mouse.");
			OnDragStarted();
		}

		if (Input.GetMouseButtonUp(0) && _previewCube.CanRotate)
		{
			OnDragFinished();
		}
		*/
		if (IsDragging && _previewCube.CanRotate)
		{
			Rotate();
		}
	}

	public void OnDragStarted()
	{
		Debug.Log("Drag Started");
		GetComponent<PreviewCube>().DisableFaceColliders();
		IsDragging = true;
		CheckNormal();
	}

	public void OnDragFinished()
	{
		
		Debug.Log("Drag Finished");
		IsDragging = false;
		if (CurrentWorldCube != null)
		{
			GetComponent<SnapCubeToAxis>().Align(Callback);

			Vector3 alignedForward = SnapCubeToAxis.NearestWorldAxis(transform.forward);
			Vector3 alignedUp = SnapCubeToAxis.NearestWorldAxis(transform.up);
			CurrentWorldCube.GetComponent<SnapCubeToAxis>().Align(alignedForward, alignedUp, OnSnapFinish);
		}
		else
		{
			Debug.LogWarning("You are trying to rotate a world cube that does not exist.");
		}
	}

	public void OnSnapFinish()
	{
		if (GetComponent<PreviewCube>())
		{
			EventsManager.control.PreviewCubeMoved(GetComponent<PreviewCube>());
		}
	}

	private void Callback()
	{
		// TODO: Aca pones lo que quieras hacer cuando termine la rotacion.
	}

	private void CheckNormal()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit))
		{
			cubeNormal = hit.normal;

			if (ApproximatelyPercentage(cubeNormal, Vector3.forward))
				_normalType = NormalType.Forward;
			if (ApproximatelyPercentage(cubeNormal, Vector3.back))
				_normalType = NormalType.Back;
			if (ApproximatelyPercentage(cubeNormal, Vector3.right))
				_normalType = NormalType.Right;
			if (ApproximatelyPercentage(cubeNormal, Vector3.left))
				_normalType = NormalType.Left;
			if (ApproximatelyPercentage(cubeNormal, Vector3.up))
				_normalType = NormalType.Up;
			if (ApproximatelyPercentage(cubeNormal, Vector3.down))
				_normalType = NormalType.Down;
		} 
	}

	public void Rotate()
	{
		float XaxisRotation = 0;
		float YaxisRotation = 0; 
		
		if (LevelManager.control.GameSettings.EditorMode)
		{
			XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed * 30;
			YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed * 30;
		}

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			XaxisRotation = touch.deltaPosition.x * rotationSpeed;
			YaxisRotation = touch.deltaPosition.y* rotationSpeed;
		}

		Vector3 rotationX, rotationY;
		rotationX = Vector3.zero;
		rotationY = Vector3.zero;

		switch (_normalType)
		{
			case NormalType.Forward:
				rotationX = Vector3.Cross(cubeNormal, -Vector3.right);
				rotationY = Vector3.Cross(cubeNormal, Vector3.up);
				break;
			case NormalType.Back:
				rotationX = Vector3.Cross(cubeNormal, Vector3.right);
				rotationY = Vector3.Cross(cubeNormal, Vector3.up);
				break;
			case NormalType.Right:
				rotationX = Vector3.Cross(cubeNormal, Vector3.forward);
				rotationY = Vector3.Cross(cubeNormal, Vector3.up);
				break;
			case NormalType.Left:
				rotationX = Vector3.Cross(cubeNormal, -Vector3.forward);
				rotationY = Vector3.Cross(cubeNormal, Vector3.up);
				break;
			case NormalType.Up:
				rotationX = Vector3.Cross(cubeNormal, Vector3.right);
				rotationY = Vector3.Cross(cubeNormal, Vector3.forward);
				break;
			case NormalType.Down:
				rotationX = Vector3.Cross(cubeNormal, -Vector3.right);
				rotationY = Vector3.Cross(cubeNormal, Vector3.forward);
				break;
			default:
				break;
		}

		transform.Rotate(rotationX * XaxisRotation, Space.World);
		transform.Rotate(rotationY * YaxisRotation, Space.World);

		
		if (CurrentWorldCube != null)
		{
			CurrentWorldCube.Rotate(rotationX * XaxisRotation, Space.World);
			CurrentWorldCube.Rotate(rotationY * YaxisRotation, Space.World);
		}
		
	}
	private bool ApproximatelyPercentage(Vector3 me, Vector3 other)
	{
		float result = Vector3.Dot(me, other);

		if (result > 0.9f && result < 1.1f)
			return true;
		else
			return false;
	}
}
