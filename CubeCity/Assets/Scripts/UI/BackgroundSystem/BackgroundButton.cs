using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BackgroundButton : MonoBehaviour
{
    [SerializeField] private Image myImage;
    [SerializeField] private Sprite backgroundSpriteToShow;
    [SerializeField] private Material myBackgroundMaterial;

    public event Action<Sprite,Material> OnBackgroundSelected;

    public void Init()
    {
        GetComponent<Button>().onClick.AddListener(() => Selected());
        myImage.sprite = backgroundSpriteToShow;
    }

    private void Selected()
    {
        OnBackgroundSelected?.Invoke(backgroundSpriteToShow, myBackgroundMaterial);
    }
}
