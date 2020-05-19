using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOff : MonoBehaviour {

    [SerializeField] GameObject blinkItem;
    [SerializeField] float activationTimer;
    [SerializeField] float timer;
    bool onOff = true;

    private void Start() {
        timer = activationTimer;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            SwapState();
        }
    }

    void SwapState() {
        if (onOff) {
            print("turn off");
            blinkItem.SetActive(false);
            onOff = false;
        } else {
            print("turn on");
            blinkItem.SetActive(true);
            onOff = true;
        }

        timer = activationTimer;

    }

}
