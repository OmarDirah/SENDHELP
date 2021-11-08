using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static ref to game manager 
    public static GameManager ins;

    // Starting Node
    public Node startingNode;
    // Keep track of current node
    [HideInInspector]
    public Node currentNode;
    public CameraRig rig;



    // instaniation of game manager 
    void Awake() 
    {

        ins = this;
    
    
    }

    void Start() {

        startingNode.Arrive();
    
    
    
    
    
    }
     void Update()
    {
        // check for mouse input and if current node is a prop
        if (Input.GetMouseButtonDown(1) && currentNode.GetComponent<Prop>() != null)
        {
            currentNode.GetComponent<Prop>().loc.Arrive();
        }
    }

}
