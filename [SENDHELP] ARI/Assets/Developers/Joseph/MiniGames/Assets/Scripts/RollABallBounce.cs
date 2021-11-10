using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollABallBounce : MonoBehaviour
{

    public float bounceForce = 200.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 bounceDir = -playerRb.velocity;
                playerRb.AddForce(bounceDir * bounceForce);
            }
        }
    }
}
