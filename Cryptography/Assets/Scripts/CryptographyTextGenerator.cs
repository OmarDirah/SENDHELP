using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CryptographyTextGenerator : MonoBehaviour
{
    string[] possiblePhrases = new string[5]
    {
        "hello world",
        "julius caesar",
        "space robot",
        "password security",
        "send help"
    };

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI plainText;
    public TextMeshProUGUI cipherText;

    public void Start()
    {
        Generate(FindObjectOfType<CryptographyGameManager>().GetLevel());
    }

    void Generate(int stage)
    {
        string plaintext = possiblePhrases[(Random.Range(0, (possiblePhrases.Length)))];

        int shiftedAmount;

        if (stage < 4)
        {
            shiftedAmount = Random.Range(3, 23);
        }
        else
        {
            shiftedAmount = 13;
        }

        string ciphertext = Encipher(plaintext, shiftedAmount);

        FindObjectOfType<CryptographyGameManager>().SetTexts(plaintext, ciphertext, shiftedAmount);
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
