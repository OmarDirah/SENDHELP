using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;
    public int lives = 3;
    public int respawnTimer = 3;
    public int spawnProtection = 3;

    private int score = 0;

    public GameObject winTextObject;
    public GameObject lostTextObject;

    public ParticleSystem explosion;

    public void AsteroidDestroyed(Asteroid asteroid)
    {

        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.8f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.1f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

        if (score > 500)
        {
            GameWon();
        }
    }


    public void PlayerDeath(Player player)
    {

        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

        if (this.lives <= 0)
        {
            GameLost();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTimer);
        }
    }

    void GameLost()
    {
        lostTextObject.SetActive(true);

        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), this.respawnTimer);
        lostTextObject.SetActive(false);
    }

    void GameWon()
    {
        winTextObject.SetActive(true);
    }

    public void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);

        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        Invoke(nameof(TurnOnCollisions), spawnProtection);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
