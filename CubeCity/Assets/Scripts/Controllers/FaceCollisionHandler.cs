using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCollisionHandler : MonoBehaviour
{
    [SerializeField] private List<Face> _affectedFaces = new List<Face>();

    private void OnEnable()
    {
        EventsManager.Instance.onPreviewCubeMoved += OnPreviewCubeMovedEvent;
        EventsManager.Instance.onCubeBuilded += SetCollisionStateToSceneCube;
        EventsManager.Instance.onLevelEndEvent += OnLevelEnd;
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.onPreviewCubeMoved -= OnPreviewCubeMovedEvent;
            EventsManager.Instance.onCubeBuilded -= SetCollisionStateToSceneCube;

        }
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
            if (affectedFace.GetFaceCollisionState() == FaceCollisionState.Overlapped)
            {
                // TODO: Esto es un workarround para que solo agrege caras overlapeadas, por que no vamos a hacer eso de cambiar los graficos.
                // de todas maneras hay que revisar bien por que se agregaban solo caras conlisionadas del mundo y no del cubo que se esta por poner.
                _affectedFaces.Add(affectedFace);
            }
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
