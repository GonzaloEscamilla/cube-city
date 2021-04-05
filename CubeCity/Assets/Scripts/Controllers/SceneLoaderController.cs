using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderController : MonoBehaviour
{
    public static SceneLoaderController Instance;

    [Header("References")]
    [SerializeField] private GameSettingsSO settings;

    [Space]

    [Header("Settings")]
    [SerializeField] private GameScenes initialScene;
    [SerializeField] private GameScenes _currentScene;

    private void Awake()
    {
        this.transform.parent = null;

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        _currentScene = initialScene;
    }

    public void Reload()
    {
        LoadScene(_currentScene);
        SoundManager.Instance.StopLevelSound();
    }

    /// <summary>
    /// Use this method to load a desired scene with a given index.
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void LoadScene(GameScenes sceneToLoad)
    {
        StartCoroutine(Loading(sceneToLoad));
    }

    private IEnumerator Loading(GameScenes _sceneToLoad)
    {
        if (_sceneToLoad == GameScenes.LevelSelectionMenu)
        {
            SoundManager.Instance.StopLevelSound();
        }

        yield return SceneManager.LoadSceneAsync((int)GameScenes.Loading, LoadSceneMode.Additive);

        yield return StartCoroutine(Unloading((int)_currentScene));

        yield return StartCoroutine(WaitTimeToNextScene());
        
        Debug.Log("Current Scene Unload Succesfully.", this);

        yield return SceneManager.LoadSceneAsync((int)_sceneToLoad, LoadSceneMode.Additive);

        Debug.Log("Next Scene Loaded.", this);

        yield return StartCoroutine(Unloading((int)GameScenes.Loading));

        _currentScene = _sceneToLoad;

        EventsManager.Instance.SceneLoaded(_sceneToLoad);
    }

    private IEnumerator Unloading(int sceneToUnload)
    {
        yield return SceneManager.UnloadSceneAsync(sceneToUnload);
    }

    private IEnumerator WaitTimeToNextScene()
    {
        yield return new WaitForSeconds(settings.loadingTime);
    }
}
