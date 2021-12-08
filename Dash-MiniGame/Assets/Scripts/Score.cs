using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

  public Transform player;
  public Text scoreText;

  // Update is called once per frame
  void Update(){

    //adding zero inside the parens will round and create a clean looking score
    scoreText.text = player.position.z.ToString("0");
  
  }
}
