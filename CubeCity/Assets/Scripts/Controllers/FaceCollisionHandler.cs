using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCollisionHandler : MonoBehaviour
{
    [SerializeField] private List<Face> _affectedFaces = new List<Face>();

    private void OnEnable()
    {
        EventsManager.control.onPreviewCubeMoved += OnPreviewCubeMovedEvent;
        EventsManager.control.onCubeBuilded += SetCollisionStateToSceneCube;
        EventsManager.control.onLevelEndEvent += OnLevelEnd;
    }

    private void OnDisable()
    {
        EventsManager.control.onPreviewCubeMoved -= OnPreviewCubeMovedEvent;
        EventsManager.control.onCubeBuilded -= SetCollisionStateToSceneCube;
    }

    private void OnLevelEnd(LevelEndData data)
    {
        //this.gameObject.SetActive(false);
        OnDisable();
    }

    public List<Face> GetAffectedFaces()
    {
        return _affectedFaces;
    }

    public void HandleFaceCollision(Face firstFace, Face secondFace)
    {
        float faceDistance = Vector3.Distance(firstFace.transform.position,secondFace.transform.position);

        SetStateWithDistance(firstFace, faceDistance);
        SetStateWithDistance(secondFace, faceDistance);
        
        ManageCurrentCubeState(firstFace);

        AddAffectedFaceToList(secondFace);
    }

    private void AddAffectedFaceToList(Face affectedFace)
    {
        if (!_affectedFaces.Contains(affectedFace))
        {
            _affectedFaces.Add(affectedFace);
        }
    }

    private void SetStateWithDistance(Face face, float distance)
    {
        if (distance <= 0.5)
        {
            face.SetFaceCollisionState(FaceCollisionState.Overlapped);
        }
        else if (distance > 0.5)
        {
            face.SetFaceCollisionState(FaceCollisionState.Colliding);
        }
    }

    private void OnPreviewCubeMovedEvent(PreviewCube previewCube)
    {
        foreach (Face face in _affectedFaces)
        {
            face.SetFaceCollisionState(FaceCollisionState.None);
        }
        _affectedFaces.Clear();

        previewCube.DisableFaceColliders();
        previewCube.EnableFaceColliders();
    }

    private void ManageCurrentCubeState(Face face)
    {
        CubeBehaviour currentSpawnedCube = LevelManager.control.GetCubeSpawner().GetCurrentCube();

        Face[] auxFaces = currentSpawnedCube.GetFaces();

        for (int i = 0; i < auxFaces.Length; i++)
        {
            if (face.GetOrientationType() == auxFaces[i].GetOrientationType())
            {
                auxFaces[i].SetFaceCollisionState(face.GetFaceCollisionState());
                if (auxFaces[i].GetFaceCollisionState() == FaceCollisionState.Overlapped)
                {
                    AddAffectedFaceToList(auxFaces[i]);
                }
            }
        }
    }

    private void SetCollisionStateToSceneCube(CubeBehaviour cube)
    {
        foreach (Face face in _affectedFaces)
        {
            if (face.GetFaceCollisionState() == FaceCollisionState.Overlapped)
            {
                face.gameObject.SetActive(false);
            }
        }
        _affectedFaces.Clear();
    }

    
}
