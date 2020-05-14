using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Level Data")]
    [SerializeField] Level level;
    /*
    [SerializeField] string levelName;
    [TextArea(10,10)]
    [SerializeField] string levelDescription;
    [SerializeField] float startingFuel = 100f;
    [SerializeField] public bool fuelConsumption = false;
    [SerializeField] float fuelConsumptionRate = 1f;
    [SerializeField] float maxLandVelocity = 2f;
    */

    [SerializeField] public bool isStarted = false;

    [Header("UI")] // TODO Move some of this to UI Class
    [SerializeField] float levelLoadTime = 2f;
    [SerializeField] Transform obstacleHolder;

    [SerializeField] Text levelNameLabel;
    [SerializeField] Text levelDescriptionLabel;

    GameObject player;
    GameObject directionsPanel;
    [SerializeField] FuelBarController fbc;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        // UI Stuff TODO move to own class
        if (!level.fuelConsumption) {
            fbc.gameObject.SetActive(false);
        }
        
        // Setup UI
        levelNameLabel.text = level.levelName;
        levelDescriptionLabel.text = level.levelDescription;
    }

    public void StartLevel() {
        isStarted = true;
        player.GetComponent<Engine>().SetupData(level);
        StartSpawners();
    }

    void StartSpawners() {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("ObstacleSpawner");
        foreach (GameObject spawner in spawners) {
            spawner.GetComponent<ObstacleSpawner>().BeginSpawning();
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

    void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
          
    void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
