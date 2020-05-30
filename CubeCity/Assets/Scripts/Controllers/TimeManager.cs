using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager control;
    private void Awake()
    {
        if (control == null)
            control = this;
        else
        {
            Destroy(control);
            control = this;
        }
    }

    /// <summary>
    /// Whaits until the condition is true. 
    /// </summary>
    /// <param name="condition">The condition that should be evaluated.</param>
    /// <returns></returns>
    public IEnumerator WaitForCondition(bool condition)
    {
        while (!condition)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Whaits until the time is pased or the condition is evaluated to true.
    /// </summary>
    /// <param name="condition">The condition that should be evaluated.</param>
    /// <returns></returns>
    public IEnumerator WaitForCondition(bool condition, float waitTime)
    {
        float elapsedTime = 0;

        while (elapsedTime <= waitTime || !condition)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
