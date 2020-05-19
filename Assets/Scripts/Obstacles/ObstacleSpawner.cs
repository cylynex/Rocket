/* Obstacle Spawner v1
 * 
 * What it Does:
 * Spawns obstacles.  Many configuration options to do various things once spawned.
 * 
 * Variables:
 * Object to Spawn - The GameObject you want to Spawn
 * Movement Direction - Vector for speed and direction you want the object to move in, if you want it to move.
 *      X: Will move along the X axis at a speed based on the number you give it.  + = Left->Right.  - = Right-> Left
 *      Y: Will move along the Y axis at a speed based on the number you give it.  + = Up.  - = Down.
 * 
 * Random Variables:
 * If you check Randomize All, it will cook up random X and Y variables based on the range you supply in the Min and Max Fields.
 * Standard Movement rules apply for setting these values (See movement direction above).
 * 
 * Repeating Spawner Variables:
 * This allows the spawner to not spawn just 1 item, but continuously spawn more.  Set the timer you want it to spawn at.
 * You can also randomize the timer, with a Min and Max Range.
 * 
 * Dashboard:
 * Informational only.
 */

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
