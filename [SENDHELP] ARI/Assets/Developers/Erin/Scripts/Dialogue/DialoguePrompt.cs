// [NAME] Erin Alvarico
// [Dialogue Prompt] Handles Interaction UI for Story/Diagloeu Progression in Level via Triggers

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePrompt : MonoBehaviour
{

    // VARIABLES - Currently inefficient way to store animators, will find another way to reference
    public Animator prompt1;
    public Animator prompt2;

    private int num, convoNum;

    // ONTRIGGER - ENTER
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartUpDialogue"))
        {
            num = 1;
            UIAppear(num);
        }
        if (other.CompareTag("WindowDialogue"))
        {
            num = 2;
            UIAppear(num);
        }
    }

    // ONTRIGGER - EXIT
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StartUpDialogue"))
        {
            num = 1;
            UIDisappear(num);
        }
        if (other.CompareTag("WindowDialogue"))
        {
            num = 2;
            UIDisappear(num);
        }
    }

    // UI APPEAR
    public void UIAppear(int num)
    {
        convoNum = num;
        if (convoNum == 1)
        {
            prompt1.SetBool("IsPrompted", true);
            Debug.Log("New Dialogue/Interaction Found: ");
        }
        if (convoNum == 2)
        {
            prompt2.SetBool("IsPrompted", true);
            Debug.Log("New Dialogue/Interaction Found: ");
        }
    }

    // UI DISAPPEAR
    public void UIDisappear(int num)
    {
        convoNum = num;
        if (convoNum == 1)
        {
            prompt1.SetBool("IsPrompted", false);
            Debug.Log("Walked Out of Range of Dialogue/Interaction");
        }
        if (convoNum == 2)
        {
            prompt2.SetBool("IsPrompted", false);
            Debug.Log("Walked Out of Range of Dialogue/Interaction");
        }
            
    }

}
