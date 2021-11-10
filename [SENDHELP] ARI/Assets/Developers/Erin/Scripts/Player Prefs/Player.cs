using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    public string levelActive;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Your Player Data has now been saved.");
        Debug.Log("Your Position Saved: x:" + transform.position.x + ", y:" + transform.position.y + ", z:" + transform.position.z);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        levelActive = data.levelInstance;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
