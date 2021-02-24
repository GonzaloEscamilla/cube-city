using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class used to set the level pop up
/// </summary>
[RequireComponent(typeof(UIButtonUtility))]
public class SetLevelButton : ButtonComponent
{
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

    /// <summary>
    /// Reference to the level name text.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI levelName;

    /// <summary>
    /// Reference to the level score text.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI levelScore;

    /// <summary>
    /// Reference to the level objectives texts.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI[] levelObjectives;

    /// <summary>
    /// Reference to the level Handler
    /// </summary>
    [SerializeField]
    private SelectLevelHandler levelHandler;

    /// <summary>
    /// Reference to the level to load.
    /// </summary>
    [SerializeField]
    private Level levelToLoad;

    /// <summary>
    /// Sets the pop up.
    /// </summary>
    public override void Release()
    {
        levelName.text = levelNameToSet;
        levelScore.text = "SCORE " + levelScoreToSet;

        for (int i = 0; i < levelObjectives.Length; i++)
            levelObjectives[i].text = levelObjectivesToSet[i];

        levelHandler.SetLevelToLoad(levelToLoad);
    }
}
