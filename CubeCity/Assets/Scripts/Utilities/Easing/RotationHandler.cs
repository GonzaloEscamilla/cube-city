using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
        
    /*
    public EasingFunction.Ease type;
    private EasingFunction.Function function;
    */

    [SerializeField] GameObject cubeToRotate;
    [SerializeField] Vector3 axis;
    [SerializeField] float duration = 1; // seconds, must be >0.0f
    
    // TODO: Se puede sobrecargar el mewtodo para que reciba  un tiempo de duracion. Tambien para que reciba una funcion de easing definida.
    
    [ContextMenu("TestRotation")]
    public void RotateTest()
    {
        RotateObject(cubeToRotate,axis,90);
    }

    /// <summary>
    /// Rotates and object on a defined axis a desired amount of euler angles.
    /// </summary>
    /// <param name="objToRotate"></param>
    /// <param name="axis">Must be in the from of a vector 3 representing the axis e.g (1,0,0) for positive x axis, or (0,-1,0) for negative y axis. Other values might run in to undesired results.</param>
    /// <param name="anglesToRotate"></param>
    public void RotateObject(GameObject objToRotate, Vector3 axis, int anglesToRotate)
    {
        Quaternion initialRotation;
        Quaternion targetRotation;
        initialRotation = objToRotate.transform.rotation;
        
        targetRotation = initialRotation;
        targetRotation = Quaternion.Euler(axis * anglesToRotate) * targetRotation;

        StopAllCoroutines();
        StartCoroutine(DoRotation(objToRotate,initialRotation,targetRotation));
    }

    IEnumerator DoRotation(GameObject objToRotate, Quaternion from, Quaternion to)
    {
        float currentTime = 0;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / duration;

            objToRotate.transform.rotation = Quaternion.Slerp(from, to, currentTime);

            yield return null;
        }
        objToRotate.transform.rotation = to;
    }
}
