using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float maxSpeed = 15.0f;

	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	private float movementX;
	private float movementY;
	private Rigidbody rb;

	private int count;
	private GameObject[] characters;
	private Vector3 startPos;

	private char[] lowerChars = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
											'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
	private char[] upperChars = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
											'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
	private char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	private char[] specialChars = new char[] {'!', '"', '#', '$', '%', '&', '\'', '*', '+', ',', '.', '/',
												':', ';', '=', '?', '@', '\\', '^', '~', '`', '|'};



	// At the start of the game..
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		characters = GameObject.FindGameObjectsWithTag("PickUps");

		startPos = rb.position;

		SetCountText();

		winTextObject.SetActive(false);
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);

		EnforceMaxSpeed();
	}

	void EnforceMaxSpeed()
    {
		if (rb.velocity.magnitude > maxSpeed)
        {
			rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("PickUps"))
		{
			other.gameObject.SetActive(false);

			count = count + 1;

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

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
	}

	void WinCondition()
    {
		if (count >= 12)
		{
			winTextObject.SetActive(true);
		}
        else
        {
			countText.text = "NOT DONE";

		}
	}

	void Restart()
    {
		count = 0;
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