using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonReset : MonoBehaviour
{
    private void Start() => GetComponent<Button>().onClick.AddListener(() => SceneLoaderController.Instance.Reload());
}
