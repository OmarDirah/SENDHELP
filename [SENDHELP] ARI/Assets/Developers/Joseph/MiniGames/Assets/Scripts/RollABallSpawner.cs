using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RollABallSpawner : MonoBehaviour
{
    public int bouncersToSpawn;
	public int charsToSpawn;
    public List<GameObject> spawnPool;
    public GameObject quad;



	public GameObject ItemPrefab;

	private char[] lowerChars = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
											'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
	private char[] upperChars = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
											'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
	private char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	private char[] specialChars = new char[] {'!', '"', '#', '$', '%', '&', '\'', '*', '+', ',', '.', '/',
												':', ';', '=', '?', '@', '\\', '^', '~', '`', '|'};

	private float percentOfLetters = .7f;
	private float percentOfOther = .2f;

	private float lowerCount;
	private float upperCount;
	private int lowerCharCount;
	private int upperCharCount;
	private int digitCount;
	private int specialCharCount;

	public TextMeshPro itemText;

	void Start()
    {
		CharCreation();

		bouncersToSpawn = 8;

		SpawnObjects();
    }

	void CharCreation()
    {
		int charGoal = Random.Range(10, 20);

		lowerCount = Random.Range(.3f, (percentOfLetters - .2f));
		upperCount = percentOfLetters - lowerCount;

		lowerCharCount = (int)Mathf.Ceil(lowerCount * charGoal);
		upperCharCount = (int)Mathf.Ceil(upperCount * charGoal);
		digitCount = (int)Mathf.Ceil(charGoal * (percentOfOther / 2));
		specialCharCount = (int)Mathf.Ceil(charGoal * (percentOfOther / 2));

		charsToSpawn = lowerCharCount + upperCharCount + digitCount + specialCharCount;
	}

	public void SpawnObjects()
	{
		GameObject toSpawn;
		MeshCollider c = quad.GetComponent<MeshCollider>();

		float screenX, bouncerY, screenZ;
		Vector3 pos;

		for (int i = 0; i < bouncersToSpawn; i++)
		{
			toSpawn = spawnPool[0];

			bouncerY = Random.Range(1f, 4f);

			screenX = Random.Range(c.bounds.min.x + .5f, c.bounds.max.x - .5f);
			screenZ = Random.Range(c.bounds.min.z + .3f, c.bounds.max.z - 1.5f);

			pos = new Vector3(screenX, bouncerY, screenZ);

			Instantiate(toSpawn, pos, toSpawn.transform.rotation);
		}

		for (int j = 0; j < charsToSpawn; j++)
		{
			toSpawn = spawnPool[1];

			screenX = Random.Range(c.bounds.min.x + 1, c.bounds.max.x - 1);
			screenZ = Random.Range(c.bounds.min.z + 3, c.bounds.max.z - 1);

			pos = new Vector3(screenX, .5f, screenZ);

			char thisChar = 'f';
			if ( lowerCharCount > 0 )
            {
				thisChar = lowerChars[Random.Range(0, 25)];
				lowerCharCount--;
            }
			else if ( upperCharCount > 0 )
			{
				thisChar = upperChars[Random.Range(0, 25)];
				upperCharCount--;
			}
			else if ( digitCount > 0)
			{
				thisChar = digits[Random.Range(0, 8)];
				digitCount--;
			}
			else if ( specialCharCount > 0)
			{
				thisChar = specialChars[Random.Range(0, specialChars.Length - 1)];
				specialCharCount--;
			}

			
			itemText.text = thisChar.ToString();
			GameObject a = Instantiate(toSpawn, pos, toSpawn.transform.rotation);
			a.name = thisChar.ToString();
		}
	}

}

