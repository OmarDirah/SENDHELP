using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class RollABallPlayerController : MonoBehaviour
{
    // VARIABLES
    public string SceneToLoad;
	public float speed;
	public float maxSpeed = 15.0f;
    public Animator anim;
    public int delayTime;

    public TextMeshProUGUI countText;
	public GameObject winTextObject;

	private float movementX;
	private float movementY;
	private Rigidbody rb;

	private string count;
	private GameObject[] characters;
	private Vector3 startPos;

	private char[] lowerChars = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
											'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
	private char[] upperChars = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
											'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
	private char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	private char[] specialChars = new char[] {'!', '"', '#', '$', '%', '&', '\'', '*', '+', ',', '.', '/',
												':', ';', '=', '?', '@', '\\', '^', '~', '`', '|'};
	
	private bool hasLower, hasUpper, hasDigit, hasSpecial = false;

	public TextMeshPro objectText;

    // START
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = "";

		characters = GameObject.FindGameObjectsWithTag("PickUps");

		startPos = rb.position;

		SetCountText();

		winTextObject.SetActive(false);
	}

    // FIXED UPDATE
	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);

		EnforceMaxSpeed();
	}

    // ENFORCE MAX SPEED
    void EnforceMaxSpeed()
    {
		if (rb.velocity.magnitude > maxSpeed)
        {
			rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    // ON TRIGGER ENTER
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("PickUps"))
		{
			other.gameObject.SetActive(false);
			Debug.Log(other.gameObject.ToString().Substring(0, 1) + " PICKED UP");

			count += other.gameObject.ToString().Substring(0,1);

			SetCountText();
		}

		if (other.gameObject.CompareTag("Finish"))
		{
			WinCondition();
		}

		if (other.gameObject.CompareTag("Respawn"))
		{
			Restart();
		}

	}

    // ON MOVE
	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

    // SET COUNT TEXT
	void SetCountText()
	{
		countText.text = "PASSWORD: " + count.ToString();
	}

    // WIN CONDITION
	void WinCondition()
    {
		if (count.Length >= 8)
		{
			char[] charArr = count.ToCharArray();
			foreach (char c in charArr)
			{

				if(lowerChars.Contains(c))
                {
					hasLower = true;
                }				
				else if(upperChars.Contains(c))
                {
					hasUpper = true;
                }
				else if (digits.Contains(c))
				{
					hasDigit = true;
				}
				else if (specialChars.Contains(c))
				{
					hasSpecial = true;
				}
			}

			if(hasLower & hasUpper & hasDigit & hasSpecial)
            {
                // WIN CONDITION!
		        Debug.Log("Time Elapsed: " + Time.time);
                winTextObject.SetActive(true);

                anim.SetBool("MinigameWon", true);
                Invoke("DelayedAction", delayTime);

            }
            else
            {
				countText.text = "YOU ARE MISSING STUFF!";
            }
		}
        else
        {
			countText.text = "PASSWORD SHOULD BE AT LEAST 8 CHARACTERS";

		}
	}

    // DELAYED ACTION
    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }

    // RESTART
	void Restart()
    {
		count = "";
		SetCountText();

		for (int i = 0; i < characters.Length; i++)
        {
			characters[i].gameObject.SetActive(true);
        }


		rb.position = startPos;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
    }
}