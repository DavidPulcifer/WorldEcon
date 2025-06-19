using System;
using UnityEngine;

namespace WorldEcon.World
{
    public class CitizenSpawner : MonoBehaviour
    {
        [SerializeField] GameObject citizenPrefab;
        [Range(0,60)][SerializeField] float minSpawnTime = 2f;
        [Range(0,300)][SerializeField] float maxSpawnTime = 10f;

        void Start()
        {
            Invoke("SpawnCitizen", UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
        }

        void SpawnCitizen()
        {
            Instantiate(citizenPrefab, transform.position, Quaternion.identity);
            Invoke("SpawnCitizen", UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}