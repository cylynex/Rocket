using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [Header("Level Stuff")]
    //public string[] levelSceneNames;
    //public static int levelIndex = 0;
    // TODO map should be dynamically assigned from somewhere else.
    [SerializeField] public Map currentMap;
    [SerializeField] Level currentLevel;
    [SerializeField] static int currentLevelIndex = 0;
    [SerializeField] float levelLoadTime = 3f;
    [SerializeField] Transform obstacleHolder;

    private void Start() {
        LoadLevel();
    }

    void LoadLevel() {
        print("levels in this map: " + currentMap.levels.Length);
        currentLevel = currentMap.levels[currentLevelIndex];
        if (currentLevel.obstacles.Length > 0) {
            print("Obstacles on this level - build now.");
            CreateObstacles();
        } else {
            print("No Obstacles on this level to build");
        }
    }

    void CreateObstacles() {
        //
        /*
        foreach (Obstacle obstacle in currentLevel.obstacles) {
            print(obstacle.name + " - spawn at: " + obstacle.startingPosition);
            GameObject thisObstacle = Instantiate(obstacle.obstacleObject, obstacle.startingPosition, Quaternion.identity, obstacleHolder);
            thisObstacle.GetComponent<MovingObstacle>().direction = obstacle.startingDirection;
        }
        */

        for (int i = 0; i < currentLevel.obstacles.Length; i++) {
            GameObject thisObstacle = Instantiate(currentLevel.obstacles[i].obstacleObject, currentLevel.obstacleSpawnPositions[i], Quaternion.identity, obstacleHolder);
            thisObstacle.GetComponent<MovingObstacle>().direction = currentLevel.obstacles[i].startingDirection;
        }

    }

    public void LoadLevel(string levelToLoad) {
        switch (levelToLoad) {
            case "Reload":
                Invoke("ReloadLevel", levelLoadTime);
                break;
            default:
                Invoke("LoadNextLevel", levelLoadTime);
                break;
        }
    }
          
    void ReloadLevel() {
        print("Reload this level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel() {
        currentLevelIndex++;
        print(currentLevelIndex + " - " + currentMap.levels.Length);
        if (currentLevelIndex < currentMap.levels.Length) {
            print("Next level found - ok to load");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else {
            print("last level for this map already beaten - do something else");
        }
    }

}
