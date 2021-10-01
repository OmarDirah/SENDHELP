using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private SpriteRenderer sr;

    public Sprite[] sprites;
    private int spriteIndex;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    public float speed = 50.0f;
    public float timeToLive = 30.0f;

    private Rigidbody2D rigidbody;


    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();

        this.sr = GetComponent<SpriteRenderer>();

        this.spriteIndex = Random.Range(0, sprites.Length);

        this.sr.sprite = sprites[spriteIndex];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        this.rigidbody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        this.rigidbody = GetComponent<Rigidbody2D>();

        this.rigidbody.AddForce(direction * this.speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if ((this.size / 2) >= this.minSize)
            {
                Debug.Log(this);
                SplitAsteroid();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    void SplitAsteroid()
    {
        Vector2 position = this.transform.position;
        Vector2 position1 = position + Random.insideUnitCircle * 0.5f;
        Vector2 position2 = position + Random.insideUnitCircle * 0.5f;

        Asteroid half1 = Instantiate(this, position1, this.transform.rotation);
        half1.size = this.size / 2;
        half1.SetTrajectory(Random.insideUnitCircle.normalized);

        Asteroid half2 = Instantiate(this, position2, this.transform.rotation);
        half2.size = this.size / 2;
        half2.SetTrajectory(Random.insideUnitCircle.normalized);
    }

}
