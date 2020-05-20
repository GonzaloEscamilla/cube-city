using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestROtate : MonoBehaviour
{
    [SerializeField] float duration = 1; // seconds, must be >0.0f
    Quaternion targetRotation = Quaternion.identity;

    [ContextMenu("Rotate X Positive")]
    public void RotateAround_xAxisPositive()
    {
        //targetRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(90, 0, 0) * targetRotation;
        StopAllCoroutines();
        StartCoroutine(DoRotation());
    }

    [ContextMenu("Rotate X Negative")]
    public void RotateAround_xAxisNegative()
    {
        //targetRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(-90, 0, 0) * targetRotation;
        StopAllCoroutines();
        StartCoroutine(DoRotation());
    }

    [ContextMenu("Rotate Y Positive")]
    public void RotateAround_yAxisPositive()
    {
        //targetRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(0, 90, 0) * targetRotation;
        StopAllCoroutines();
        StartCoroutine(DoRotation());
    }

    [ContextMenu("Rotate Y Negative")]
    public void RotateAround_yAxisNegative()
    {
        //targetRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(0, -90, 0) * targetRotation;
        StopAllCoroutines();
        StartCoroutine(DoRotation());
    }

    [ContextMenu("Rotate Z Positive")]
    public void RotateAround_zAxisPositive()
    {
        //targetRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, 90) * targetRotation;
        StopAllCoroutines();
        StartCoroutine(DoRotation());
    }

    [ContextMenu("Rotate Z Negative")]
    public void RotateAround_zAxisNegative()
    {
        //targetRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, -90) * targetRotation;
        StopAllCoroutines();
        StartCoroutine(DoRotation());
    }

    // other rotate methods

    IEnumerator DoRotation()
    {
        float currentTime = 0;
        Quaternion startRotation = transform.rotation;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / duration;

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, currentTime);

            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
