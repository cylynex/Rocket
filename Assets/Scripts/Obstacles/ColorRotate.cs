using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRotate : MonoBehaviour {

    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] Color currentColor;
    [SerializeField] float speedFactor = 1f;
    Color nextColor;
    float timer;

    private void Start() {
        currentColor = color1;
        nextColor = color2;
    }

    private void Update() {
        if (currentColor == color1) {
            nextColor = color2;
        } else if (currentColor == color2) {
            nextColor = color1;
        }

        timer = 0;
        timer += Time.deltaTime * speedFactor;
        currentColor = Color.Lerp(currentColor, nextColor, timer);
        GetComponent<MeshRenderer>().material.color = currentColor;
    }

}