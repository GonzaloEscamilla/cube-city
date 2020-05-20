using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public EasingFunction.Ease type;
    private EasingFunction.Function function;

    [SerializeField]
    private bool loop;

    [SerializeField]
    private bool rewindOnEnd;

    [SerializeField]
    private float elapsedTime;

    [SerializeField]
    private float movementDuration;

    [SerializeField]
    private Transform[] positions;
    private Vector3[] auxPositions;

    [Header("Clamp")]
    [SerializeField]
    private bool xAxis;

    [SerializeField]
    private bool yAxis;

    [SerializeField]
    private bool zAxis;

    [Space]

    public UnityEvent OnStart;
    public UnityEvent OnNewPosition;
    public UnityEvent OnEnd;
    public UnityAction action;

    private Coroutine _movementCoroutine;

    private void OnValidate()
    {
        auxPositions = new Vector3[positions.Length];

        for (int i = 0; i < positions.Length; i++)
            auxPositions[i] = positions[i].position;
    }

    [ContextMenu("StartMoving")]
    public void StartMove(Action callBack)
    {
        if (_movementCoroutine != null)
            StopCoroutine(_movementCoroutine);
        
        _movementCoroutine = StartCoroutine(Move(auxPositions, movementDuration, callBack));
    }

    public void StartMove(float duration, Action callBack)
    {
        if (_movementCoroutine != null)
            StopCoroutine(_movementCoroutine);

        _movementCoroutine = StartCoroutine(Move(auxPositions, duration, callBack));
    }

    public void StartMove(Vector3[] positions, Action callBack)
    {
        if (_movementCoroutine != null)
            StopCoroutine(_movementCoroutine);

        _movementCoroutine = StartCoroutine(Move(positions, movementDuration, callBack));
    }

    public void StartMove(Vector3[] positions, float duration, Action callBack)
    {
        if (_movementCoroutine != null)
            StopCoroutine(_movementCoroutine);

        _movementCoroutine = StartCoroutine(Move(positions, duration, callBack));
    }


    IEnumerator Move(Vector3[] positions, float duration, Action callBack)
    {
        action += Messange;
        OnStart.AddListener(action);

        OnStart.Invoke();

        function = EasingFunction.GetEasingFunction(type);

        elapsedTime = 0f;

        float x, y, z, distancePercentage, durationPercentage;
        float totalDistance = GetTotalDistance(positions);

        do
        {
            OnStart.Invoke();

            transform.position = positions[0];

            for (int i = 0; i < positions.Length - 1; i++)
            {
                x = positions[i].x;
                y = positions[i].y;
                z = positions[i].z;

                distancePercentage = Vector3.Distance(positions[i], positions[i + 1]) / totalDistance;
                durationPercentage = duration * distancePercentage;

                while (elapsedTime < durationPercentage)
                {
                    x = function(positions[i].x, positions[i + 1].x, (elapsedTime / durationPercentage));
                    y = function(positions[i].y, positions[i + 1].y, (elapsedTime / durationPercentage));
                    z = function(positions[i].z, positions[i + 1].z, (elapsedTime / durationPercentage));

                    elapsedTime += Time.deltaTime;

                    if (xAxis)
                        x = transform.position.x;
                    if (yAxis)
                        y = transform.position.y;
                    if (zAxis)
                        z = transform.position.z;

                    transform.position = new Vector3(x, y, z);

                    yield return null;
                }

                if (!xAxis && !yAxis && !zAxis)
                    transform.position = positions[i + 1];

                elapsedTime = 0f;

                if (i < positions.Length - 2)
                    OnNewPosition.Invoke();
            }

            if (!rewindOnEnd)
            {
                OnEnd.Invoke();
                callBack.Invoke();
            }
            else
            {
                for (int i = positions.Length - 1 ; i > 0; i--)
                {
                    x = positions[i].x;
                    y = positions[i].y;
                    z = positions[i].z;

                    distancePercentage = Vector3.Distance(positions[i], positions[i - 1]) / totalDistance;
                    durationPercentage = duration * distancePercentage;

                    while (elapsedTime < durationPercentage)
                    {
                        x = function(positions[i].x, positions[i - 1].x, (elapsedTime / durationPercentage));
                        y = function(positions[i].y, positions[i - 1].y, (elapsedTime / durationPercentage));
                        z = function(positions[i].z, positions[i - 1].z, (elapsedTime / durationPercentage));

                        elapsedTime += Time.deltaTime;

                        if (xAxis)
                            x = transform.position.x;
                        if (yAxis)
                            y = transform.position.y;
                        if (zAxis)
                            z = transform.position.z;

                        transform.position = new Vector3(x, y, z);

                        yield return null;
                    }

                    if (!xAxis && !yAxis && !zAxis)
                        transform.position = positions[i - 1];

                    elapsedTime = 0f;

                    if (i > positions.Length + 2)
                        OnNewPosition.Invoke();
                }

                OnEnd.Invoke();
                callBack.Invoke();

            }
        }
        while (loop);
    }

    public void Messange()
    {
        Mathf.Abs(5f);
    }

    public float GetTotalDistance(Vector3[] positions)
    {
        float totalDistance = 0;

        for (int i = 0; i < positions.Length - 1; i++)
            totalDistance += Vector3.Distance(positions[i], positions[i + 1]);

        return totalDistance;
    }

}