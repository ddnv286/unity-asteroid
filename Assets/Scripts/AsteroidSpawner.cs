using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float spawnDistance = 15.0f;
    public Asteroid asteroidPrefab;
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float trajectoryVariance = 15.0f;

    private void Start ()
    {
        // repeat the function over a fixed time
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn ()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            // random radial direction
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            // offset spawn point
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            // random rotation and size
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            // set trajectory
            asteroid.SetTrajectory(rotation * -spawnDirection);
        } 
    }
}
