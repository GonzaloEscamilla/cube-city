using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using System;

public class ButtonSwitchAudio : MonoBehaviour
{
    [SerializeField] private Button buttonOn;
    [SerializeField] private Button buttonOff;

    private bool isMuted;

    void Start()
    {
        buttonOn.onClick.AddListener(()  => Switch());
        buttonOff.onClick.AddListener(() => Switch());
    }

    private void Switch()
    {
        isMuted = !isMuted;
        SoundManager.Instance.TurnOnOffAudio(isMuted);
    }
}
