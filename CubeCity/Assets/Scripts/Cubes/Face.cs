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

    [SerializeField] private FaceOrientationType _orientation;

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

    private bool isCovered;

    [SerializeField] private FaceStats _stats;

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

    public FaceOrientationType GetOrientationType()
    {
        return _orientation;
    }

    public void SetValues()
    {
        if (LevelManager.control != null)
            _stats = LevelManager.control.facesData.GetStats(Type);
    }

    private void OnValidate()
    {
        Type = _type;
    }
}
