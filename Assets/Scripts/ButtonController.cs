using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour {

    [SerializeField] GameController gc;
    [SerializeField] GameObject parentPanel;
    bool gameStarted = false;

    public void StartLevel() {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        parentPanel.GetComponent<Animation>().Play("DPanelOut");
        gc.GetComponent<GameController>().StartLevel();
        Invoke("GoAway", 0.5f);
    }

    private void Update() {
        if (!gameStarted) {
            if (Input.GetKey(KeyCode.JoystickButton1)) {
                gameStarted = true;
                StartLevel();
            }
        }
    }

    void GoAway() {
        gameObject.SetActive(false);
    }
}
