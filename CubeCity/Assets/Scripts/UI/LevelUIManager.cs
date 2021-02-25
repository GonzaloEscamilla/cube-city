using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private LevelStatistics _levelStatistics;

    public Button _createButton;

    private void OnEnable()
    {
        EventsManager.Instance.OnStatisticsUpdate += StatisticsUpdateEvent;
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnStatisticsUpdate -= StatisticsUpdateEvent;
        }
    }

    private void Start()
    {
        _createButton.onClick.AddListener(() => CreateNewCube());
    }

    public void CreateNewCube()
    {
        EventsManager.Instance.CreateButtonPressed();
    }

    public void StatisticsUpdateEvent()
    {
    }

}