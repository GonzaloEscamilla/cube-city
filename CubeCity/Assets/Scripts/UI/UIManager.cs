using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button _createButton;

    private void Start()
    {
        _createButton.onClick.AddListener(() => CreateNewCube());
    }

    public void CreateNewCube()
    {
        EventsManager.control.CreateButtonPressed();
    }
}