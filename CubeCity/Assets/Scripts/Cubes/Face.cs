using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Face : MonoBehaviour, IRaySelectable
{
    public FaceTypes Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
            SetValues();
        }
    }
    [SerializeField] private FaceTypes _type;

    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
        }
    }
    [SerializeField] private bool _isSelected;

    [SerializeField] private FaceOrientationType _orientation;
    [SerializeField] private FaceData _data;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _previewCubeOffsetPosition = 1.2f;
    [SerializeField] private float _initialSpawnPositionOffset = 10f;


    private bool isCovered;

    public void Select()
    {
        IsSelected = true;
    }

    public void Unselect()
    {
        IsSelected = false;
    }

    public bool GetStatus()
    {
        return isCovered;
    }

    public Vector3 GetInitialSpawnPosition()
    {
        Vector3 direction = (_spawnPosition.position - this.transform.position).normalized;
        return _spawnPosition.position + direction * _initialSpawnPositionOffset;
    }

    public Vector3 GetFinalSpawnPosition()
    {
        return _spawnPosition.position;
    }

    public Vector3[] GetSpawnPositions()
    {
        return new Vector3[] { GetInitialSpawnPosition(), GetFinalSpawnPosition()};
    }

    public Vector3 GetPreviewCubePosition()
    {
        Vector3 direction = (_spawnPosition.position - this.transform.position).normalized;
        return  _spawnPosition.position + direction * _previewCubeOffsetPosition;
    }

    public FaceOrientationType GetOrientationType()
    {
        return _orientation;
    }

    public void SetValues()
    {
        if (LevelManager.control != null)
            _data = LevelManager.control.GetFaceData(Type);
    }

    private void OnValidate()
    {
        Type = _type;
    }
}
