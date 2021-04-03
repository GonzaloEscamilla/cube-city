using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

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

    public Action OnCancelButtonPressed;

    public Action OnComboMade;

    public Action OnBonusMade;

    public Action<GameScenes> OnSceneLoaded;

    #region UI Events

    public Action OnStatisticsUpdate;
    public Action<int> OnStartsUpdate;          // Estos tres son para el sistema de achivements y solo deberia ser ejecutados cuando se update por completo el valor total
    public Action<int> OnCombosUpdate;          //     en los player prefs, ejecutar desde el player.
    public Action<int> OnPowerupsUpdate;        //
    public Action<int> OnMaxProsperityUpdate;   //
    public Action OnBuy;
    public Action OnAchievementRedimed;
    public Action<Vector3, Quaternion> OnCubeMovingToPosition;

    #endregion

    public void CubeMovingToPosition(Vector3 finalPosition, Quaternion rotation)
    {
        OnCubeMovingToPosition?.Invoke(finalPosition, rotation);
    }

    public void CombosUpdate(int amount)
    {
        OnCombosUpdate?.Invoke(amount);
    }

    public void AchievementRedimed()
    {
        OnAchievementRedimed?.Invoke();
    }

    public void SceneLoaded(GameScenes loadedScene)
    {
        OnSceneLoaded?.Invoke(loadedScene);
    }

    public void Buy()
    {
        OnBuy?.Invoke();
    }

    public void MaxProsperityUpdate(int amount)
    {
        OnMaxProsperityUpdate?.Invoke(amount);
    }

    public void StarsUpdate(int currentAmount)
    {
        OnStartsUpdate?.Invoke(currentAmount);
    }

    public void ComboMade()
    {
        OnComboMade?.Invoke();
        Player.Instance.AmountOfCombosMade++;
    }

    public void BonusMade()
    {
        OnBonusMade?.Invoke();
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

    public void CancelButtonPressed()
    {
        OnCancelButtonPressed?.Invoke();
    }
}
