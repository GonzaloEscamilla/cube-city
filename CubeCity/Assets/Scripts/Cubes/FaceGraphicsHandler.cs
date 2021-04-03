using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGraphicsHandler : MonoBehaviour
{
    public void SwitchFaceGraphics(FaceTypes newType)
    {
        RemovePreviewFaceGraphics();
        AddNewFaceGraphics(newType);
    }

    [ContextMenu("Remove Face Graphics")]
    private void RemovePreviewFaceGraphics()
    {
        GameObject faceToRemove = GetComponentInChildren<FaceGraphics>().gameObject;
        faceToRemove.SetActive(false);
        faceToRemove.GetComponentInChildren<Setup>().ReturnToParent();
    }

    private void AddNewFaceGraphics(FaceTypes newType)
    {
        GameObject newFaceGraphics = LevelManager.control.GetCubeSpawner().GetFaceByType(newType);
        newFaceGraphics.transform.parent = this.transform;
        newFaceGraphics.transform.position = this.transform.position;
        newFaceGraphics.transform.rotation = this.transform.rotation;
    }
}
