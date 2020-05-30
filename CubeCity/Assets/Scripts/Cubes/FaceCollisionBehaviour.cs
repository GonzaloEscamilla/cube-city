using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCollisionBehaviour : MonoBehaviour, ISetup
{
    [SerializeField] Collider collisionBox;
    
    [SerializeField] bool isPreviewCube = false;
    float _distance = 0;

    private void Awake()
    {
        collisionBox = GetComponent<Collider>();
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
            _distance = Vector3.Distance(this.transform.position, other.transform.position);

            Debug.DrawLine(this.transform.position, other.transform.position, Color.red, 4f);

            Debug.Log("Colliding with: " + other.gameObject.name + " center distance = " + _distance);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (isPreviewCube)
        {
            return;
        }

        if (other.GetComponentInParent<Face>() && other.CompareTag("FaceCollider"))
        {
            _distance = Vector3.Distance(this.transform.position, other.transform.position);

            Debug.DrawLine(this.transform.position, other.transform.position, Color.red);

            Debug.Log("Colliding with: " + other.gameObject.name + " center distance = " + _distance);
        }
    }

    public void Setup()
    {
        if (GetComponentInParent<PreviewCube>())
        {
            isPreviewCube = true;
            collisionBox.enabled = false;
        }
    }

}
