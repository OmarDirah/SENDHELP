using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

bool gameHasEnded = false;

  public void EndGame(){

    if(gameHasEnded == false){
      
      gameHasEnded = true;
      Debug.Log("Game over");

      //delay a few frames before restarting
      Invoke("restart", 1f);

    }
  }

  void restart(){

    //restart current scene
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

  }
}
