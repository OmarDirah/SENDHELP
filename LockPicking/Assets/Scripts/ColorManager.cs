using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color originalColor;
    public Color winColor;
    public Color loseColor;

    Camera _cam;

    void Start()
    {
        _cam = GetComponent<Camera>();
        _cam.backgroundColor = originalColor;
    }

    public void ChangeToLost()
    {
        _cam.backgroundColor = loseColor;
    }

    public void ChangeToWin()
    {
        _cam.backgroundColor = winColor;
    }

    public void ChangeToOriginal()
    {
        _cam.backgroundColor = originalColor;
    }
}
