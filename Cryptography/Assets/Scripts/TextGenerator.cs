using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    string[] possiblePhrases = new string[5]
    {
        "hello world",
        "julius caesar",
        "space robot",
        "password security",
        "send help"
    };

    public TextMeshProUGUI plainText;
    public TextMeshProUGUI cipherText;
    public TextMeshProUGUI keyText;

    public void Start()
    {
        string plaintext = possiblePhrases[(Random.Range(0, (possiblePhrases.Length)))];
        Debug.Log(plaintext);

        int shiftedAmount = Random.Range(3, 23);
        Debug.Log(shiftedAmount);

        string ciphertext = Encipher(plaintext, shiftedAmount);
        Debug.Log(ciphertext);

        FindObjectOfType<GameManager>().SetPlainText(plaintext);
        FindObjectOfType<GameManager>().SetCipherText(ciphertext);
        FindObjectOfType<GameManager>().SetKeyText(shiftedAmount);
    }

    public static char cipher(char ch, int key)
    {
        if (!char.IsLetter(ch))
        {

            return ch;
        }

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((((ch + key) - d) % 26) + d);


    }

    public static string Encipher(string input, int key)
    {
        string output = string.Empty;

        foreach (char ch in input)
            output += cipher(ch, key);

        return output;
    }

    public static string Decipher(string input, int key)
    {
        return Encipher(input, 26 - key);
    }
}
