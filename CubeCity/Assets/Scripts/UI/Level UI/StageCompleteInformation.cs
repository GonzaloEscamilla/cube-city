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
    private Sprite CompleteStarSprite;

    [SerializeField]
    private Image[] PopUpStars;

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

        SaveInformarion(endData.levelNumber,starsAmount, levelStatistics.GetResourceAmount(ResourceTypes.Prosperity));

        for (int i = 0; i < starsAmount; i++)
            PopUpStars[i].sprite = CompleteStarSprite;
    }

    private void SaveInformarion(int levelnumber,int starsAmount, int levelscore)
    {
        if (saveData.levelDatas == null)
            saveData.levelDatas = new List<levelData>();

        levelData LevelDataToSave = new levelData();
        LevelDataToSave.starsAmount = starsAmount;
        LevelDataToSave.levelScore = levelscore;

        int length = saveData.levelDatas.Count;

        saveData.levelDatas.RemoveAll(Data => Data.levelNumber == levelnumber && Data.starsAmount < starsAmount);
        
        if(length != saveData.levelDatas.Count)
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
