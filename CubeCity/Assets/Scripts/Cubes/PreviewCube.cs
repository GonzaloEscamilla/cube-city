using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCube : CubeBehaviour, IRaySelectable
{
    [SerializeField] private Quaternion _currentRotation;

    [SerializeField] private Pool[] _graphicsPool;

    private CubeDragRotator dragRotator;

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
    [SerializeField] private bool _isSelected;

    private void Awake()
    {
        _graphicsPool = GetComponentsInChildren<Pool>();
        dragRotator = GetComponent<CubeDragRotator>();

        Debug.Log("Aca", this.gameObject);
    }

    private void OnEnable()
    {
        EventsManager.control.onfaceSelected += SetPosition;
        EventsManager.control.onCreateButtonPressed += ResetPosition;
        EventsManager.control.onFaceUnselected += OnfaceUnselected;

        EventsManager.control.onCubeCreated += HandleNewCube;
    }

   

    private void OnDestroy()
    {
        EventsManager.control.onfaceSelected -= SetPosition;
        EventsManager.control.onCreateButtonPressed -= ResetPosition;
        EventsManager.control.onFaceUnselected -= OnfaceUnselected;

        EventsManager.control.onCubeCreated -= HandleNewCube;
    }

    private void Start()
    {
        DisableFaceColliders();
    }
    
    public void HandleNewCube(CubeBehaviour newCube)
    {
        SetFacesGraphics(newCube);
        SetCubeRotation(newCube);
    }

    private void SetCubeRotation(CubeBehaviour newCube)
    {
        Debug.Log("New Rotation");
        newCube.transform.rotation = this.transform.rotation;
    }

    public void SetFacesGraphics(CubeBehaviour cube)
    {
        dragRotator.CurrentWorldCube = cube.transform;

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
        Debug.Log("SetPosition");
        DisableFaceColliders();

        this.transform.position = selectedFace.GetPreviewCubePosition();
        
        EventsManager.control.PreviewCubeMoved(this);
    }

    private void OnfaceUnselected()
    {
       
    }

    private void ResetPosition()
    {
        DisableFaceColliders();
        this.transform.position = new Vector3(10000, 10000, 1000);
        //EventsManager.control.PreviewCubeMoved(this);
    }

    /// <summary>
    /// This will disable all the FaceCollisionBehaviour colliders.
    /// </summary>
    public void DisableFaceColliders()
    {
        Debug.Log("DisableFaceColliders");
        ISwitchState[] setups = GetComponentsInChildren<ISwitchState>();

        for (int i = 0; i < setups.Length; i++)
        {
            setups[i].Disable();
        }
    }

    /// <summary>
    /// This will disable all the FaceCollisionBehaviour colliders.
    /// </summary>
    public void EnableFaceColliders()
    {
        Debug.Log("Enable Face Colliders");
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

    public void SetInputs(Vector2 deltaPosition)
    {

    }
}
