using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour 
{
    public Transform player;

    void LateUpdate ()
    {
        Vector3 newPostion = player.position;
        newPostion.y = transform.position.y;
        transform.position = newPostion;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }


}
