using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupLevelSelection : MonoBehaviour
{
    /// <summary>
    /// Reference to the level name text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI levelName;

    /// <summary>
    /// Reference to the level score text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI levelScore;

    /// <summary>
    /// Reference to the level objectives texts.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI[] levelObjectives;
}
