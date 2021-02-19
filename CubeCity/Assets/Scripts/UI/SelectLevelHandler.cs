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
        // TODO: Agregarlo Aca
    }

    public void LoadSelectedLevel()
    {
        _levelSystem.SetCurrentLevel(_levelToSelect);
        SceneLoaderController.Instance.LoadScene(GameScenes.Level);
    }
}
