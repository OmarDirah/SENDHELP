using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CryptographyGameManager : MonoBehaviour
{
    // VARIABLES
    public string SceneToLoad;
    public Animator anim;
    public int delayTime;
    
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI plainText;
    public TextMeshProUGUI cipherText;

    public GameObject keyEntryObject;
    public GameObject plainEntryObject;
    public GameObject cipherEntryObject;

    public TMP_InputField keyUserText;
    public TMP_InputField plainUserText;
    public TMP_InputField cipherUserText;

    public TextMeshProUGUI stageText;
    private int levelStage = 1;

    public TextMeshProUGUI correctText;
    public TextMeshProUGUI failureText;


    void RegenerateTexts()
    {
        FindObjectOfType<CryptographyTextGenerator>().Start();
    }

    public void SetTexts(string pText, string cText, int kText)
    {
        plainText.text = pText;
        cipherText.text = cText;
        keyText.text = kText.ToString();

        SetStage();
    }

    public void SetStage()
    {

        correctText.enabled = false;
        failureText.enabled = false;

        if (levelStage == 1)
        {
            keyEntryObject.SetActive(true);
            keyText.enabled = false;
            
            plainEntryObject.SetActive(false);
            plainText.enabled = true;

            cipherEntryObject.SetActive(false);
            cipherText.enabled = true;
        }
        else if (levelStage == 2)
        {

            keyEntryObject.SetActive(false);
            keyText.enabled = true;

            plainEntryObject.SetActive(true);
            plainText.enabled = false;

            cipherEntryObject.SetActive(false);
            cipherText.enabled = true;
        }
        else if (levelStage == 3)
        {

            keyEntryObject.SetActive(false);
            keyText.enabled = true;

            plainEntryObject.SetActive(false);
            plainText.enabled = true;

            cipherEntryObject.SetActive(true);
            cipherText.enabled = false;
        }
        else if (levelStage == 4)
        {
            keyEntryObject.SetActive(false);
            keyText.enabled = true;
            keyText.text = "13";

            plainUserText.text = "";
            plainEntryObject.SetActive(true);
            plainText.enabled = false;

            cipherEntryObject.SetActive(false);
            cipherText.enabled = true;
        }
        else if (levelStage == 5)
        {
            keyEntryObject.SetActive(false);
            keyText.enabled = true;
            keyText.text = "13";

            plainEntryObject.SetActive(false);
            plainText.enabled = true;

            cipherUserText.text = "";
            cipherEntryObject.SetActive(true);
            cipherText.enabled = false;
        }

        if ((0 < levelStage) && (levelStage < 4))
        {
            stageText.text = "Stage: " + levelStage.ToString() + "\nCaesar Ciphers";
        }
        else if (levelStage <= 5)
        {
            stageText.text = "Stage: " + levelStage.ToString() + "\nROT13 Ciphers";
        }
        else if (levelStage > 5)
        {
	    Debug.Log("TIME ELAPSED: " + Time.time);
            
            stageText.text = "YOU WIN";
	            
	    keyEntryObject.SetActive(false);
            keyText.enabled = false;

            plainEntryObject.SetActive(false);
            plainText.enabled = false;

            cipherEntryObject.SetActive(false);
            cipherText.enabled = false;
            // WIN CONDITION!

            anim.SetBool("MinigameWon", true);
            Invoke("DelayedAction", delayTime);
            return;
        }

        Debug.Log("Stage: " + levelStage);
        Debug.Log("Key: " + keyText.text.ToLower());
        Debug.Log("Plaintext: " + plainText.text.ToLower());
        Debug.Log("Ciphertext: " + cipherText.text.ToLower());

    }

    public void LevelProgress()
    {
        failureText.enabled = false;
        correctText.enabled = true;
        levelStage += 1;
        StartCoroutine(ExampleCoroutine());       
    }


    void checkKeyEntry()
    {
        if (keyUserText.text.ToLower().Equals(keyText.text.ToLower()))
        {
            LevelProgress();
        }
        else
        {
            failureText.enabled = true;
        }
    }

    void checkPlaintextEntry()
    {
        if (plainUserText.text.ToLower().Equals(plainText.text.ToLower()))
        {
            LevelProgress();
        }
        else
        {
            failureText.enabled = true;
        }
    }

    void checkCiphertextEntry()
    {
        if (cipherUserText.text.ToLower().Equals(cipherText.text.ToLower()))
        {
            LevelProgress();
        }
        else
        {
            failureText.enabled = true;
        }
    }

    public int GetLevel()
    {
        return levelStage;
    }

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }


    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);
        RegenerateTexts();
    }

}
