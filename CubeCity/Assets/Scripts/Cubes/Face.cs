using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Face : MonoBehaviour, IRaySelectable, IPoolable
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

    [Tooltip("0: Forwards, 1: Backguards, 2: Right, 3: Left.")]
    [SerializeField] private Transform[] _detectionPoints;

    [SerializeField] private List<Face> faceColliders = new List<Face>();
    [SerializeField] private LayerMask _mask;

    [ContextMenu("Test")]
    public List<Face> GetAdjacentFaces()
    {
        return _adjacentFaces;
    }

    public void DiscoverAdjacentFaces()
    {
        for (int j = 0; j < 4; j++)
        {
            Collider[] auxColliders = Physics.OverlapSphere(_detectionPoints[j].position, 0.05f, _mask);

            for (int i = 0; i < auxColliders.Length; i++)
            {
                if (auxColliders[i].GetComponent<Face>())
                {
                    AddAdjacentFace(auxColliders[i].GetComponent<Face>());
                }
            }
        }
    }

    public List<Face> GetAdjacentGroup()
    {
        List<Face> result = new List<Face>();
        Dictionary<Face, bool> visited = new Dictionary<Face, bool>();
        Queue<Face> facesToVisit = new Queue<Face>();
        facesToVisit.Enqueue(this);
        while (facesToVisit.Count > 0)
        {
            Face currentFace = facesToVisit.Dequeue();
            bool isVisited;
            visited.TryGetValue(currentFace, out isVisited);
            if (!isVisited)
            {
                result.Add(currentFace);
                visited[currentFace] = true;

                foreach (Face adjacentFace in currentFace.GetAdjacentFaces())
                {
                    bool adjacentIsVisited;
                    visited.TryGetValue(adjacentFace, out adjacentIsVisited);
                    if (!adjacentIsVisited &&
                        adjacentFace.Type == currentFace.Type
                        && adjacentFace._level == this._level)
                    {
                        facesToVisit.Enqueue(adjacentFace);
                    }
                }
            }
        }
        return result;
    }

    [SerializeField] private bool _isSelected;

    [SerializeField] private FaceCollisionState _collisionState;
    [SerializeField] private FaceOrientationType _orientation;
    [SerializeField] private Resources _data;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _previewCubeOffsetPosition = 0f;
    [SerializeField] private float _initialSpawnPositionOffset = 10f;

    [SerializeField] private List<Face> _adjacentFaces = new List<Face>();


    private bool isCovered;

    // TODO: esta variable se debería asignar en 0 cuando la cara es creada
    [SerializeField] private int _level;

    public void OnDisable()
    {
        foreach (Face adjacentFace in GetAdjacentFaces())
        {
            adjacentFace.RemoveAdjacentFace(this);
        }
    }

    public void Select()
    {
        IsSelected = true;
    }

    public void Unselect()
    {
        IsSelected = false;
    }

    public void Upgrade()
    {
        //TODO: cambiar el gráfico, animaciones, etc
        _level++;
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

    public Resources GetFaceData()
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

    public void AddAdjacentFace(Face adjacentFace)
    {
        if (!adjacentFace._adjacentFaces.Contains(adjacentFace))
        {
            _adjacentFaces.Add(adjacentFace);
        }
    }

    public void ClearAdjacentFaces()
    {
        _adjacentFaces.Clear();
    }

    public void RemoveAdjacentFace(Face adjacentFace)
    {
        if (_adjacentFaces.Contains(adjacentFace))
        {
            _adjacentFaces.Remove(adjacentFace);
        }
    }

    [ContextMenu("Demolition")]
    public void Demolition()
    {
        LevelManager.control.GetLevelStatistics().CalculateNextResources(-GetFaceData());
        CheckAdjacenciesAffected();
        Type = FaceTypes.Demolished;
    }

    [ContextMenu("Reform")]
    public void CheckReform()
    {
        ReformFace(FaceTypes.GarbagedumpArea);
    }

    public void ReformFace(FaceTypes newType)
    {
        Demolition();
        Type = newType;
        foreach (Face adjacentFace in GetAdjacentFaces())
        {
            LevelManager.control.GetLevelStatistics().CalculateNextResources(
                LevelManager.control.GetAdjacencyBounisesSO().GetBonusForFaces(Type, adjacentFace.Type)
            );
        }
    }

    private void CheckAdjacenciesAffected()
    {
        foreach (Face adjacentFace in GetAdjacentFaces())
        {
            LevelManager.control.GetLevelStatistics().CalculateNextResources(-
                LevelManager.control.GetAdjacencyBounisesSO().GetBonusForFaces(Type, adjacentFace.Type)
            );
        }
    }

    private void OnValidate()
    {
        //Type = _type;
    }

    void IPoolable.Initialize()
    {
        _level = 0;
        ClearAdjacentFaces();
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _detectionPoints.Length; i++)
        {
            Gizmos.DrawSphere(_detectionPoints[i].position, 0.25f);
        }
    }
}
