using UnityEngine;
using UnityEngine.UI;

public class PasswordTerminalDisplay : MonoBehaviour
{
    [SerializeField] PasswordTerminalTerminal connectedToTerminal;

    [SerializeField] int charactersWide = 50;
    [SerializeField] int charactersHigh = 17;

    Text screenText;

    private void Start()
    {
        screenText = GetComponentInChildren<Text>();
        WarnIfTerminalNotConneced();
    }

    private void WarnIfTerminalNotConneced()
    {
        if (!connectedToTerminal)
        {
            Debug.LogWarning("Display not connected to a terminal");
        }
    }

    private void Update()
    {
        if (connectedToTerminal)
        {
            screenText.text = connectedToTerminal.GetDisplayBuffer(charactersWide, charactersHigh);
        }
    }
} 