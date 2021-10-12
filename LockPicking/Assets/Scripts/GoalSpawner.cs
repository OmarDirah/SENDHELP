using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSpawner : MonoBehaviour
{

    public AnchoredMotor Motor;
    public GameObject GoalPrefab;

    public GameData GameData;

    GameObject ActiveDot;


    void Start()
    {
        GameData.ResetLevel();
        Spawn();
    }

    public void Spawn()
    {
        Debug.Log("IN SPAWN");
        if (ActiveDot)
        {
            Debug.Log("IN ACTIVE");
            Destroy(ActiveDot.gameObject);
        }

        if (GameData.GoalsLeft > 0)
        {
            var angle = Random.Range(40, 120);
            ActiveDot = Instantiate(GoalPrefab, Motor.transform.position, Quaternion.identity, transform);
            ActiveDot.transform.RotateAround(transform.position, Vector3.forward, -angle * (int)Motor._direction);
        }
    }

}
