using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Node : MonoBehaviour
{
    
    public Transform cameraPosition;
    public List<Node> reachableNodes = new List<Node>();


    // Node collider 
    [HideInInspector]
    public Collider col;
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
        
    }

    // On mouse down when a node collider is clicked camera moves to position and rotation 
    void OnMouseDown() 
    {
        Arrive();
        




    }

    public virtual void Arrive()
    {
        // Leave existing current node

        if (GameManager.ins.currentNode != null)
        {
            GameManager.ins.currentNode.Leave();
        }
        // set current node
        GameManager.ins.currentNode = this;

        // move camera
        GameManager.ins.rig.AlignTo(cameraPosition);




        //Old camera movement. only uncomment for testing without DOTween functions!
        //Camera.main.transform.position = cameraPosition.position;
        //Camera.main.transform.rotation = cameraPosition.rotation;


        // turn off collider 
        if (col != null)
        {
            col.enabled = false;
        }
         // turn on all reachable node's colliders
        foreach (Node node in reachableNodes)
        {
            if (node.col != null)
            {
                node.col.enabled = true;
            }
        }


    }

    public virtual void Leave()
    {
        // turn off all reachable nodes
        foreach (Node node in reachableNodes)
        {
            if (node.col != null)
            {
                node.col.enabled = false;
            }
        }


    }
    
}
