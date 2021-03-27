using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIBottomTools : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation cubePlacementButtons;
    [SerializeField] private DOTweenAnimation powerUpsButtons;

    public bool IsPreviewMode
    {
        get
        {
            return isPreviewmode;
        }
        set
        {
            isPreviewmode = value;
            SwitchPopUps();
        }
    }
    private bool isPreviewmode;

    [ContextMenu("Switch")]
    private void SwitchPopUps()
    {
        if (isPreviewmode)
        {
            cubePlacementButtons.DOPlayForward();
            powerUpsButtons.DOPlayForward();
            isPreviewmode = false;
            return;
        }
        cubePlacementButtons.DOPlayBackwards();
        powerUpsButtons.DOPlayBackwards();
        isPreviewmode = true;
    }
}
