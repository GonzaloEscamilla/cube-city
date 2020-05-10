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

    public Transform GetSpawnPosition()
    {
        return _spawnPosition;
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
