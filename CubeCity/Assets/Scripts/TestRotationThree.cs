using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TestRotationThree : MonoBehaviour
{

    [SerializeField] float duration = 1; // seconds, must be >0.0f
    Quaternion targetRotation = Quaternion.identity;
    [SerializeField] private int rotationAmount = 90;
    [SerializeField] private float deltaRotation = 0;

    [ContextMenu("Rotate X Positive")]
    public void Rotate_X_Axis_Positive()
    {
        this.transform.Rotate(90, 0, 0,Space.World);
    }
    [ContextMenu("Rotate Y Positive")]
    public void Rotate_Y_Axis_Positive()
    {
        this.transform.Rotate(0, 90, 0, Space.World);
    }
    [ContextMenu("Rotate Z Positive")]
    public void Rotate_Z_Axis_Positive()
    {
        this.transform.Rotate(0, 0, 90, Space.World);
    }

    [ContextMenu("Rotation Test")]
    public void oli()
    {
        StartCoroutine(DoRotation());
    }

    IEnumerator DoRotation()
    {
        float currentTime = 0;
        Quaternion startRotation = transform.rotation;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / duration;

            deltaRotation = 90 * currentTime;
            this.transform.Rotate(0,0,deltaRotation);

            yield return null;
        }
        //transform.rotation = targetRotation;
    }
}
