using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager control;

    private void Awake()
    {
        if (control != null)
            Destroy(control);
        control = this;
    }

    public delegate void OnFaceSelected(Face selectedFace);
    public OnFaceSelected onfaceSelected;

    public delegate void OnFaceUnselected();
    public OnFaceUnselected onFaceUnselected;

    public delegate void OnCreateButtonPressed();
    public OnCreateButtonPressed onCreateButtonPressed;

    public delegate void OnCubeBuilded(CubeBehaviour newCube);
    public OnCubeBuilded onCubeBuilded;

    public delegate void OnCubeCreated(CubeBehaviour newCube);
    public OnCubeCreated onCubeCreated;

    public Action<Vector3> OnPreviewCubeRotated;

    public void FaceSelected(Face selectedFace)
    {
        onfaceSelected?.Invoke(selectedFace);
    }
    public void FaceUnselected()
    {
        onFaceUnselected?.Invoke();
    }

    public void CreateButtonPressed()
    {
        onCreateButtonPressed?.Invoke();
    }

    public void CubeBuilded(CubeBehaviour addedCube)
    {
        onCubeBuilded?.Invoke(addedCube);
    }

    public void CubeCreated(CubeBehaviour newCreatedCube)
    {
        onCubeCreated?.Invoke(newCreatedCube);
    }

    public void PreviewCubeRotated(Vector3 axis)
    {
        OnPreviewCubeRotated?.Invoke(axis);
    }
}
