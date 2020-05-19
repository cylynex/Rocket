/* Color Rotate v1
 * 
 * What it Does:
 * Rotates between 2 materials on a gameobject.
 * 
 * Variables:
 * Color1 - The First color you want to rotate between.
 * Color2 - The Second color you want to rotate between.
 * Speed Factor - How fast it fades between colors.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRotate : MonoBehaviour {

    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] float speedFactor = 1f;
    Color currentColor;
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