using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Debugger : MonoBehaviour
{
    [SerializeField] private Text txtTargetFramerate;
    [SerializeField] private Text txtVSyncCount;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        /*
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        */
    }

    private void Update()
    {
        txtTargetFramerate.text =  "TargetFrameRate: " + Screen.currentResolution.ToString();
        txtVSyncCount.text = "VSyncCount: " + QualitySettings.vSyncCount.ToString();
    }
}
