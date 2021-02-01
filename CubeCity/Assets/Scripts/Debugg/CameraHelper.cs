using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    public GameSettingsSO gameSettingsSO;
    public CinemachineFreeLook CMCamera;

    private void Start()
    {
        if (gameSettingsSO.EditorMode)
        {
            /*
            CMCamera.m_XAxis.m_InputAxisName = "Mouse X";
            CMCamera.m_YAxis.m_InputAxisName = "Mouse Y";
            */    
        }
    }
}
