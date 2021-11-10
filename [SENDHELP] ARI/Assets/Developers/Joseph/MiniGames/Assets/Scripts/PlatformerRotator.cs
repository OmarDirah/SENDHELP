using UnityEngine;
using System.Collections;

public class PlatformerRotator : MonoBehaviour
{
	void Update()
	{
		transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
	}
}