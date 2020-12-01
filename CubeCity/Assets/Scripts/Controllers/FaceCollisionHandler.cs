using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCollisionHandler : MonoBehaviour
{
    [SerializeField] private List<Face> _affectedFaces = new List<Face>();

    private void OnEnable()
    {
        EventsManager.control.onPreviewCubeMoved += OnPreviewCubeMovedEvent;
        EventsManager.control.onCreateButtonPressed += SetCollisionStateToSceneCube;
    }

    private void OnDisable()
    {
        EventsManager.control.onPreviewCubeMoved -= OnPreviewCubeMovedEvent;
        EventsManager.control.onCreateButtonPressed -= SetCollisionStateToSceneCube;
    }

    public void HandleFaceCollision(Face firstFace, Face secondFace)
    {

        // TODO: Importante recordar de que antes de posicionar los cubos todo los cambios de estado se deben hacer solo en preview.

        float faceDistance = Vector3.Distance(firstFace.transform.position,secondFace.transform.position);

        /*
        Debug.DrawLine(firstFace.transform.position, secondFace.transform.position, Color.red, 4f);
        Debug.Log("The distance of " + firstFace.transform.root.name + " to " + secondFace.transform.parent + " is equal to " + faceDistance);
        */
        SetStateWithDistance(firstFace, faceDistance);
        SetStateWithDistance(secondFace, faceDistance);
        
        EventsManager.control.PreviewFaceCollision(firstFace);

        if (!_affectedFaces.Contains(secondFace))
        {
            _affectedFaces.Add(secondFace);
        }
    }

    private void SetStateWithDistance(Face face, float distance)
    {
        if (distance <= 0.5)
        {
            Debug.Log(face + " is overlaped.");
            face.SetFaceCollisionState(FaceCollisionState.Overlapped);
        }
        else if (distance > 0.5)
        {
            Debug.Log(face + " is colliding.");
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
    }

    private void SetCollisionStateToSceneCube()
    {
        //Debug.Log("SetCOlliison");

        foreach (Face face in _affectedFaces)
        {
            if (face.GetFaceCollisionState() == FaceCollisionState.Overlapped)
            {
                // TODO: Aca tendria que recalcularse el valor de las estadisticas.
                face.gameObject.SetActive(false);
            }
        }
    }

}
