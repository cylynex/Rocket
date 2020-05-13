using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Level Setup")]
    [SerializeField] string levelName;
    [TextArea(10,10)]
    [SerializeField] string levelDescription;
    [SerializeField] float startingFuel = 100f;
    [SerializeField] public bool fuelConsumption = false;
    [SerializeField] float fuelConsumptionRate = 1f;
    [SerializeField] float maxLandVelocity = 2f;
    [SerializeField] public bool isStarted = false;
    Level thisLevel = new Level();

    [Header("UI")] // TODO Move some of this to UI Class
    [SerializeField] float levelLoadTime = 2f;
    [SerializeField] Transform obstacleHolder;

    [SerializeField] Text levelNameLabel;
    [SerializeField] Text levelDescriptionLabel;

    GameObject player;
    GameObject directionsPanel;
    FuelBarController fbc;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        fbc = GameObject.FindGameObjectWithTag("FuelController").GetComponent<FuelBarController>();
        
        // Setup Level Object
        thisLevel.levelName = levelName;
        thisLevel.levelDescription = levelDescription;
        thisLevel.startingFuel = startingFuel;
        thisLevel.fuelConsumption = fuelConsumption;
        thisLevel.fuelConsumptionRate = fuelConsumptionRate;
        thisLevel.maxLandVelocity = maxLandVelocity;

        // UI Stuff TODO move to own class
        if (!fuelConsumption) {
            fbc.gameObject.SetActive(false);
        }
        
        // Setup UI
        levelNameLabel.text = levelName;
        levelDescriptionLabel.text = levelDescription;
    }

    public void StartLevel() {
        isStarted = true;
        player.GetComponent<Engine>().SetupData(thisLevel);
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
