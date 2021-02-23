using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UILevelEnd : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation fadePanel; 
    [SerializeField] private DOTweenAnimation endLevelUI; 

    private void Start() => Init();

    private void Init()
    {
        EventsManager.control.onLevelEndEvent += OnLevelEnd;
    }

    private void OnLevelEnd(LevelEndData data)
    {
        fadePanel.DOPlayForward();
        endLevelUI.DOPlayForward();
    }
}
