using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCube : CubeBehaviour
{
    [SerializeField] private Quaternion _currentRotation;

    private void Start()
    {
        EventsManager.control.onfaceSelected += SetPosition;
        EventsManager.control.onFaceUnselected += ClearPosition;
    }

    private void OnDestroy()
    {
        EventsManager.control.onfaceSelected -= SetPosition;
        EventsManager.control.onFaceUnselected -= ClearPosition;
    }

    
    public void Rotate()
    {

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
        this.transform.position = selectedFace.GetPreviewCubePosition();
    }

    private void ClearPosition()
    {
        this.transform.position = new Vector3(1000, 1000, 1000);
    }
}
