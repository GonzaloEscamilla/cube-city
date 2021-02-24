using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonUtility : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// The button can be preseed?
    /// </summary>
    public bool enabledButton;

    /// <summary>
    /// Is the pointer inside the button area?
    /// </summary>
    protected bool inside;

    /// <summary>
    /// Is the button being clicked?
    /// </summary>
    protected bool clicked;

    /// <summary>
    /// Its called when the button is released.
    /// </summary>
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (!enabledButton)
            return;

        if(clicked)
            PreRelease();

        clicked = false;
    }

    /// <summary>
    /// Its called when the button is pressed.
    /// </summary>
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!enabledButton)
            return;

        if(!clicked)
            PrePress();

        clicked = true;
    }

    /// <summary>
    /// Its called when the pointer leaves the button area.
    /// </summary>
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!enabledButton)
            return;

        if(inside)
            PreExit();

        inside = false;
    }

    /// <summary>
    /// Its called when the pointer enter the button area.
    /// </summary>
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!enabledButton)
            return;

        if(!inside)
            PreEnter();

        inside = true;                              
    }

    /// <summary>
    /// Se llama automáticamente cuando el botón es presionado (solo sirve para sobreescribir). 
    /// </summary>
    public void PrePress()
    {
        ButtonComponent[] buttons = GetComponents<ButtonComponent>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Press();
        }
    }

    /// <summary>
    /// Se llama automáticamente cuando el botón es soltado (solo sirve para sobreescribir).
    /// </summary>
    public void PreRelease()
    {
        ButtonComponent[] buttons = GetComponents<ButtonComponent>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Release();
        }
    }

    /// <summary>
    /// Se llama automáticamente cuando el puntero sale del boton (solo sirve para sobreescribir).
    /// </summary>
    public void PreExit()
    {
        ButtonComponent[] buttons = GetComponents<ButtonComponent>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Exit();
        }
    }

    /// <summary>
    /// Se llama automáticamente cuando el puntero entra al boton (solo sirve para sobreescribir).
    /// </summary>
    public void PreEnter()
    {
        ButtonComponent[] buttons = GetComponents<ButtonComponent>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Enter();
        }
    }
}
