using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*jumping tutorial
https://www.youtube.com/watch?v=vdOFUFMiPDU
*/

public class PlayerMovement : MonoBehaviour{

  public Rigidbody rb;
  //end floats with f in c#
  private float sidewaysForce; //side to side
  private float fowardForce; //move foward
  private float jumpForce = 70000f; //jumping
  private float brakeForce = -5000f; //reduce speed when we jump
  private float drag = -50f;

  // Update is called once per frame
  //use FixedUpdate in c# unity for better gameplay
  void FixedUpdate(){

    //if the player is airborne we will adjust some variables
    //have to hardcode due to constant refresh
    if(isGrounded()){
      fowardForce = 10000f;
      sidewaysForce = 60f;
    }
    else{
      sidewaysForce = 30f;
      fowardForce += drag; //each frame take away 10 speed 
    }
    
    
    //always be moving foward. 
    //Dont want ForceMode.VelocityChange
    moveWithMomentum(0, 0, fowardForce * Time.deltaTime);  
    
    //move right
    if(Input.GetKey("d")){
      move(sidewaysForce * Time.deltaTime, 0, 0);
    }

    //move left
    if(Input.GetKey("a")){
      move(-sidewaysForce * Time.deltaTime, 0, 0);
    }

    //jump
    if(isGrounded() && (Input.GetKey(KeyCode.Space) || Input.GetKey("w"))){
      
      //go up and slow down a bit, reduce movement ability in the air
      moveWithMomentum(0, jumpForce * Time.deltaTime, brakeForce * Time.deltaTime);
    }

    //end game if we fall off
    if(rb.position.y < 0f){
      FindObjectOfType<GameManager>().EndGame();
    }
  }

  //check if were touching the plane
  //if we are in the air we will lose sideways movement by 50%
  private bool isGrounded(){

    bool grounded = true;

    //if player is > 1.1 off the ground, theyre airborne
    if(rb.position.y > 1.1f){
      grounded = false;
    }
  
    return grounded;
  }

  private void move(float x, float y, float z){
    rb.AddForce(x, y, z, ForceMode.VelocityChange); //Momentum ignored
  }

  private void moveWithMomentum(float x, float y, float z){
    rb.AddForce(x,y,z);
  }
}

