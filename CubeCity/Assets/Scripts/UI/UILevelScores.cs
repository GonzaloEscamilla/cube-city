using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelScores : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelStatistics _levelStatistics;
    [SerializeField] private Image _prosperityBar;

    [Header("Settings")]
    [SerializeField] private float prosperityBarFillSpeed = 0.5f;
    [SerializeField] private EasingFunction.Ease type;

    private EasingFunction.Function function;
    private int _levelMaxProsperity;
    private float _prosperityBarFillAmount;

    private void Start() => Init();

    private void Init()
    {
        EventsManager.Instance.OnStatisticsUpdate += OnStatisticsUpdate;
        _levelMaxProsperity = LevelManager.control.GetLevelSystem().GetCurrentLevel().GetMainObjective().GetResourceValue();
    }

    private void Shutdown()
    {
        EventsManager.Instance.OnStatisticsUpdate -= OnStatisticsUpdate;
    }

    private void OnDestroy()
    {
        Shutdown();
    }

    private void OnStatisticsUpdate()
    {
        StartCoroutine(FillingProsperity());
    }

    private IEnumerator FillingProsperity()
    {
        function = EasingFunction.GetEasingFunction(type);

        float elapsedTime = 0;
        float initialProsperityFill = _prosperityBarFillAmount;
        _prosperityBarFillAmount = Mathf.Clamp((float)_levelStatistics.GetResourceAmount(ResourceTypes.Prosperity) / (float)_levelMaxProsperity, 0, 1);

        while (elapsedTime <= prosperityBarFillSpeed)
        {
            _prosperityBar.fillAmount = function(initialProsperityFill, _prosperityBarFillAmount, elapsedTime/prosperityBarFillSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _prosperityBar.fillAmount = _prosperityBarFillAmount;
    }
}
