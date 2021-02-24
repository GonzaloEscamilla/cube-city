using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelHandler : MonoBehaviour
{
    [SerializeField] private LevelsSettingsSO _levelSystem;
    [SerializeField] private Level _levelToSelect;

    public void SetLevelToLoad(Level levelToLoad)
    {
        _levelToSelect = levelToLoad;
    }

    [ContextMenu("Next Level")]
    public void LoadSelectedLevel()
    {
        _levelSystem.SetCurrentLevel(_levelToSelect);
        SceneLoaderController.Instance.LoadScene(GameScenes.Level);
    }
}
