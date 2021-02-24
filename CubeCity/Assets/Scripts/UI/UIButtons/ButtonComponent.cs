using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    /// <summary>
    /// Is called when the button gets pressed (override only). 
    /// </summary>
    public virtual void Press()
    {

    }

    /// <summary>
    /// Is called when the button gets released (override only). 
    /// </summary>
    public virtual void Release()
    {

    }

    /// <summary>
    /// Is called when the button gets inside a button (override only). 
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// Is called when the button gets out of a button (override only). 
    /// </summary>
    public virtual void Exit()
    {

    }


    /// <summary>
    /// Is called when the the state of a button gets changed.
    /// </summary>
    /// <param name="eneable"> True: the button is enable - False: the button is disabled</param>
    public virtual void SetEneable(bool eneable)
    {

    }
}
