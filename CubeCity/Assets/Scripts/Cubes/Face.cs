using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Face : MonoBehaviour, IRaySelectable
{
    [SerializeField] FaceDataSO _faceData;

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

    [SerializeField] private FaceCollisionState _collisionState;
    [SerializeField] private FaceOrientationType _orientation;
    [SerializeField] private FaceData _data;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _previewCubeOffsetPosition = 0f;
    [SerializeField] private float _initialSpawnPositionOffset = 10f;

    [SerializeField] private List<Face> _adjacencies = new List<Face>();


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

    public FaceData GetFaceData()
    {
        return _data;
    }

    public void SetValues()
    {
        _data = _faceData.GetStats(Type);
    }

    public void SetFaceCollisionState(FaceCollisionState newState)
    {
        _collisionState = newState;
    }

    public FaceCollisionState GetFaceCollisionState()
    {
        return _collisionState;
    }

    public void SetNewAdjacencie(Face newAdjacencie)
    {
        if (!_adjacencies.Contains(newAdjacencie))
        {
            _adjacencies.Add(newAdjacencie);
        }
    }

    public void ClearAllAdjacencies()
    {
        _adjacencies.Clear();
    }

    public void RemoveAdjacencie(Face adjacencieToRemove)
    {
        if (_adjacencies.Contains(adjacencieToRemove))
        {
            _adjacencies.Remove(adjacencieToRemove);
        }
    }

    private void OnValidate()
    {
        Type = _type;
    }
}
