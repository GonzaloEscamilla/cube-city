using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private LevelStatistics _levelStatistics;

    [SerializeField] private Text txtCubeAmount;
    [SerializeField] private Text txtTime;

    public Button _createButton;

    private void OnEnable()
    {
        EventsManager.control.OnStatisticsUpdate += StatisticsUpdateEvent;
    }

    private void OnDisable()
    {
        EventsManager.control.OnStatisticsUpdate -= StatisticsUpdateEvent;
    }

    private void Start()
    {
        _createButton.onClick.AddListener(() => CreateNewCube());
    }

    public void CreateNewCube()
    {
        EventsManager.control.CreateButtonPressed();
    }

    public void StatisticsUpdateEvent()
    {
        // txtCubeAmount.text = "CurrentCubeAmount: " + _levelStatistics.CurrentCubeAmount.ToString();
        // txtTime.text = "CurrentTimeIs: " + _levelStatistics.ElapsedTime.ToString();
    }

}