using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private LevelStatistics _levelStatistics;
    [SerializeField] private UIBottomTools _UIBottomTools;

    [SerializeField] private Button _UIButtonQuit;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _cancel;

    private void Start()
    {
        _createButton.onClick.AddListener(() => CreateNewCube());
        _cancel.onClick.AddListener(() => CancelButtonPressed());
        _UIButtonQuit.onClick.AddListener(() => QuitLevel());
    }

    private void OnEnable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.onPreviewCubeMoved += PreviewCubeMoved;
        }
        else
        {
            Debug.LogWarning("The event manager is not instantiated yet.");
        }
    }


    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.onPreviewCubeMoved -= PreviewCubeMoved;
        }
        else
        {
            Debug.LogWarning("There is no more an Events Manger in the scene.");
        }
    }

    public void CreateNewCube()
    {
        EventsManager.Instance.CreateButtonPressed();
        _UIBottomTools.IsPreviewMode = false;
    }

    private void CancelButtonPressed()
    {
        EventsManager.Instance.CancelButtonPressed();
        _UIBottomTools.IsPreviewMode = false;
    }

    private void PreviewCubeMoved(PreviewCube previewCube)
    {
        _UIBottomTools.IsPreviewMode = true;
    }

    private void QuitLevel()
    {
        SceneLoaderController.Instance.LoadScene(GameScenes.LevelSelectionMenu);
    }

}