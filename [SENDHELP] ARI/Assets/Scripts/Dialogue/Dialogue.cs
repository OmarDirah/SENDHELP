// [NAME] Erin Alvarico
// [Dialogue] Dialogue for What will be Transcribed onto the Textbox

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] names;

    [TextArea(3, 10)]
    public string[] sentences;

}
