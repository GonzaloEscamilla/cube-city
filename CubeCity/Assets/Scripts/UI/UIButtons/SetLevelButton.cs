using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to set the level pop up
/// </summary>
[RequireComponent(typeof(UIButtonUtility))]
public class SetLevelButton : ButtonComponent
{
    /// <summary>
    /// Reference to the level Handler
    /// </summary>
    private SelectLevelHandler levelHandler;

    /// <summary>
    /// Reference to the PopUpLevelSelection.
    /// </summary>
    private PopupLevelSelection popUpLevelSelection;

    /// <summary>
    /// Reference to the level to load.
    /// </summary>
    [SerializeField]
    public Level levelToLoad;

    [Header("Settings")]

    /// <summary>
    /// Level name to set to the pop up.
    /// </summary>
    [SerializeField]
    private string levelNameToSet;

    /// <summary>
    /// Score to set to the pop up (only numbers).
    /// </summary>
    [SerializeField]
    private string levelScoreToSet;

    [SerializeField]
    private GameObject[] unlockedButtonImages;

    [SerializeField]
    private Button nextLevelButton;

    /// <summary>
    /// objectives to set to the pop up.
    /// </summary>
    [SerializeField]
    private string[] levelObjectivesToSet;

    [SerializeField]
    private GameObject[] levelStars;

    private SaveData saveData;

    public void Init(SelectLevelHandler selectLevelHandler, PopupLevelSelection popupLevelSelection)
    {
        this.levelHandler = selectLevelHandler;
        this.popUpLevelSelection = popupLevelSelection;
        saveData = SaveLoadController.instance.saveData;
        SetLevel();
    }

    /// <summary>
    /// Sets the pop up.
    /// </summary>
    public override void Release()
    {
        if (popUpLevelSelection != null)
        {
            popUpLevelSelection.levelName.text = levelNameToSet;
            popUpLevelSelection.levelScore.text = "SCORE " + levelScoreToSet;

            for (int i = 0; i < popUpLevelSelection.levelObjectives.Length; i++)
                popUpLevelSelection.levelObjectives[i].text = levelObjectivesToSet[i];

            levelToLoad.SetSecondaryObjectivesNames(levelObjectivesToSet);
        }
        else
            Debug.LogWarning("The PopUpLevelSelection is null in this object.", gameObject);


        if(levelToLoad != null)
            SoundManager.Instance.SetLevelSound(levelToLoad.GetLevelClipSound());
        else
            Debug.LogWarning("The levelToLoad is null in this object.", gameObject);

        if (levelHandler != null)
            levelHandler.SetLevelToLoad(levelToLoad);
        else
            Debug.LogWarning("The levelHandler is null in this object.", gameObject);

    }


    public void SetLevel()
    {
        if (saveData.levelDatas == null)
            return;            

        for (int i = 0; i < saveData.levelDatas.Count; i++)
        {
            if (levelToLoad != null && saveData.levelDatas[i].levelNumber == levelToLoad.LevelNumber)
            {
                if (saveData.levelDatas[i].completed)
                {
                    if (nextLevelButton != null)
                        nextLevelButton.interactable = true;

                    for (int j = 0; j < unlockedButtonImages.Length; j++)
                        unlockedButtonImages[j].SetActive(true);
                }

                for (int j = 0; j < saveData.levelDatas[i].starsAmount; j++)
                {
                    levelStars[j].SetActive(true);
                    Player.Instance.StarsAmount++;
                }
            }
        }

        EventsManager.Instance.SceneLoaded(GameScenes.LevelSelectionMenu);
    }

}
