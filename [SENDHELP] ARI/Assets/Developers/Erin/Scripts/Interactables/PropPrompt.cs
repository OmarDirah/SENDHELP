// [NAME] Erin Alvarico
// [Prop Prompt] Prompts Visual Cue for Player to Press E to Interact with the Item

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPrompt : MonoBehaviour
{
    // VARIABLES
    public Animator eInteract;

    // ONTRIGGER - ENTER
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChipDialogue"))
        {
            UIAppear();
        }
        if (other.CompareTag("TerminalDialogue"))
        {
            UIAppear();
        }
    }

    // ONTRIGGER - EXIT
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ChipDialogue"))
        {
            UIDisappear();
        }
        if (other.CompareTag("TerminalDialogue"))
        {
            UIDisappear();
        }
    }

    // UI APPEAR
    public void UIAppear()
    {
        eInteract.SetBool("IsOpen", true);
        Debug.Log("New Interaction with Prop Found: ");
    }

    // UI DISAPPEAR
    public void UIDisappear()
    {
        eInteract.SetBool("IsOpen", false);
        Debug.Log("Walked Out of Range of Prop");
    }
}
