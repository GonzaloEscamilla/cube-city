using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PopUpSettings : MonoBehaviour
{
    [SerializeField] private Button _buttonQuitApplication;

    private void Awake() => Init();

    private void Init()
    {
        _buttonQuitApplication.onClick.AddListener(() => QuitGame());
    }

    private void QuitGame()
    {

#if UNITY_EDITOR

        Debug.Log("Shutting Down The Game");
        EditorApplication.isPlaying = false;

#endif

        Application.Quit();
    }
}
