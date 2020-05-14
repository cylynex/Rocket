using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [Header("Setup")]
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Vector3 movementDirection;
    
    [Header("Random Variables")]
    [SerializeField] bool randomizeAll;
    [SerializeField] Vector3 randomMin;
    [SerializeField] Vector3 randomMax;

    [Header("Repeating Spawner Settings")]
    [SerializeField] bool repeating = false;
    [SerializeField] float spawnTime;
    [SerializeField] bool randomSpawnTimer = false;
    [SerializeField] Vector2 randomTimerRange;
    
    [Header("Dashboard")]
    [SerializeField] Vector3 randomAll;
    [SerializeField] bool spawnStarted = false;
    [SerializeField] float spawnTimer;

    public void BeginSpawning() {
        SpawnObstacle();

        if (repeating) {
            SetupSpawnTimer();
        }

        spawnStarted = true;
        
    }

    private void Update() {
        if (spawnStarted && repeating) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                SpawnObstacle();
                SetupSpawnTimer();
            }
        }
    }

    void SpawnObstacle() {
        GameObject newItem = Instantiate(objectToSpawn, transform.position, Quaternion.identity, transform);
        newItem.name = "(Mobile)" + newItem.name;
        if (randomizeAll) {
            randomAll = new Vector3(
                Random.Range(randomMin.x, randomMax.x),
                Random.Range(randomMin.y, randomMax.y),
                Random.Range(0, 0)
                );
            newItem.GetComponent<Rigidbody>().AddForce((randomAll * 100) * Time.deltaTime);
        }
        else {
            newItem.GetComponent<Rigidbody>().AddForce((movementDirection * 100) * Time.deltaTime);
        }
    }

    void SetupSpawnTimer() {
        if (randomSpawnTimer) {
            spawnTimer = Random.Range(randomTimerRange.x, randomTimerRange.y);
        } else {
            spawnTimer = spawnTime;
        }
    }

}
