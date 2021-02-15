using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

[CreateAssetMenu(fileName = "Sounds", menuName = "ScriptableObjects/Audio/Sounds", order = 1)]
public class SoundsDefinition : ScriptableObject
{
    [Space(10f)]
    [Header("One Shoots")]

    public List<OneShootCubeSound> CubeSounds;
    public List<OneShootUISound> UISounds;
    public List<OneShootBonusSound> BonusSounds;
    public List<LevelClip> LevelClips;

    [Space(10f)]
    [Header("Music")]
    public bool None;

    public class Sound
    {
        [HideInInspector]
        public string Name;

        [FMODUnity.EventRef]
        public string SoundEvent = "";
    }

    [System.Serializable]
    public class OneShootCubeSound : Sound { }

    [System.Serializable]
    public class OneShootUISound : Sound { }

    [System.Serializable]
    public class OneShootBonusSound : Sound { }

    [System.Serializable]
    public class LevelClip : Sound { }

    private bool _isInitialized = false;

    private void OnEnable()
    {
        if (!_isInitialized)
        {

            CubeSounds = new List<OneShootCubeSound>();
            UISounds = new List<OneShootUISound>();
            BonusSounds = new List<OneShootBonusSound>();
            LevelClips = new List<LevelClip>();

            for (int i = 0; i < System.Enum.GetNames(typeof(CubeSound)).Length; i++)
            {
                CubeSounds.Add(new OneShootCubeSound());
                CubeSounds[i].Name = ((CubeSound) i).ToString();
            }
            
            for (int i = 0; i < System.Enum.GetNames(typeof(UISound)).Length; i++)
            {
                UISounds.Add(new OneShootUISound());
                UISounds[i].Name = ((UISound)i).ToString();
            }

            for (int i = 0; i < System.Enum.GetNames(typeof(BonusSound)).Length; i++)
            {
                BonusSounds.Add(new OneShootBonusSound());
                BonusSounds[i].Name = ((BonusSound)i).ToString();
            }

            for (int i = 0; i < System.Enum.GetNames(typeof(LevelClipSound)).Length; i++)
            {
                LevelClips.Add(new LevelClip());
                LevelClips[i].Name = ((LevelClipSound)i).ToString();
            }

            _isInitialized = true;
        }

    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        RefreshCubeSounds();
        RefreshUISounds();
        RefreshBonusSounds();
        RefreshLevelClipSounds();
    }

    private void RefreshCubeSounds()
    {
        int diference = CubeSounds.Count - System.Enum.GetNames(typeof(CubeSound)).Length;

        if (diference > 0)
        {
            for (int i = 0; i < diference; i++)
            {
                CubeSounds.RemoveAt(CubeSounds.Count - 1);
            }
        }

        for (int i = CubeSounds.Count; i < System.Enum.GetNames(typeof(CubeSound)).Length; i++)
        {
            CubeSounds.Add(new OneShootCubeSound());
        }

        for (int i = 0; i < CubeSounds.Count; i++)
        {
            CubeSounds[i].Name = ((CubeSound)i).ToString();
        }
    }

    private void RefreshUISounds()
    {
        int diference = UISounds.Count - System.Enum.GetNames(typeof(UISound)).Length;

        if (diference > 0)
        {
            for (int i = 0; i < diference; i++)
            {
                CubeSounds.RemoveAt(UISounds.Count - 1);
            }
        }

        for (int i = UISounds.Count; i < System.Enum.GetNames(typeof(UISound)).Length; i++)
        {
            UISounds.Add(new OneShootUISound());
        }

        for (int i = 0; i < UISounds.Count; i++)
        {
            UISounds[i].Name = ((UISound)i).ToString();
        }
    }

    private void RefreshBonusSounds()
    {
        int diference = BonusSounds.Count - System.Enum.GetNames(typeof(BonusSound)).Length;

        if (diference > 0)
        {
            for (int i = 0; i < diference; i++)
            {
                BonusSounds.RemoveAt(BonusSounds.Count - 1);
            }
        }

        for (int i = BonusSounds.Count; i < System.Enum.GetNames(typeof(BonusSound)).Length; i++)
        {
            BonusSounds.Add(new OneShootBonusSound());
        }

        for (int i = 0; i < BonusSounds.Count; i++)
        {
            BonusSounds[i].Name = ((BonusSound)i).ToString();
        }
    }

    private void RefreshLevelClipSounds()
    {
        int diference = LevelClips.Count - System.Enum.GetNames(typeof(LevelClipSound)).Length;

        if (diference > 0)
        {
            for (int i = 0; i < diference; i++)
            {
                LevelClips.RemoveAt(LevelClips.Count - 1);
            }
        }

        for (int i = LevelClips.Count; i < System.Enum.GetNames(typeof(LevelClipSound)).Length; i++)
        {
            LevelClips.Add(new LevelClip());
        }

        for (int i = 0; i < LevelClips.Count; i++)
        {
            LevelClips[i].Name = ((LevelClipSound)i).ToString();
        }
    }
}

