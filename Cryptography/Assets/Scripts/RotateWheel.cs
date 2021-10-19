using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{

    void Update()
    {
        if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) ) 
        {
            transform.Rotate(0, 0, -.05f);
        }
        else if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) )
        {
            transform.Rotate(0, 0, .05f);
        } 
    }
}
