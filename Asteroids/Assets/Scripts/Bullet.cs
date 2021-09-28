using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5000.0f;
    public float timeToLive = 10.0f;

    private Rigidbody2D rigidbody;


    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        this.rigidbody = GetComponent<Rigidbody2D>();

        this.rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.timeToLive);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
