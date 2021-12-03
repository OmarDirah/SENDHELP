using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime: MonoBehaviour
{
    // VARIABLES
    public int timeToWait;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
    }


    IEnumerator Test()
    {
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("Wait is Over");
    }
}
