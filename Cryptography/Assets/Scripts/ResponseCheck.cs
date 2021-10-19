using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseCheck : MonoBehaviour
{

    public TMP_InputField keyUserText;
    public TMP_InputField plainUserText;
    public TMP_InputField cipherUserText;

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI plainText;
    public TextMeshProUGUI cipherText;

    public TextMeshProUGUI stageText;
    private int levelStage;

    public TextMeshProUGUI correctText;
    public TextMeshProUGUI failureText;

    void checkKeyEntry()
    {
        Debug.Log("USER TEXT: " + keyUserText.text);
        Debug.Log("KEY TEXT: " + keyText.text);

        if (keyUserText.text.Equals(keyText.text))
        {
            FindObjectOfType<GameManager>().LevelProgress();
        }
        else
        {
            failureText.enabled = true;
        }
    }

    void checkPlaintextEntry()
    {
        Debug.Log("USER TEXT: " + plainUserText.text);
        Debug.Log("PLAIN TEXT: " + plainText.text);
        if (plainUserText.text.ToLower().Equals(plainText.text.ToLower()))
        {
            FindObjectOfType<GameManager>().LevelProgress();
        }
        else
        {
            failureText.enabled = true;
        }
    }

    void checkCiphertextEntry()
    {
        Debug.Log("USER TEXT: " + cipherUserText.text);
        Debug.Log("CIPHER TEXT: " + cipherText.text);


        if (cipherUserText.text.ToLower().Equals(cipherText.text.ToLower()))
        {
            FindObjectOfType<GameManager>().LevelProgress();
        }
        else
        {
            failureText.enabled = true;
        }
    }

    public void checkEntry()
    {
        Debug.Log("IN CHECK ENTRY");
        levelStage = FindObjectOfType<GameManager>().GetLevel();

        if (levelStage == 1)
        {
            checkKeyEntry();
        }
        else if (levelStage == 2 || levelStage == 4)
        {
            checkPlaintextEntry();
        }
        else if (levelStage == 3 || levelStage == 5)
        {
            checkCiphertextEntry();
        }
    }

}
