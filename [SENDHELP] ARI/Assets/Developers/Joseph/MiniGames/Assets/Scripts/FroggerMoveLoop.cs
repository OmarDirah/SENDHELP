using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerMoveLoop : MonoBehaviour
{

    public Vector2 direction = Vector2.right;
    public float speed = 1f;
    public int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        if ( (direction.x > 0) && ( (transform.position.x - size) > rightEdge.x) ) //moving right
        {
            Vector3 position = transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;
        }
        else if ((direction.x < 0) && ((transform.position.x + size) < leftEdge.x)) //moving left
        {
            Vector3 position = transform.position;
            position.x = rightEdge.x + size;
            transform.position = position;
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
