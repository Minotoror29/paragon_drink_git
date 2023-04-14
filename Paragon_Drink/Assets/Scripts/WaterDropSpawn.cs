using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropSpawn : MonoBehaviour
{
    [SerializeField] private WaterDrop waterDropPrefab;
    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float maxSpawnRate = 5f;
    private float _spawnRateTimer = 0f;

    private void Start()
    {
        StartCoroutine(SpawnDrop());
    }

    private IEnumerator SpawnDrop()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));

        WaterDrop newDrop = Instantiate(waterDropPrefab, transform.position, Quaternion.identity);
        newDrop.Initialize();
        StartCoroutine(SpawnDrop());
    }
}
