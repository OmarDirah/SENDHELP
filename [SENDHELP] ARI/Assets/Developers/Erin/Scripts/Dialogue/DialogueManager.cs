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
    public Animator conversation;
    public Queue<string> names;
    public Queue<string> sentences;
    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
    }

    // START DIALOGUE
    public void StartDialogue(Dialogue dialogue)
    {
        conversation.SetBool("IsOpen", true);
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

    // DISPLAY NEXT NAME
    public void DisplayNextName()
    {
        if (names.Count == 0)
        {
            EndDialogue();
            return;
        }

        source.PlayOneShot(clip, 7f);
        string name = names.Dequeue();
        nameText.text = name;
        Debug.Log(name);
    }

    // DISPLAY NEXT SENTENCE
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        source.PlayOneShot(clip, 7f);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence);
    }

    // TYPEWRITER AESTHETIC
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    // END DIALOGUE
    void EndDialogue()
    {
        conversation.SetBool("IsOpen", false);
        Debug.Log("End of conversation.");
    }
}
