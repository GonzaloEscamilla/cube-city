using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Debugger : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
        
    }
}
