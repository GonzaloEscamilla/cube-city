﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// objectives to set to the pop up .
    /// </summary>
    [SerializeField]
    private string[] levelObjectivesToSet;

    public void Init(SelectLevelHandler selectLevelHandler, PopupLevelSelection popupLevelSelection)
    {
        this.levelHandler = selectLevelHandler;
        this.popUpLevelSelection = popupLevelSelection;
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
        }

        if(levelToLoad != null)
            SoundManager.Instance.SetLevelSound(levelToLoad.GetLevelClipSound());

        if (levelHandler != null)
            levelHandler.SetLevelToLoad(levelToLoad);
    }
}
