using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "Sounds", menuName = "ScriptableObjects/Audio/Sounds", order = 1)]
public class SoundsDefinition : ScriptableObject
{
    [Space(10f)]
    [Header("One Shoots")]

    public OneShootCubeSound[] CubeSounds;
    public OneShootUISound[] UISounds;

    [Space(10f)]
    [Header("Music")]
    public bool None;

    [System.Serializable]
    public class OneShootCubeSound
    {
        [HideInInspector]
        public string Name;

        [FMODUnity.EventRef]
        public string SoundEvent = "";
    }

    [System.Serializable]
    public class OneShootUISound
    {
        [HideInInspector]
        public string Name;

        [FMODUnity.EventRef]
        public string SoundEvent = "";
    }

    private bool _isInitialized = false;

    private void OnEnable()
    {
        if (!_isInitialized)
        {
            CubeSounds = new OneShootCubeSound[System.Enum.GetNames(typeof(CubeSound)).Length];
            UISounds = new OneShootUISound[System.Enum.GetNames(typeof(UISound)).Length];

            for (int i = 0; i < CubeSounds.Length; i++)
            {
                CubeSounds[i] = new OneShootCubeSound();
                CubeSounds[i].Name = ((CubeSound) i).ToString();
            }
            
           
            for (int i = 0; i < UISounds.Length; i++)
            {
                UISounds[i] = new OneShootUISound();
                UISounds[i].Name = ((UISound)i).ToString();
            }
            

            _isInitialized = true;
        }

    }

}

