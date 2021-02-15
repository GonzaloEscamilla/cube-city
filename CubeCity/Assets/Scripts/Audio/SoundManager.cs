using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundsDefinition soundsDefinition;
    [SerializeField] private Dictionary<string, FMOD.Studio.EventInstance> AllSounds = new Dictionary<string, FMOD.Studio.EventInstance>();

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        InstantiateAllSounds();
    }

    public void PlayOneShoot(string Name)
    {
        AllSounds[Name].start();
    }

    private void InstantiateAllSounds()
    {
        foreach (SoundsDefinition.OneShootCubeSound cubeSound in soundsDefinition.CubeSounds)
        {
            FMOD.Studio.EventInstance newSound = FMODUnity.RuntimeManager.CreateInstance(cubeSound.SoundEvent);
            AllSounds.Add(cubeSound.Name, newSound);
        }

        foreach (SoundsDefinition.OneShootUISound uiSound in soundsDefinition.UISounds)
        {
            FMOD.Studio.EventInstance newSound = FMODUnity.RuntimeManager.CreateInstance(uiSound.SoundEvent);
            AllSounds.Add(uiSound.Name, newSound);
        }
    }
}
