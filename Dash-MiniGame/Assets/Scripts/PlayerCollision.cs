using UnityEngine;

public class PlayerCollision : MonoBehaviour {

  public PlayerMovement movement;

  void OnCollisionEnter (Collision collisionInfo){

    if(collisionInfo.collider.tag == "Obstacle" || collisionInfo.collider.tag == "Hurdle"){
      movement.enabled = false;

      //end the game
      FindObjectOfType<GameManager>().EndGame();
    }

    if(collisionInfo.collider.tag == "Food"){
      
      //destroy the food once we hit it
      Destroy(collisionInfo.collider.gameObject);

    }
  }
}
