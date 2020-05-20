using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    public Vector3 initialRotation;
    public Vector3 anglesToRotate;

    void Start()
    {
        
        Quaternion yRotation = Quaternion.AngleAxis(initialRotation.y, Vector3.up);
        Quaternion xRotation = Quaternion.AngleAxis(initialRotation.x, Vector3.right);
        Quaternion zRotation = Quaternion.AngleAxis(initialRotation.z, Vector3.forward);
        this.transform.rotation = yRotation * xRotation * zRotation;
        
    }

    void Update()
    {
        Quaternion yRotation = Quaternion.AngleAxis(anglesToRotate.y * Time.deltaTime, Vector3.up);
        Quaternion xRotation = Quaternion.AngleAxis(anglesToRotate.x * Time.deltaTime, Vector3.right);
        Quaternion zRotation = Quaternion.AngleAxis(anglesToRotate.z * Time.deltaTime, Vector3.forward);
        this.transform.rotation = yRotation * xRotation * zRotation * this.transform.rotation;

        initialRotation += anglesToRotate * Time.deltaTime;

    }
}
