using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderController : MonoBehaviour
{
    //  TODO: POr ahora lo hago un singleton, pero despues estaria bueno utilizar el sistema de Code Monkey.
    public static SceneLoaderController control;

    private void Awake()
    {
        if (control == null)
            control = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(GameScenes sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}
