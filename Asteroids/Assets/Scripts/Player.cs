using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public Bullet bulletPrefab;

    private bool movingForward;
    private float turnDirection;

    public float moveSpeed = 1.0f;
    public float turnSpeed = 0.1f;

    public GameObject winTextObject;
    public GameObject lostTextObject;

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();

        winTextObject.SetActive(false);
        lostTextObject.SetActive(false);
    }

    void Update()
    {
        this.movingForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); 
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            this.turnDirection = 1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
        {
            this.turnDirection = -1.0f;
        }
        else
        {
            this.turnDirection = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if(this.movingForward)
        {
            this.rigidbody.AddForce(this.transform.up * this.moveSpeed);
        }

        if(turnDirection != 0.0f)
        {
            this.rigidbody.AddTorque(this.turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Asteroid")
        {
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDeath(this);
        }
    }
}
