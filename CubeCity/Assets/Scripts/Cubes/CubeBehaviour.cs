using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private Movement _movement;

    private void Awake()
    {
        if (!GetComponent<Movement>())
            Debug.LogWarning("Please add a movement script to the gameobject.");

        _movement = GetComponent<Movement>();

        EventsManager.control.onCubeCreated += CubeInnerSpawn;
    }

    /// <summary>
    /// Called after the Spawner spawns this cube, so this cube can performe individual tasks.
    /// </summary>
    public void CubeInnerSpawn(CubeBehaviour cube)
    {

    }

    /// <summary>
    /// Called after the LevelManager builds this cube, so this cube can performe individual tasks.
    /// </summary>
    public void CubeInnerBuild(CubeBehaviour cube)
    {

    }

    public void Move(Vector3[] positions, Action callBack)
    {
        _movement.StartMove(positions,callBack);
    }

    [ContextMenu("Rotate X Positive")]
    public void RotateAround_xAxisPositive()
    {
        Rotate(RotationAxis.X, true);
    }

    [ContextMenu("Rotate X Negative")]
    public void RotateAround_xAxisNegative()
    {
        Rotate(RotationAxis.X, false);
    }

    [ContextMenu("Rotate Y Positive")]
    public void RotateAround_yAxisPositive()
    {
        Rotate(RotationAxis.Y, true);
    }

    [ContextMenu("Rotate Y Negative")]
    public void RotateAround_yAxisNegative()
    {
        Rotate(RotationAxis.Y, false);
    }

    [ContextMenu("Rotate Z Positive")]
    public void RotateAround_zAxisPositive()
    {
        Rotate(RotationAxis.Z, true);
    }

    [ContextMenu("Rotate Z Negative")]
    public void RotateAround_zAxisNegative()
    {
        Rotate(RotationAxis.Z, false);
    }

    public void Rotate(RotationAxis axis, bool positiveRotation)
    {
        StartCoroutine(DoRotation(axis,90, positiveRotation));
    }

    IEnumerator DoRotation(RotationAxis axis,float angles, bool positiveRotation)
    {
        float time = 1;
        float elapsedTime = 0;
        Quaternion destinationRotation = Quaternion.identity;
        Vector3 anglesToRotate = Vector3.zero;

        switch (axis)
        {
            case RotationAxis.X:
                if (positiveRotation)
                    anglesToRotate = new Vector3(90, 0, 0);
                else
                    anglesToRotate = new Vector3(-90, 0, 0);
                break;
            case RotationAxis.Y:
                if (positiveRotation)
                    anglesToRotate = new Vector3(0, 90, 0);
                else
                    anglesToRotate = new Vector3(0, -90, 0);
                break;
            case RotationAxis.Z:
                if (positiveRotation)
                    anglesToRotate = new Vector3(0, 0, 90);
                else
                    anglesToRotate = new Vector3(0, 0, -90);
                break;
            default:
                Debug.LogError("You must assign a rotation axis!");
                break;
        }
        destinationRotation *= this.transform.rotation * Quaternion.Euler(anglesToRotate);

        Quaternion yRotation = Quaternion.identity;
        Quaternion xRotation = Quaternion.identity;
        Quaternion zRotation = Quaternion.identity;


        while (elapsedTime < time)
        {
            yRotation = Quaternion.AngleAxis(anglesToRotate.y * Time.deltaTime, Vector3.up);
            xRotation = Quaternion.AngleAxis(anglesToRotate.x * Time.deltaTime, Vector3.right);
            zRotation = Quaternion.AngleAxis(anglesToRotate.z * Time.deltaTime, Vector3.forward);

            this.transform.rotation = yRotation * xRotation * zRotation * this.transform.rotation;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //this.transform.rotation = yRotation * xRotation * zRotation;
        //this.transform.rotation = destinationRotation;
    }

}

public enum RotationAxis
{
    X,
    Y,
    Z
}
