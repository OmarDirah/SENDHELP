using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int bouncersToSpawn;
	public int charsToSpawn;
    public List<GameObject> spawnPool;
    public GameObject quad;

    void Start()
    {
        SpawnObjects();
    }

	public void SpawnObjects()
	{
		GameObject toSpawn;
		MeshCollider c = quad.GetComponent<MeshCollider>();

		float screenX, bouncerY, screenZ;
		Vector3 pos;

		for (int i = 0; i < bouncersToSpawn; i++)
		{
			toSpawn = spawnPool[0];

			bouncerY = Random.Range(-.5f, 1f);

			screenX = Random.Range(c.bounds.min.x + .5f, c.bounds.max.x - .5f);
			screenZ = Random.Range(c.bounds.min.z + .3f, c.bounds.max.z - 1.5f);

			pos = new Vector3(screenX, bouncerY, screenZ);

			Instantiate(toSpawn, pos, toSpawn.transform.rotation);
		}

		for (int i = 0; i < charsToSpawn; i++)
		{
			toSpawn = spawnPool[1];
			
			screenX = Random.Range(c.bounds.min.x + 1, c.bounds.max.x - 1);
			screenZ = Random.Range(c.bounds.min.z + 3, c.bounds.max.z - 1);

			pos = new Vector3(screenX, .5f, screenZ);

			Instantiate(toSpawn, pos, toSpawn.transform.rotation);
		}



	}
}
