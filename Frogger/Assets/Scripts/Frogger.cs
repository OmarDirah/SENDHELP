using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogger : MonoBehaviour
{

    void Start()
    {
        
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
        StartCoroutine(Leap(destination));
    }

    void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
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
}
