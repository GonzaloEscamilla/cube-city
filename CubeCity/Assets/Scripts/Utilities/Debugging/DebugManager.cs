using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pyros
{
    public class DebugManager : MonoBehaviour
    {
        public static DebugManager control;

        public Text displayText;

        private void Awake()
        {
            if (control != null) Destroy(control);

            control = this;
        }

        public void Log(String text, GameObject sender)
        {
            displayText.text += ("Log: " + text + " from: " + sender.name + ". \n");
        }
    }
}