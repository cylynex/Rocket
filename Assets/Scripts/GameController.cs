using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [Header("Level Stuff")]
    public string[] levelSceneNames;
    public static int levelIndex = 0;
    [SerializeField] float levelLoadTime = 3f;

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
        levelIndex++;
        //SceneManager.LoadScene()
    }

}
