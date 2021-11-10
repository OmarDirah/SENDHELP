using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{

  public GameObject movingObstalce;
  public GameObject movingParent;
  public Rigidbody movingRb;
  public GameObject parent1;
  public GameObject objectToSpawn1;
  public GameObject parent2;
  public GameObject objectToSpawn2;

  //Food
  public GameObject parent3;
  public GameObject objectToSpawn3;
  private int foodGap = 3;
  private int numberToSpawn = 100;
  private Vector3 spawnLocation;
  private float Z_Modifier = 50f;
 
          
  // Start is called before the first frame update
  void Start(){

    Debug.Log("Spawning Obstacles");
    obstacleSpawner();

  }

  public void obstacleSpawner(){


    //loop to repeat spawning across the z axis
    for (int i = 0; i < numberToSpawn; i++){

      //first pick hurdle or obstacle
      int hurdleOrObstacle = randInt(0,3);
      
      //obstacle
      if(hurdleOrObstacle == 0){
        spawnObstacle(parent1, objectToSpawn1);
      }

      //hurdle
      else if(hurdleOrObstacle == 1){
        spawnHurdle(parent2, objectToSpawn2);
      }
      //spawn moving obstacle
      else{
        spawnMovingObstacle(movingParent, movingObstalce);
      }
    }
  }

  int randInt(int min, int max){

    //rand int min-max
    int modifier = Random.Range(min, max);

    return modifier;
  }

  void spawnHurdle(GameObject parent, GameObject objectToSpawn){

    //generate a location to spawn
    spawnLocation = new Vector3(0, 1, this.transform.position.z + Z_Modifier);
  
    //spawn the item
    Instantiate(objectToSpawn, spawnLocation, Quaternion.identity, parent.transform);

    Z_Modifier += 30; //increment z axis
  }

  void spawnObstacle(GameObject parent, GameObject objectToSpawn){
    
    int xRand = randInt(-6,-4); //generate the first leftmost X value -6 through -3
    int quantityOfObjects = randInt(2,4);

    //keep spawning for quantOfObjects
    for(int i = 0; i < quantityOfObjects; i++){

      //only spawn if we are within bounds
      if(xRand <= 6){
      
        //generate a location to spawn
        spawnLocation = new Vector3(this.transform.position.x + xRand, 1, this.transform.position.z + Z_Modifier);
      
        //spawn the item
        Instantiate(objectToSpawn, spawnLocation, Quaternion.identity, parent.transform);

        int gapIncrement = randInt(4, 7);

        xRand = xRand + gapIncrement;

        //if we have a big gap spawn food
        if(gapIncrement == 6){

          //generate a location to spawn
          spawnLocation = new Vector3(this.transform.position.x + foodGap, 1, this.transform.position.z + Z_Modifier);

          //spawn the item
          Instantiate(objectToSpawn3, spawnLocation, Quaternion.identity, parent3.transform);
        }
      }
      else{
        break;
      }
    }

    Z_Modifier += 15; //increment z axis
  }

   void spawnMovingObstacle(GameObject parent, GameObject objectToSpawn){

     int xSpawn = randInt(-5,6);
    
    //generate a location to spawn
    spawnLocation = new Vector3(xSpawn, 1f, this.transform.position.z + Z_Modifier);
  
    //spawn the item
    Instantiate(objectToSpawn, spawnLocation, Quaternion.identity, parent.transform);

    Z_Modifier += 15; //increment z axis
  }
}