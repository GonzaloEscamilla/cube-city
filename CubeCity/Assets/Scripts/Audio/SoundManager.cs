using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private StudioEventEmitter levelMusicEmmiter;
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
        levelMusicEmmiter.Event = levelClip.SoundEvent;
    }

    public void PlayLevelSound()
    {
        levelMusicEmmiter.Play();
        SetEventParameterMusic(MusicParameters.Running_Music.ToString(), 1, true);
    }

    public void PlayComboSound(FaceTypes type)
    {
        PlayOneShoot(BonusSound.ComboSound.ToString());

        switch (type)
        {
            case FaceTypes.BusinessArea:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 2, true);
                break;
            case FaceTypes.CommercialArea:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 1, true);
                break;
            case FaceTypes.GarbagedumpArea:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 6, true);
                break;
            case FaceTypes.IndustrialArea:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 3, true);
                break;
            case FaceTypes.ParkArera:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 0, true);
                break;
            case FaceTypes.ResidentialArea:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 5, true);
                break;
            case FaceTypes.FarmArea:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 4, true);
                break;
            case FaceTypes.Demolished:
                SetEventParameterOneShot(BonusSound.ComboSound.ToString(), MusicParameters.Face_Type.ToString(), 6, true);
                break;
            default:
                break;
        }
    }

    public void StopLevelSound()
    {
        SetEventParameterMusic(MusicParameters.Running_Music.ToString(), 0, true);
    }

    public void SetEventParameterOneShot(string soundName, string parameterName, float value, bool lerp)
    {
        AllSounds[soundName].setParameterByName(parameterName, value, !lerp);
    }

    public void SetEventParameterMusic(string parameterName, float value, bool lerp)
    {
        levelMusicEmmiter.SetParameter(parameterName, value, !lerp);
    }
}

public enum MusicParameters
{
    Is_Menu_Active,
    Running_Music,
    Face_Type, // 0 - Park 1 - Market 2 - Business 3 - Fabric 4 - Farm 5 - House
    Fabric_Down_Town // Activar en mundo 3 nivel 5 al 13
}