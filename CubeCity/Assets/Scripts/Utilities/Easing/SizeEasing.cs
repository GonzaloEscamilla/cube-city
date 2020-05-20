using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeEasing : MonoBehaviour
{
    public EasingFunction.Ease type;
    private EasingFunction.Function function;

    [SerializeField]
    public float changeAmount;

    [SerializeField]
    private float durationTime;

    [SerializeField]
    private float elapsedTime;

    [SerializeField]
    private bool returnToOriginalSize;

    private float x,y,z;

    [ContextMenu("ChangeSize")]
    public void StartChangeSize()
    {
        StartCoroutine(ChangeSize(changeAmount, durationTime));
    }

    IEnumerator ChangeSize(float amount, float duration)
    {
        function = EasingFunction.GetEasingFunction(type);

        elapsedTime = 0f;

        Vector3 oldScale = transform.localScale;
        Vector3 newScale = oldScale;

        while (elapsedTime < duration)
        {
            x = function(newScale.x, amount, (elapsedTime / duration));
            y = function(newScale.y, amount, (elapsedTime / duration));
            z = function(newScale.z, amount, (elapsedTime / duration));
            Debug.Log("newScale = " + newScale.x);

            transform.localScale = new Vector3(x,y,z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        
        if (returnToOriginalSize)
        {
            elapsedTime = 0f;

            newScale = transform.localScale;

            while (elapsedTime < duration)
            {
                x = function(newScale.x, oldScale.x, elapsedTime / duration);
                y = function(newScale.y, oldScale.y, elapsedTime / duration);
                z = function(newScale.z, oldScale.z, elapsedTime / duration);

                transform.localScale = new Vector3(x, y, z);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
        }
    }

}
