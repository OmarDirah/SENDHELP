using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTrigger : MonoBehaviour
{
    // VARIABLES
    public Animator saver;

    // ONTRIGGER - ENTER
    void OnTriggerEnter(Collider other)
    {
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
    }

    // ONTRIGGER - EXIT
    void OnTriggerExit(Collider other)
    {
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

    }

    // UI APPEAR
    public void UIAppear()
    {
        saver.SetBool("IsSavePoint", true);
        Debug.Log("NOTICE! Player, you are now able to save.");
    }

    // UI DISAPPEAR
    public void UIDisappear()
    {
        saver.SetBool("IsSavePoint", false);
        Debug.Log("You have walked away from the save point.");
    }
}
