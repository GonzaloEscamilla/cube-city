using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    [SerializeField] private EasingFunction.Ease type;
    private EasingFunction.Function function;

    [SerializeField] float duration = 1; // seconds, must be >0.0f
    Quaternion targetRotation = Quaternion.identity;
    
    // TODO: Se puede sobrecargar el mewtodo para que reciba  un tiempo de duracion. Tambien para que reciba una funcion de easing definida.

    /// <summary>
    /// Rotates and object on a defined axis a desired amount of euler angles.
    /// </summary>
    /// <param name="objToRotate"></param>
    /// <param name="axis">Must be in the from of a vector 3 representing the axis e.g (1,0,0) for positive x axis, or (0,-1,0) for negative y axis. Other values might run in to undesired results.</param>
    /// <param name="anglesToRotate"></param>
    public void RotateObject(GameObject objToRotate, Vector3 axis, int anglesToRotate)
    {
        Quaternion initialRotation;
        initialRotation = objToRotate.transform.rotation;
        
        targetRotation = Quaternion.Euler(axis * anglesToRotate) * targetRotation;

        StopAllCoroutines();
        StartCoroutine(DoRotation(objToRotate,initialRotation,targetRotation));
    }

    IEnumerator DoRotation(GameObject objToRotate, Quaternion from, Quaternion to)
    {
        function = EasingFunction.GetEasingFunction(type);

        float currentTime = 0;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / duration;

            //objToRotate.transform.rotation = Quaternion.Slerp(from, to, currentTime);
            objToRotate.transform.rotation = QuaternionEasing(from, to, currentTime);

            yield return null;
        }
        objToRotate.transform.rotation = to;
    }

    private Quaternion QuaternionEasing(Quaternion from, Quaternion to, float amount)
    {
        Quaternion result = Quaternion.identity;

        float x, y, z, w;
        float a, b, c, d;

        x = from.x;
        y = from.y;
        z = from.z;
        w = from.w;

        a = to.x;
        b = to.y;
        c = to.z;
        d = to.w;

        x = function(x, a, amount);
        y = function(y, b, amount);
        z = function(z, c, amount);
        w = function(w, d, amount);

        result = new Quaternion(x, y, z, w);
        return result;
    }

}
