public class PasswordTerminalInputBuffer
{
    string currentInputLine;

    public delegate void OnCommandSentHandler(string command);
    public event OnCommandSentHandler onCommandSent;

    public void ReceiveFrameInput(string input)
    {
        foreach (char c in input)
        {
            UpdateCurrentInputLine(c);
        }
    }

    public string GetCurrentInputLine()
    {
        return currentInputLine;
    }

    private void UpdateCurrentInputLine(char c)
    {
        if (c == '\b')
        {
            DeleteCharacters();
        }
        else if (c == '\n' || c == '\r')
        {
            SendCommand(currentInputLine); 
        }
        else
        {
            currentInputLine += c;
        }
    }

    private void DeleteCharacters()
    {
        if (currentInputLine.Length > 0)
        {
            currentInputLine = currentInputLine.Remove(currentInputLine.Length - 1);
        }
    }

    private void SendCommand(string command)
    {
        onCommandSent(command);
        currentInputLine = "";
    } 
}
