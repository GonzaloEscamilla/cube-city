using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TestEmmiter : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string PlayerStateEvent = "";

    [SerializeField] StudioEventEmitter _emmiter;

    [ContextMenu("Test")]
    public void Test()
    {
        _emmiter.Play();
    }
}
