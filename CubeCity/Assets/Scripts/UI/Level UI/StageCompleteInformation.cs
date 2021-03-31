using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageCompleteInformation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI scoreTextFailed;

    [SerializeField]
    private Image[] PopUpStars;

    [SerializeField]
    private Image[] PopUpStarsFailed;

    [SerializeField]
    private TextMeshProUGUI[] secondaryObjectivesText;

    [SerializeField]
    private TextMeshProUGUI[] secondaryObjectivesTextFailed;

    [SerializeField]
    private GameObject[] secondaryObjectivesCheck;

    [SerializeField]
    private GameObject[] secondaryObjectivesCheckFailed;

    private LevelStatistics levelStatistics;

    private SaveData saveData;

    private void Start() => Init();

    private void Init()
    {
        EventsManager.Instance.onLevelEndEvent += OnLevelEnd;
        levelStatistics = LevelManager.control.GetLevelStatistics();
    }

    private void OnLevelEnd(LevelEndData data)
    {
        SetEndStageInfo(data);
    }

    /// <summary>
    /// Sets score and Stars to UI
    /// </summary>
    private void SetEndStageInfo(LevelEndData endData)
    {  
        scoreText.text = levelStatistics.GetResourceAmount(ResourceTypes.Prosperity).ToString();
        scoreTextFailed.text = levelStatistics.GetResourceAmount(ResourceTypes.Prosperity).ToString();

        int starsAmount = 0;

        if (endData.success)
            starsAmount++;

        switch (endData.secondaryObjectives.Length)
        {
            case 3:
                if (endData.secondaryObjectives[0] && endData.secondaryObjectives[1])
                    starsAmount++;
                
                if(endData.secondaryObjectives[2])
                    starsAmount++;
                break;

            case 2:
                for (int i = 0; i < endData.secondaryObjectives.Length; i++)
                    if (endData.secondaryObjectives[i])
                        starsAmount++;
                break;

            case 1:
                if (endData.secondaryObjectives[0])
                    starsAmount += 2;                    
                break;
        }

        SaveInformarion(endData.levelNumber,starsAmount, levelStatistics.GetResourceAmount(ResourceTypes.Prosperity), endData.success);

        for (int i = 0; i < endData.secondaryObjectives.Length; i++)
        {
            secondaryObjectivesCheck[i].SetActive(endData.secondaryObjectives[i]);
            secondaryObjectivesCheckFailed[i].SetActive(endData.secondaryObjectives[i]);
        }

        for (int i = 0; i < LevelManager.control.GetCurrentLevel().GetSecondaryObjectivesNames().Length; i++)
        {
            secondaryObjectivesText[i].text = LevelManager.control.GetCurrentLevel().GetSecondaryObjectivesNames()[i];
            secondaryObjectivesTextFailed[i].text = LevelManager.control.GetCurrentLevel().GetSecondaryObjectivesNames()[i];
        }

        for (int i = 0; i < starsAmount; i++)
        {
            PopUpStars[i].gameObject.SetActive(true);
            PopUpStarsFailed[i].gameObject.SetActive(true);
        }
    }

    private void SaveInformarion(int levelnumber,int starsAmount, int levelscore, bool hasWon)
    {
        if (saveData.levelDatas == null)
            saveData.levelDatas = new List<levelData>();

        levelData LevelDataToSave = new levelData();
        LevelDataToSave.levelNumber = levelnumber;
        LevelDataToSave.starsAmount = starsAmount;
        LevelDataToSave.levelScore = levelscore;
        LevelDataToSave.completed = hasWon;

        levelData aux = new levelData();
        bool exists = false;
        int auxStars = 0;

        for (int i = 0; i < saveData.levelDatas.Count; i++)
        {
            if (saveData.levelDatas[i].levelNumber == levelnumber)
            {
                exists = true;
                auxStars = saveData.levelDatas[i].starsAmount;
                aux = saveData.levelDatas[i];
                break;
            }
        }

        if (exists && auxStars <= starsAmount)
        {
            saveData.levelDatas.Remove(aux);
            saveData.levelDatas.Add(LevelDataToSave);
        }
        else if (auxStars <= starsAmount)
            saveData.levelDatas.Add(LevelDataToSave);

        SaveLoadController.instance.Save();
    }

    private void OnEnable()
    {
        saveData = SaveLoadController.instance.saveData;
    }

    private void OnDisable()
    {
        EventsManager.Instance.onLevelEndEvent -= OnLevelEnd;
    }
}
