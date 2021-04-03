using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private StudioEventEmitter studioEventEmitter;
    [SerializeField] private SoundsDefinition soundsDefinition;
    [SerializeField] private Dictionary<string, FMOD.Studio.EventInstance> AllSounds = new Dictionary<string, FMOD.Studio.EventInstance>();
    [SerializeField] private List<string> allSound = new List<string>();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Init();
    }

    public void Init()
    {
        InstantiateAllSounds();
        //DontDestroyOnLoad(this);
        //EventsManager.control.OnLevelLoaded += context => PlaylevelSound(); 
    }

    public void PlayOneShoot(string name)
    {
        if (AllSounds.ContainsKey(name))
        {
            AllSounds[name].start();
            return;
        }

        Debug.LogWarning("The sound your al trying to play doesn´t exist.");
    }

    private void InstantiateAllSounds()
    {
        InstantiateCubeSounds();
        InstantiateUISounds();
        InstantiateBonusSounds();
        InstantiateLevelClipSounds();
        InstantiateAmbienceSounds();
    }

    private void InstantiateAmbienceSounds()
    {
        foreach (SoundsDefinition.Ambience ambienceSound in soundsDefinition.AmbienceSounds)
        {
            FMOD.Studio.EventInstance newSound = new FMOD.Studio.EventInstance();
            try
            {
                newSound = FMODUnity.RuntimeManager.CreateInstance(ambienceSound.SoundEvent);
                AllSounds.Add(ambienceSound.Name, newSound);
                allSound.Add(ambienceSound.Name);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning($"The path of {ambienceSound.Name} does not exist.");
            }
        }
    }

    private void InstantiateLevelClipSounds()
    {
        foreach (SoundsDefinition.LevelClip levelClipSound in soundsDefinition.LevelClips)
        {
            FMOD.Studio.EventInstance newSound = new FMOD.Studio.EventInstance();
            try
            {
                newSound = FMODUnity.RuntimeManager.CreateInstance(levelClipSound.SoundEvent);
                AllSounds.Add(levelClipSound.Name, newSound);
                allSound.Add(levelClipSound.Name);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning($"The path of {levelClipSound.Name} does not exist. Please add an event path to that sound");
            }
        }
    }

    private void InstantiateBonusSounds()
    {
        foreach (SoundsDefinition.OneShootBonusSound bonusSound in soundsDefinition.BonusSounds)
        {
            FMOD.Studio.EventInstance newSound = new FMOD.Studio.EventInstance();
            try
            {
                newSound = FMODUnity.RuntimeManager.CreateInstance(bonusSound.SoundEvent);
                AllSounds.Add(bonusSound.Name, newSound);
                allSound.Add(bonusSound.Name);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning($"The path of {bonusSound.Name} does not exist.");
            }
        }
    }

    private void InstantiateUISounds()
    {
        foreach (SoundsDefinition.OneShootUISound uiSound in soundsDefinition.UISounds)
        {
            FMOD.Studio.EventInstance newSound = new FMOD.Studio.EventInstance();
            try
            {
                newSound = FMODUnity.RuntimeManager.CreateInstance(uiSound.SoundEvent);
                AllSounds.Add(uiSound.Name, newSound);
                allSound.Add(uiSound.Name);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning($"The path of {uiSound.Name} does not exist.");
            }
        }
    }

    private void InstantiateCubeSounds()
    {
        foreach (SoundsDefinition.OneShootCubeSound cubeSound in soundsDefinition.CubeSounds)
        {
            FMOD.Studio.EventInstance newSound = new FMOD.Studio.EventInstance();
            try
            {
                newSound = FMODUnity.RuntimeManager.CreateInstance(cubeSound.SoundEvent);
                AllSounds.Add(cubeSound.Name, newSound);
                allSound.Add(cubeSound.Name);
            }
            catch (EventNotFoundException)
            {
                Debug.LogWarning($"The path of {cubeSound.Name} does not exist.");
            }
        }
    }

    public void SetLevelSound(SoundsDefinition.LevelClip levelClip)
    {
        studioEventEmitter.Event = levelClip.SoundEvent;
    }

    public void PlaylevelSound()
    {
        studioEventEmitter.Play();
    }

    public void SetEventParameterOneShot(string soundName, string parameterName, float value, bool lerp)
    {
        AllSounds[soundName].setParameterByName(parameterName, value, !lerp);
    }

    public void SetEventParameterMusic(string parameterName, float value, bool lerp)
    {
        studioEventEmitter.SetParameter(parameterName, value, !lerp);
    }
}
