using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCube : CubeBehaviour, IRaySelectable
{
    [SerializeField] private Quaternion _currentRotation;
    private RotationBehaviour _rotationBehaviour;
    
    [SerializeField] private Pool[] _graphicsPool;

    public bool IsSelected 
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
        }
    }
    private bool _isSelected;

    private void Awake()
    {
        _rotationBehaviour = GetComponent<RotationBehaviour>();
        _graphicsPool = GetComponentsInChildren<Pool>();
    }

    private void OnEnable()
    {
        EventsManager.control.onfaceSelected += SetPosition;
        EventsManager.control.onCreateButtonPressed += ResetPosition;
        EventsManager.control.onFaceUnselected += ResetPosition;

        EventsManager.control.onCubeCreated += SetFacesGraphics;
    }

    private void OnDestroy()
    {
        EventsManager.control.onfaceSelected -= SetPosition;
        EventsManager.control.onCreateButtonPressed -= ResetPosition;
        EventsManager.control.onFaceUnselected -= ResetPosition;

        EventsManager.control.onCubeCreated -= SetFacesGraphics;
    }

    private void Start()
    {
        DisableFaceColliders();
    }
    
    public void Rotate_X_positive()
    {
        DisableFaceColliders();
        _rotationBehaviour.RotateObject(this.gameObject, Vector3.right, 90, EnableFaceColliders);
        EventsManager.control.PreviewCubeRotated(Vector3.right);
    }

    public void Rotate_X_Negative()
    {
        DisableFaceColliders();
        _rotationBehaviour.RotateObject(this.gameObject, -Vector3.right, 90, EnableFaceColliders);
        EventsManager.control.PreviewCubeRotated(-Vector3.right);
    }

    public void Rotate_Y_positive()
    {
        DisableFaceColliders();
        _rotationBehaviour.RotateObject(this.gameObject, Vector3.up, 90, EnableFaceColliders);
        EventsManager.control.PreviewCubeRotated(Vector3.up);
    }

    public void Rotate_Y_Negative()
    {
        DisableFaceColliders();
        _rotationBehaviour.RotateObject(this.gameObject, -Vector3.up, 90, EnableFaceColliders);
        EventsManager.control.PreviewCubeRotated(-Vector3.up);
    }

    public void Rotate_Z_positive()
    {
        DisableFaceColliders();
        _rotationBehaviour.RotateObject(this.gameObject, Vector3.forward, 90, EnableFaceColliders);
        EventsManager.control.PreviewCubeRotated(Vector3.forward);
    }

    public void Rotate_Z_Negative()
    {
        DisableFaceColliders();
        _rotationBehaviour.RotateObject(this.gameObject, -Vector3.forward, 90, EnableFaceColliders);
        EventsManager.control.PreviewCubeRotated(-Vector3.forward);
    }
    
    public void SetFacesGraphics(CubeBehaviour cube)
    {
        //Debug.Log("SetFacesGraphics");

        ClearGraphics();

        Face[] previewCubeFaces = GetComponentsInChildren<Face>();
        Face[] cubeFaces = cube.GetComponentsInChildren<Face>();

        for (int i = 0; i < previewCubeFaces.Length; i++)
        {
            previewCubeFaces[i].Type = cubeFaces[i].Type;

            SetFaceGraphics(previewCubeFaces[i], (int)previewCubeFaces[i].Type);
        }

    }

    private void SetFaceGraphics(Face faces, int type)
    {
        Transform newFace;
        faces.Type = (FaceTypes)type;

        newFace = _graphicsPool[type].GetPooledObject().transform;
        newFace.SetParent(faces.transform);
        newFace.SetPositionAndRotation(faces.transform.position, faces.transform.rotation);
    }

    public void ClearGraphics()
    {
        Setup[] facesGraphics = GetComponentsInChildren<Setup>();

        //Debug.Log(facesGraphics.Length, this.gameObject);

        for (int i = 0; i < facesGraphics.Length; i++)
        {
            //Debug.Log(facesGraphics[i], this.gameObject);
            // TODO: Revisar este chorizote, quizas se puede hacer de una manera mucho mas simple. No es muy flexible.
            facesGraphics[i].transform.SetParent(_graphicsPool[((int)facesGraphics[i].GetComponentInParent<Face>().Type)].transform);
            facesGraphics[i].gameObject.SetActive(false);
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
        EventsManager.control.PreviewCubeMoved(this);

        EnableFaceColliders();
        
        this.transform.position = selectedFace.GetPreviewCubePosition();
    }

    private void ResetPosition()
    {
        EventsManager.control.PreviewCubeMoved(this);

        DisableFaceColliders();

        this.transform.position = new Vector3(1000, 1000, 1000);
    }

    /// <summary>
    /// This will disable all the FaceCollisionBehaviour colliders.
    /// </summary>
    private void DisableFaceColliders()
    {
        ISwitchState[] setups = GetComponentsInChildren<ISwitchState>();

        for (int i = 0; i < setups.Length; i++)
        {
            setups[i].Disable();
        }
    }

    /// <summary>
    /// This will disable all the FaceCollisionBehaviour colliders.
    /// </summary>
    private void EnableFaceColliders()
    {
        ISwitchState[] setups = GetComponentsInChildren<ISwitchState>();

        for (int i = 0; i < setups.Length; i++)
        {
            setups[i].Enable();
        }
    }

    public void Select()
    {
        IsSelected = true;
    }

    public void Unselect()
    {
        IsSelected = false;
    }
}
