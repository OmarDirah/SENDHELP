using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoredMotor : MonoBehaviour
{

    public int Speed = 100;

    Transform _anchor;
    public Direction _direction = Direction.Clockwise;

    public GameData GameData;

    Vector3 _initialPos;

    void Start()
    {
        _anchor = GameObject.FindGameObjectWithTag("Anchor").transform;
        _initialPos = GetComponent<Transform>().localPosition;
    } 

    void Update()
    {

        if (GameData.isRunning)
        {
            transform.RotateAround(_anchor.position, Vector3.forward, Speed * Time.deltaTime * (int)(_direction));
        }

    }

    public void ChangeDirection()
    {
        if(_direction == Direction.Clockwise)
        {
            _direction = Direction.CounterClockwise;
        }
        else if(_direction == Direction.CounterClockwise)
        {
            _direction = Direction.Clockwise;
        }
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, _initialPos.y, 0);
        transform.localRotation = Quaternion.identity;
    }

}

public enum Direction
{
    Clockwise = -1,
    CounterClockwise = 1
}
