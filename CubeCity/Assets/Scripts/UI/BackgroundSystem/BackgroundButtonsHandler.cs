using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundButtonsHandler : MonoBehaviour
{
    [SerializeField] private BackgroundButton[] backgroundButtons;
    [SerializeField] private Image backgroundColoredImage;
    
    private Material currentSelectedBackground;

    private void Start()
    {
        for (int i = 0; i < backgroundButtons.Length; i++)
        {
            backgroundButtons[i].Init();
            backgroundButtons[i].OnBackgroundSelected += BackgroundSelected;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < backgroundButtons.Length; i++)
            backgroundButtons[i].OnBackgroundSelected -= BackgroundSelected;
    }

    private void BackgroundSelected(Sprite newBackgroundSprite, Material backgroundMaterial)
    {
        backgroundColoredImage.sprite = newBackgroundSprite;
        currentSelectedBackground = backgroundMaterial;

        Player.Instance.LevelSkybox = currentSelectedBackground;
    }
}
