using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialManager : Singleton<TutorialManager>
{
    private TutorialSO currentLevelTutorial;

    [SerializeField] private DOTweenAnimation tutorialPanel;

    [SerializeField] private TextMeshProUGUI tutorialText;

    [SerializeField] private Image tutorialImage;

    public void SetTutorials(Level levelToGetTutorial, LevelStatistics levelStatistics)
    {
        currentLevelTutorial = levelToGetTutorial.GetTutorial();
        currentLevelTutorial.Init(levelStatistics);
    }

    public void SetTutorialScreen(Sprite image, string description)
    {
        tutorialText.text = description;

        if(image != null)
            tutorialImage.sprite = image;

        tutorialPanel.DOPlayForward();
    }
}
