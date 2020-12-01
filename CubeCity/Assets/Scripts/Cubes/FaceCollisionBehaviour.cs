using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCollisionBehaviour : MonoBehaviour, ISwitchState
{
    [SerializeField] Collider collisionBox;
    
    [SerializeField] bool isPreviewCube = false;

    private void Awake()
    {
        collisionBox = GetComponent<Collider>();
        DisableIfPreviewCube();
    }

    [ContextMenu("Try")]
    public void TryCollision()
    {
        collisionBox.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Face>() && this.GetComponentInParent<PreviewCube>() && other.CompareTag("FaceCollider"))
        {
            Face previewFace = GetComponentInParent<Face>();
            Face otherFace = other.GetComponentInParent<Face>();

            //Debug.Log("FaceCollisionBehaviour enter.");
            LevelManager.control.GetFaceCollidionsHandler().HandleFaceCollision(previewFace, otherFace);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Face>() && this.GetComponentInParent<PreviewCube>() && other.CompareTag("FaceCollider"))
        {
            //Debug.Log("FaceCollisionBehaviour exit.");
        }
    }

    public void Enable()
    {
        isPreviewCube = true;
        collisionBox.enabled = true;
    }

    public void Disable()
    {
        isPreviewCube = true;
        collisionBox.enabled = false;
        GetComponentInParent<Face>().SetFaceCollisionState(FaceCollisionState.None);
    }

    private void DisableIfPreviewCube()
    {
        if (GetComponentInParent<PreviewCube>())
        {
            collisionBox.enabled = false;
        }
    }

}
