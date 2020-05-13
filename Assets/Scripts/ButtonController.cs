using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    [SerializeField] GameController gc;
    [SerializeField] GameObject parentPanel;

    public void StartLevel() {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        parentPanel.GetComponent<Animation>().Play("DPanelOut");
        gc.GetComponent<GameController>().StartLevel();
        Invoke("GoAway", 0.5f);
    }

    void GoAway() {
        gameObject.SetActive(false);
    }

}
