using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float spawnTime = 2.0f;
    public int spawnRate = 1;
    public float spawnDistance = 12.0f;

    public float trajectoryVariance = 15.0f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnTime, this.spawnTime);
    }

    public void Spawn()
    {
        for (int i = 0; i < this.spawnRate; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the asteroid a distance away
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * this.spawnDistance;

            // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            spawnPoint += this.transform.position;

            // Calculate a random variance in the asteroid's rotation which will
            // cause its trajectory to change
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Create the new asteroid by cloning the prefab and set a random
            // size within the range
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            // Set the trajectory to move in the direction of the spawner
            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
        }
    }
}
