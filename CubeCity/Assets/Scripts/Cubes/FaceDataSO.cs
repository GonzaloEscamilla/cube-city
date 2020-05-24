using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FacesData", order = 1)]
public class FaceDataSO : ScriptableObject
{
    public FaceData[] faces;

    private void Awake()
    {
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].name = ((FaceTypes)i).ToString();
        }
    }
    
    public FaceData GetStats(FaceTypes type)
    {
        return faces[(int)type];
    }

}
