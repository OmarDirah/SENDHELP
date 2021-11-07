using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D bc;
    public LayerMask onGroundLayer;

    public Sprite leftWalkingSprite;
    public Sprite rightWalkingSprite;
    public Sprite leftJumpingSprite;
    public Sprite rightJumpingSprite;

    public float movementSpeed = 7.0f;
    public float jumpForce = 14.0f;

    public TextMeshProUGUI headerInformationText;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = rightWalkingSprite;

        headerInformationText.text = " ";
    }
    
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if ( ( (Input.GetKeyDown(KeyCode.Space) ) || (Input.GetKeyDown(KeyCode.W) ) || (Input.GetKeyDown(KeyCode.UpArrow) ) ) && (IsGrounded()) )
        {
            rb.velocity = new Vector2(rb.velocity.x, 14f);
            
            if ( (spriteRenderer.sprite == leftWalkingSprite) )
            {
                spriteRenderer.sprite = leftJumpingSprite;
            }
            else if ((spriteRenderer.sprite == rightWalkingSprite))
            {
                spriteRenderer.sprite = rightJumpingSprite;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            spriteRenderer.sprite = leftWalkingSprite;
        }
        else if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            spriteRenderer.sprite = rightWalkingSprite;
        } 

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(TriggerRespawn());
        }
    }

    void RestartLevel()
    {
        headerInformationText.text = " ";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        enabled = true;
    }



    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, onGroundLayer);
    }

    private IEnumerator TriggerRespawn()
    {
        headerInformationText.text = "PRESS SPACE TO RESPAWN";
        enabled = false;
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        Invoke(nameof(RestartLevel), 0f);
    }
}