﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class EventsManager : Singleton<EventsManager>
{
    private void Awake()
    {
        this.transform.parent = null;
        DontDestroyOnLoad(this);
    }

    public delegate void OnLevelEnd(LevelEndData data);
    public OnLevelEnd onLevelEndEvent;

    public delegate void OnFaceSelected(Face selectedFace);
    public OnFaceSelected onfaceSelected;

    public delegate void OnFaceUnselected();
    public OnFaceUnselected onFaceUnselected;

    public delegate void OnPreviewCubeMoved(PreviewCube previewCube);
    public OnPreviewCubeMoved onPreviewCubeMoved;

    public delegate void OnCreateButtonPressed();
    public OnCreateButtonPressed onCreateButtonPressed;

    public delegate void OnCubeBuilded(CubeBehaviour newCube);
    public OnCubeBuilded onCubeBuilded;

    public delegate void OnCubeCreated(CubeBehaviour newCube);
    public OnCubeCreated onCubeCreated;

    public Action<LevelsSettingsSO> OnLevelLoaded;

    public Action<Vector3> OnPreviewCubeRotated;

    public Action<Face> OnPreviewFaceCollision;

    public Action OnComboMade;

    #region UI Events

    public Action OnStatisticsUpdate;

    #endregion

    public void ComboMade()
    {
        OnComboMade?.Invoke();
    }

    public void FaceSelected(Face selectedFace)
    {
        onfaceSelected?.Invoke(selectedFace);
    }
    public void FaceUnselected()
    {
        onFaceUnselected?.Invoke();
    }

    public void CreateButtonPressed()
    {
        onCreateButtonPressed?.Invoke();
    }

    public void CubeBuilded(CubeBehaviour addedCube)
    {
        onCubeBuilded?.Invoke(addedCube);
    }

    public void CubeCreated(CubeBehaviour newCreatedCube)
    {
        onCubeCreated?.Invoke(newCreatedCube);
    }

    public void PreviewCubeRotated(Vector3 axis)
    {
        OnPreviewCubeRotated?.Invoke(axis);
    }

    public void StatisticsUpdate()
    {
        OnStatisticsUpdate?.Invoke();
    }

    public void PreviewFaceCollision(Face faces)
    {
        OnPreviewFaceCollision?.Invoke(faces);
    }

    public void PreviewCubeMoved(PreviewCube previewCube)
    {
        onPreviewCubeMoved?.Invoke(previewCube);
    }

    public void EndLevel(LevelEndData data)
    {
        onLevelEndEvent?.Invoke(data);
    }

    public void LevelLoaded(LevelsSettingsSO levelLoaded)
    {
        OnLevelLoaded?.Invoke(levelLoaded);
    }
}
