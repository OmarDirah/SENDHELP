using System;
using UnityEngine;
using UnityEngine.Assertions;

public class PasswordTerminalKeyboard : MonoBehaviour
{
    [SerializeField] PasswordTerminalTerminal connectedToTerminal;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 1000;
        WarnIfTerminalNotConneced();
    }

    private void WarnIfTerminalNotConneced()
    {
        if (!connectedToTerminal)
        {
            Debug.LogWarning("Keyboard not connected to a terminal");
        }
    }

    private void Update()
    {
        if (connectedToTerminal)
        {
            connectedToTerminal.ReceiveFrameInput(Input.inputString);
        }
    }
}
