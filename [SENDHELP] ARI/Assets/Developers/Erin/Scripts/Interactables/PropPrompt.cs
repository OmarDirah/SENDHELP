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
        if (other.CompareTag("GreenhouseTerminalDialogue"))
        {
            UIAppear();
        }
        if (other.CompareTag("ServerRoomTerminalDialogue"))
        {
            UIAppear();
        }
        if (other.CompareTag("LivingQuartersTerminalDialogue"))
        {
            UIAppear();
        }
        if (other.CompareTag("ServerRoomAccessDialogue"))
        {
            UIAppear();
        }
        if (other.CompareTag("XHallAccessDialogue"))
        {
            UIAppear();
        }
        if (other.CompareTag("NavigationAccessDialogue"))
        {
            UIAppear();
        }
        if (other.CompareTag("LevelExitDialogue"))
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
        if (other.CompareTag("GreenhouseTerminalDialogue"))
        {
            UIDisappear();
        }
        if (other.CompareTag("ServerRoomTerminalDialogue"))
        {
            UIDisappear();
        }
        if (other.CompareTag("LivingQuartersTerminalDialogue"))
        {
            UIDisappear();
        }
        if (other.CompareTag("ServerRoomAccessDialogue"))
        {
            UIDisappear();
        }
        if (other.CompareTag("XHallAccessDialogue"))
        {
            UIDisappear();
        }
        if (other.CompareTag("NavigationAccessDialogue"))
        {
            UIDisappear();
        }
        if (other.CompareTag("LevelExitDialogue"))
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
