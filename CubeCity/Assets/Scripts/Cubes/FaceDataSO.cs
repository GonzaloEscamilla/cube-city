using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FacesData", order = 1)]
public class FaceDataSO : ScriptableObject
{
    public FaceStats[] faces;

    private void Awake()
    {
        faces = new FaceStats[Enum.GetNames(typeof(FaceTypes)).Length];

        for (int i = 0; i < faces.Length; i++)
        {
            faces[i] = new FaceStats();
            faces[i].name = ((FaceTypes)i).ToString();
        }
    }

    public FaceStats GetStats(FaceTypes type)
    {
        return faces[(int)type];
    }

}
