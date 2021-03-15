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

    [SerializeField] private Image _happinesPositiveBar;
    [SerializeField] private Image _happinesNegativeBar;

    [SerializeField] private Image _sustainabilityPositiveBar;
    [SerializeField] private Image _sustainabilityNegativeBar;


    [SerializeField] private Image _wealthPositiveBar;
    [SerializeField] private Image _wealthNegativeBar;

    [Header("Settings")]
    [SerializeField] private float prosperityBarFillSpeed = 0.5f;
    [SerializeField] private float secondaryBarFillSpeed = 0.5f;
    [SerializeField] private EasingFunction.Ease type;

    private EasingFunction.Function function;

    private int _levelMaxProsperity;
    private float _prosperityBarFillAmount;

    // Happines
    private int _levelMaxHappines;
    private float _happinesBarsFillAmount;

    // Sustainability
    private int _levelMaxSustainability;
    private float _sustainabilityBarsFillAmount;

    // Wealth
    private int _levelMaxWealth;
    private float _wealthBarsFillAmount;

    private void Start() => Init();

    private void Init()
    {
        EventsManager.Instance.OnStatisticsUpdate += OnStatisticsUpdate;
        _levelMaxProsperity = LevelManager.control.GetLevelSystem().GetCurrentLevel().GetMainObjective().GetObjetiveValue();

        _levelMaxHappines = LevelManager.control.GetLevelSystem().GetCurrentLevel().GetResourceAmountByType(ResourceTypes.Happiness);
        _levelMaxSustainability = LevelManager.control.GetLevelSystem().GetCurrentLevel().GetResourceAmountByType(ResourceTypes.Sustainability);
        _levelMaxWealth = LevelManager.control.GetLevelSystem().GetCurrentLevel().GetResourceAmountByType(ResourceTypes.Wealth);
        OnStatisticsUpdate();
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

        StartCoroutine(FillingSecondaryResources(_happinesPositiveBar, _happinesNegativeBar, _happinesBarsFillAmount, ResourceTypes.Happiness, _levelMaxHappines));
        StartCoroutine(FillingSecondaryResources(_sustainabilityPositiveBar, _sustainabilityNegativeBar, _sustainabilityBarsFillAmount, ResourceTypes.Sustainability, _levelMaxSustainability));
        StartCoroutine(FillingSecondaryResources(_wealthPositiveBar, _wealthNegativeBar, _wealthBarsFillAmount, ResourceTypes.Wealth, _levelMaxWealth));
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

    private IEnumerator FillingSecondaryResources()
    {
        function = EasingFunction.GetEasingFunction(type);

        float elapsedTime = 0;
        float initialHappinesFill = _happinesBarsFillAmount;

        _happinesBarsFillAmount = Mathf.Clamp(Mathf.Abs((float)_levelStatistics.GetResourceAmount(ResourceTypes.Happiness)) / (float)_levelMaxHappines, 0, 1);

        while (elapsedTime <= secondaryBarFillSpeed)
        {
            if (_levelStatistics.GetResourceAmount(ResourceTypes.Happiness) > 0)
            {
                _happinesPositiveBar.fillAmount = function(initialHappinesFill, _happinesBarsFillAmount, elapsedTime / secondaryBarFillSpeed);
                _happinesNegativeBar.fillAmount = 0;
            }
            else if(_levelStatistics.GetResourceAmount(ResourceTypes.Happiness) < 0)
            {
                _happinesNegativeBar.fillAmount = function(initialHappinesFill, _happinesBarsFillAmount, elapsedTime / secondaryBarFillSpeed);
                _happinesPositiveBar.fillAmount = 0;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    private IEnumerator FillingSecondaryResources(Image positiveFill, Image negativeFill, float currentFill, ResourceTypes resourceType, int maxValue)
    {
        function = EasingFunction.GetEasingFunction(type);

        float elapsedTime = 0;
        float initialFill = 0;

        switch (resourceType)
        {
            case ResourceTypes.Happiness:
                initialFill = _happinesBarsFillAmount;
                currentFill = Mathf.Clamp(Mathf.Abs((float)_levelStatistics.GetResourceAmount(resourceType)) / (float)maxValue, 0, 1);
                _happinesBarsFillAmount = currentFill;
                break;
            case ResourceTypes.Sustainability:
                initialFill = _sustainabilityBarsFillAmount;
                currentFill = Mathf.Clamp(Mathf.Abs((float)_levelStatistics.GetResourceAmount(resourceType)) / (float)maxValue, 0, 1);
                _sustainabilityBarsFillAmount = currentFill;
                break;
            case ResourceTypes.Wealth:
                initialFill = _wealthBarsFillAmount;
                currentFill = Mathf.Clamp(Mathf.Abs((float)_levelStatistics.GetResourceAmount(resourceType)) / (float)maxValue, 0, 1);
                _wealthBarsFillAmount = currentFill;
                break;
            default:
                break;
        }


        while (elapsedTime <= secondaryBarFillSpeed)
        {
            if (_levelStatistics.GetResourceAmount(resourceType) > 0)
            {
                positiveFill.fillAmount = function(initialFill, currentFill, elapsedTime / secondaryBarFillSpeed);
                negativeFill.fillAmount = 0;
            }
            else if (_levelStatistics.GetResourceAmount(resourceType) < 0)
            {
                negativeFill.fillAmount = function(initialFill, currentFill, elapsedTime / secondaryBarFillSpeed);
                positiveFill.fillAmount = 0;
            }
            else
            {
                negativeFill.fillAmount = 0;
                positiveFill.fillAmount = 0;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
