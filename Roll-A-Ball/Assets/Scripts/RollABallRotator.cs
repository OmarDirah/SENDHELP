using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class RollABallRotator : MonoBehaviour
{

	private char[] lowerChars = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
											'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
	private char[] upperChars = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
											'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
	private char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	private char[] specialChars = new char[] {'!', '"', '#', '$', '%', '&', '\'', '*', '+', ',', '.', '/',
												':', ';', '=', '?', '@', '\\', '^', '~', '`', '|'};


	public GameObject ItemPrefab;
	public float radius = 1;
	public char thisChar;
	public TextMeshPro itemText;

	/*	void Update()
		{
			transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
		} */
	
	void Start()
    {
		SetItemText('l');
    }

	void SetItemText(char c)
	{
		itemText.text = c.ToString();
	}

}