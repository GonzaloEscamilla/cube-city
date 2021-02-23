using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils 
{
    public static Vector3 GetGeometricCenter(Vector3[] objects)
    {
        Vector3 center = new Vector3(0, 0, 0);
        float count = 0;

        for (int i = 0; i < objects.Length; i++)
        {
            center += objects[i];
            count++;
        }

        Vector3 theCenter = center / count;

        return theCenter;
    }
}
