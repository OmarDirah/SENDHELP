using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;

    public TextMeshProUGUI plainText;
    public TextMeshProUGUI cipherText;
    public TextMeshProUGUI keyText;


    public void SetPlainText(string pText)
    {
        plainText.text = "Plain Text: \n" + pText;
    }

    public void SetCipherText(string cText)
    {
        cipherText.text = "Cipher Text: \n" + cText;
    }

    public void SetKeyText(int kText)
    {
        keyText.text = "Key: \n" + kText.ToString();
    }
}
