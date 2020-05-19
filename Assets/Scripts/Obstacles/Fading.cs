using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    [SerializeField] float adjustAmount = 1f;
    Color objectColor;
    bool direction = false;
    float safeTimer = 5f;
    float timer = 0;
    bool positionTimer;

    private void Start() {
        objectColor = GetComponent<MeshRenderer>().material.color;
    }

    private void FixedUpdate() {

        if (positionTimer) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                positionTimer = false;
                GetComponent<BoxCollider>().enabled = true;
            }
        } else {

            if (direction == false) {
                AlphaDown();
            } else {
                AlphaUp();
            }

            GetComponent<MeshRenderer>().material.color = objectColor;

            if (objectColor.a <= 0) {
                direction = true;
                timer = safeTimer;
                positionTimer = true;
                GetComponent<BoxCollider>().enabled = false;
            } else if (objectColor.a >= 1) {
                direction = false;
            }
        }
    }   

    void AlphaDown() {
        objectColor.a -= adjustAmount * Time.deltaTime;
    }

    void AlphaUp() {
        objectColor.a += adjustAmount * Time.deltaTime;
    }

}