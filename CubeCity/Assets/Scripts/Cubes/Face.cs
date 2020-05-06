using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Face : MonoBehaviour, IRaySelectable, IPoolable
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

    public void Initialize()
    {
        // TODO: revisart bien si necesita ser realmente un Ipoolable.

        throw new System.NotImplementedException();
    }

    private void OnValidate()
    {
        Type = _type;
    }
}
