using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [Header("Level Stuff")]
    //public string[] levelSceneNames;
    //public static int levelIndex = 0;
    // TODO map should be dynamically assigned from somewhere else.
    [SerializeField] Map currentMap;
    [SerializeField] Level currentLevel;
    [SerializeField] int currentLevelIndex = 0;
    [SerializeField] float levelLoadTime = 3f;
    [SerializeField] Transform obstacleHolder;

    private void Start() {
        print("GameController Online and ready");
        LoadLevel();
    }

    void LoadLevel() {
        currentLevel = currentMap.levels[currentLevelIndex];
        if (currentLevel.obstacles.Length > 0) {
            print("Obstacles on this level - build now.");
            CreateObstacles();
        } else {
            print("No Obstacles on this level to build");
        }
    }

    void CreateObstacles() {

        foreach (Obstacle obstacle in currentLevel.obstacles) {
            print(obstacle.name);
            Instantiate(obstacle.obstacleObject, obstacle.startingPosition, Quaternion.identity, obstacleHolder);
        }

    }














    public void LoadLevel(string levelToLoad) {
        switch(levelToLoad) {
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

    // TODO finish this but with new SO system
    void LoadNextLevel() {
        //levelIndex++;
        //SceneManager.LoadScene()
    }

}
