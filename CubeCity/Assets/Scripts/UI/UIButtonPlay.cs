using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIButtonPlay : MonoBehaviour
{
    [SerializeField] private GameSettingsSO settings;
    [SerializeField] private DOTweenAnimation menuAnimation;

    private void Start()
    {
        if (settings.IsInitialized())
        {
            menuAnimation.DOPlayForward();
        }
    }
}
