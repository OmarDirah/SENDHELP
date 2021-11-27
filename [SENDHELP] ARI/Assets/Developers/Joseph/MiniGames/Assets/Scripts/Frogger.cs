using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Frogger : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite deathSprite;

    public TextMeshProUGUI headerInformationText;

    private Vector3 spawnPosition;

    private string defaultMessage = "BREACH THE DEFENSE LAYERS";

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
        headerInformationText.text = defaultMessage;
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow) ) )
        {
            Rotate(Quaternion.Euler(0f, 0f, 0f));
            Move(Vector3.up);
        }
        else if ( Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.DownArrow) ) )
        {
            Rotate(Quaternion.Euler(0f, 0f, 180f));
            Move(Vector3.down);
        }
        else if ( Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow) ) )
        {
            Rotate(Quaternion.Euler(0f, 0f, 90f));
            Move(Vector3.left);
        }
        else if ( Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow) ) )
        {
            Rotate(Quaternion.Euler(0f, 0f, -90f));
            Move(Vector3.right);
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        Collider2D wall = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Wall"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Float"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        if ( wall != null)
        {
            return;
        }

        if ( platform != null)
        {
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if ( ( obstacle != null) && (platform == null) )
        {
            transform.position = destination;
            Death();
        }
        else
        {
            StartCoroutine(Leap(destination));
        }
    }

    void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( (enabled) && 
             (other.gameObject.layer == LayerMask.NameToLayer("Obstacle") ) && 
             ( transform.parent == null ) )
        {
            Death();
        }

        if ((enabled) && (other.gameObject.tag == "Finish"))
        {
	    Debug.Log("TIME ELAPSED: " + Time.time);
            headerInformationText.text = "SECURITY BREACHED!";
            enabled = false;
        }
    }

    void Death()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deathSprite;
        enabled = false;

        StartCoroutine(TriggerRespawn());
    }

    void Respawn()
    {
        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        spriteRenderer.sprite = idleSprite;
        headerInformationText.text = defaultMessage;
        enabled = true;
    }

    private IEnumerator Leap(Vector3 destination)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;
        
        while ( elapsed < duration )
        {
            transform.position = Vector3.Lerp(startPos, destination, (elapsed / duration) );

            elapsed += Time.deltaTime;
            
            yield return null;
        }

    }

    private IEnumerator TriggerRespawn()
    {
        headerInformationText.text = "PRESS SPACE TO RESPAWN";
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        
        Invoke(nameof(Respawn), 0f);
    }
}
