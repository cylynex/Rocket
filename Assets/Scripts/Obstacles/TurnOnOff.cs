/* Blink v1
 * 
 * What it Does:
 * Blinks the controlled gameobject in and out of existance on the map.
 * 
 * Variables:
 * Blink Item(s) - set number of items you want this script to control then drag in the gameobjects it will control
 * Activation Timer - The Cycle time for a swap of active state
 * Timer - Debug only.  Shows you how long till next cycle.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOff : MonoBehaviour {

    [SerializeField] GameObject[] blinkItem;
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
            ChangeState(false);
            //blinkItem.SetActive(false);
            //onOff = false;
        } else {
            ChangeState(true);
            //blinkItem.SetActive(true);
            //onOff = true;
        }

        timer = activationTimer;

    }

    void ChangeState(bool whichWay) {
        for(int i = 0; i < blinkItem.Length; i++) {
            blinkItem[i].SetActive(whichWay);
            onOff = whichWay;
        }
    }

}
