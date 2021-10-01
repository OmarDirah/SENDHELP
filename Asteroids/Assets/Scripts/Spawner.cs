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
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * this.spawnDistance;

            spawnPoint += this.transform.position;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
        }
    }
}
