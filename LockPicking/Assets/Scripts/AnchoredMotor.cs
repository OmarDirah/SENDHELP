using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoredMotor : MonoBehaviour
{

    public int Speed = 10;

    Transform _anchor;
    public Direction _direction = Direction.Clockwise;
    bool _isRunning = false;

    void Start()
    {
        _anchor = GameObject.FindGameObjectWithTag("Anchor").transform;
    } 

    void Update()
    {

        if (_isRunning)
        {
            transform.RotateAround(_anchor.position, Vector3.forward, Speed * Time.deltaTime * (int)(_direction));
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (_isRunning)
                {
                    ChangeDirection();
                }
                else
                {
                    _isRunning = true;
                }
            }
        }
    }

    void ChangeDirection()
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

}

public enum Direction
{
    Clockwise = 1,
    CounterClockwise = -1
}
