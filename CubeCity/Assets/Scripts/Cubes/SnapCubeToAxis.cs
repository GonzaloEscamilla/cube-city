using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCubeToAxis : MonoBehaviour
{
    [SerializeField] private EasingFunction.Ease type;
    private EasingFunction.Function function;

    [SerializeField] float duration = 1;
    Quaternion targetRotation = Quaternion.identity;

    private bool completeYRotation = false;
    private bool completeXRotation = false;
    private bool completeZRotation = false;
    private bool completeWRotation = false;

    public static Vector3 NearestWorldAxis(Vector3 v)
    {
        if (Mathf.Abs(v.x) < Mathf.Abs(v.y))
        {
            v.x = 0;
            if (Mathf.Abs(v.y) < Mathf.Abs(v.z))
                v.y = 0;
            else
                v.z = 0;
        }
        else
        {
            v.y = 0;
            if (Mathf.Abs(v.x) < Mathf.Abs(v.z))
                v.x = 0;
            else
                v.z = 0;
        }
        return v;
    }

    public void Align(Vector3 alignedForward, Vector3 alignedUp, Action callback)
    {
        FromToRotation(this.gameObject, this.transform.rotation, Quaternion.LookRotation(alignedForward, alignedUp), callback);
    }
    
    public void Align(Action callback)
    {
        Vector3 alignedForward = NearestWorldAxis(transform.forward);
        Vector3 alignedUp = NearestWorldAxis(transform.up);

        FromToRotation(this.gameObject, this.transform.rotation, Quaternion.LookRotation(alignedForward, alignedUp), callback);
    }

    private void LastAlign()  //<= ur callback function
    {
        /*
        if (completeYRotation)
            transform.rotation = new Quaternion(transform.rotation.x, -transform.rotation.y, transform.rotation.z, transform.rotation.w);

        if (completeXRotation)
            transform.rotation = new Quaternion(-transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        if (completeZRotation)
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -transform.rotation.z, transform.rotation.w);

        if (completeWRotation)
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, -transform.rotation.w);
        */
    }


    /// <summary>
    /// Rotates and object on a defined axis a desired amount of euler angles.
    /// </summary>
    /// <param name="objToRotate"></param>
    /// <param name="axis">Must be in the from of a vector 3 representing the axis e.g (1,0,0) for positive x axis, or (0,-1,0) for negative y axis. Other values might run in to undesired results.</param>
    /// <param name="anglesToRotate"></param>
    private void RotateObject(GameObject objToRotate, Vector3 axis, int anglesToRotate)
    {
        RotateObject(objToRotate, axis, anglesToRotate, null);
    }

    /// <summary>
    /// Rotates and object on a defined axis a desired amount of euler angles.
    /// </summary>
    /// <param name="objToRotate"></param>
    /// <param name="axis">Must be in the from of a vector 3 representing the axis e.g (1,0,0) for positive x axis, or (0,-1,0) for negative y axis. Other values might run in to undesired results.</param>
    /// <param name="anglesToRotate"></param>
    private void RotateObject(GameObject objToRotate, Vector3 axis, int anglesToRotate, Action callBack)
    {
        Quaternion initialRotation;
        initialRotation = objToRotate.transform.rotation;

        targetRotation = Quaternion.Euler(axis * anglesToRotate) * targetRotation;

        StopAllCoroutines();
        StartCoroutine(DoRotation(objToRotate, initialRotation, targetRotation, callBack));
    }

    private void FromToRotation(GameObject objToRotate, Quaternion from, Quaternion to, Action callBack)
    {
        Quaternion initialRotation;
        initialRotation = objToRotate.transform.rotation;

        if (Mathf.Sign(initialRotation.y) != Mathf.Sign(to.y))
        {
            completeYRotation = true;
            to.y = -to.y;
        }

        if (Mathf.Sign(initialRotation.x) != Mathf.Sign(to.x))
        {
            completeXRotation = true;
            to.x = -to.x;
        }

        if (Mathf.Sign(initialRotation.w) != Mathf.Sign(to.w))
        {
            completeWRotation = true;
            to.w = -to.w;
        }

        if (Mathf.Sign(initialRotation.z) != Mathf.Sign(to.z))
        {
            completeZRotation = true;
            to.z = -to.z;
        }

        targetRotation = to;

        StopAllCoroutines();
        StartCoroutine(DoRotation(objToRotate, initialRotation, targetRotation, callBack));
    }

    private IEnumerator DoRotation(GameObject objToRotate, Quaternion from, Quaternion to, Action callBack)
    {
        function = EasingFunction.GetEasingFunction(type);

        float currentTime = 0;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / duration;

            objToRotate.transform.rotation = QuaternionEasing(from, to, currentTime);

            yield return null;
        }
        objToRotate.transform.rotation = to;

        LastAlign();

        callBack?.Invoke();
    }

    private Quaternion QuaternionEasing(Quaternion from, Quaternion to, float amount)
    {
        Quaternion result = Quaternion.identity;

        result.x = function(from.x, to.x, amount);
        result.y = function(from.y, to.y, amount);
        result.z = function(from.z, to.z, amount);
        result.w = function(from.w, to.w, amount);

        return result;
    }
}
