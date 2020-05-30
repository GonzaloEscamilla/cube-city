using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCube : CubeBehaviour
{
    [SerializeField] private Quaternion _currentRotation;
    private RotationBehaviour rotationBehaviour;

    private void Start()
    {
        rotationBehaviour = GetComponent<RotationBehaviour>();

        EventsManager.control.onfaceSelected += SetPosition;
        EventsManager.control.onCreateButtonPressed += ResetPosition;
        EventsManager.control.onFaceUnselected += ClearPosition;
    }

    private void OnDestroy()
    {
        EventsManager.control.onfaceSelected -= SetPosition;
        EventsManager.control.onCreateButtonPressed -= ResetPosition;
        EventsManager.control.onFaceUnselected -= ClearPosition;
    }

    
    public void Rotate_X_positive()
    {
        rotationBehaviour.RotateObject(this.gameObject, Vector3.right, 90);
        EventsManager.control.PreviewCubeRotated(Vector3.right);
    }

    public void Rotate_X_Negative()
    {
        rotationBehaviour.RotateObject(this.gameObject, -Vector3.right, 90);
        EventsManager.control.PreviewCubeRotated(-Vector3.right);
    }

    public void Rotate_Y_positive()
    {
        rotationBehaviour.RotateObject(this.gameObject, Vector3.up, 90);
        EventsManager.control.PreviewCubeRotated(Vector3.up);
    }

    public void Rotate_Y_Negative()
    {
        rotationBehaviour.RotateObject(this.gameObject, -Vector3.up, 90);
        EventsManager.control.PreviewCubeRotated(-Vector3.up);
    }

    public void Rotate_Z_positive()
    {
        rotationBehaviour.RotateObject(this.gameObject, Vector3.forward, 90);
        EventsManager.control.PreviewCubeRotated(Vector3.forward);
    }

    public void Rotate_Z_Negative()
    {
        rotationBehaviour.RotateObject(this.gameObject, -Vector3.forward, 90);
        EventsManager.control.PreviewCubeRotated(-Vector3.forward);
    }
    
    public void ClearGraphics()
    {
        Setup[] facesGraphics = GetComponentsInChildren<Setup>();

        if (facesGraphics.Length > 0)
        {
            for (int i = 0; i < facesGraphics.Length; i++)
            {
                //Destroy(facesGraphics[i].gameObject);
                facesGraphics[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetMaterialSettings()
    {
        Renderer[] facesMesh = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < facesMesh.Length; i++)
        {
            Color color = facesMesh[i].material.GetColor("_BaseColor");
            color.a = 0.55f;
            facesMesh[i].material.SetColor("_BaseColor", color);
        }

    }

    private void SetPosition(Face selectedFace)
    {
        ISetup[] setups = GetComponentsInChildren<ISetup>();

        for (int i = 0; i < setups.Length; i++)
        {
            setups[i].Setup();
        }
        
        this.transform.position = selectedFace.GetPreviewCubePosition();
    }

    private void ResetPosition()
    {
        this.transform.position = new Vector3(1000, 1000, 1000);
    }

    private void ClearPosition()
    {
        this.transform.position = new Vector3(1000, 1000, 1000);
    }
}
