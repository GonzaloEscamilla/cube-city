using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class TestEventse : MonoBehaviour
{
    public event EventHandler OnButtonPressed;
    public event EventHandler<EventArgsTest> OnSpacePress;


    int _count;

    public class EventArgsTest: EventArgs
    {
        public int count;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnSpacePress += DoSomething;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _count++;
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            OnSpacePress(this, new EventArgsTest { count = _count});
        }
    }

    public void DoSomething(object sender ,EventArgsTest eventArgs)
    {
        Debug.Log("Space Pressed: " + eventArgs.count + " times.");
    }
}
