using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CubeDragRotator : MonoBehaviour
{
	float rotationSpeed = 0.16f;
    private bool _isDragging;

	[SerializeField] Vector3 cubeNormal;

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

    private void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began && GetComponent<PreviewCube>().IsSelected)
			{
				OnDragStarted();
			}
			if (touch.phase == TouchPhase.Ended )
			{
				OnDragFinished();

			}
		}

		if (Input.GetMouseButtonDown(0) && GetComponent<PreviewCube>().IsSelected)
		{
			OnDragStarted();
		}

		if (Input.GetMouseButtonUp(0))
		{
			OnDragFinished();
		}

		if (_isDragging && GetComponent<PreviewCube>().IsSelected)
		{
			Rotate();
		}
	}

	public void OnDragStarted()
	{
		GetComponent<PreviewCube>().DisableFaceColliders();
		_isDragging = true;
		CheckNormal();
	}

	public void OnDragFinished()
	{
		
		_isDragging = false;
		if (CurrentWorldCube != null)
		{
			GetComponent<SnapCubeToAxis>().Align(Callback);

			Vector3 alignedForward = SnapCubeToAxis.NearestWorldAxis(transform.forward);
			Vector3 alignedUp = SnapCubeToAxis.NearestWorldAxis(transform.up);
			CurrentWorldCube.GetComponent<SnapCubeToAxis>().Align(alignedForward, alignedUp, EnableFaceColliders);
		}
		else
		{
			Debug.LogWarning("You are trying to rotate a world cube that does not exist.");
		}
	}

	public void EnableFaceColliders()
	{
		EventsManager.control.PreviewCubeMoved(GetComponent<PreviewCube>());
		GetComponent<PreviewCube>().EnableFaceColliders();
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
		float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
		float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

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
