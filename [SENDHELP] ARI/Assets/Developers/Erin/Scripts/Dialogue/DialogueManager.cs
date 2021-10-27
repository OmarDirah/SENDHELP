// [NAME] Erin Alvarico
// [Dialogue Manager] Manager for Dialogue and Interaction for Story Progression

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // VARIABLES
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Queue<string> names;
    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        Debug.Log("Starting conversation with " + dialogue.names[0]);

        names.Clear();
        sentences.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextName();
        DisplayNextSentence();
    }

    public void DisplayNextName()
    {
        if (names.Count == 0)
        {
            EndDialogue();
            return;
        }

        string name = names.Dequeue();
        nameText.text = name;
        Debug.Log(name);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }


    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation.");
    }
}
