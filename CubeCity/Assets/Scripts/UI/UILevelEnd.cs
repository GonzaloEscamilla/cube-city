using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UILevelEnd : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation fadePanel;
    
    [SerializeField] private DOTweenAnimation endLevelUI;
    [SerializeField] private DOTweenAnimation endLevelUIFailed;


    private void Start() => Init();

    private void Init()
    {
        EventsManager.Instance.onLevelEndEvent += OnLevelEnd;
    }

    private void OnDisable()
    {
        EventsManager.Instance.onLevelEndEvent -= OnLevelEnd;
    }

    private void OnLevelEnd(LevelEndData data)
    {        
        fadePanel.DOPlayForward();

        if (data.success)
            endLevelUI.DOPlayForward();
        else
            endLevelUIFailed.DOPlayForward();
    }
}
