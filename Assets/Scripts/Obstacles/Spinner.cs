/* Spinner Obstacle Script v1
 * 
 * spinSpeed Sets the Speed the object will rotate at.
 * You can make a parent gameobject that has this script on it to spin any 
 * other object or objects you want to rotate, at either the center or any side points
 * to create any sort of basic rotation
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] float spinSpeed;

    private void Update() {
        transform.Rotate(0, 0, spinSpeed / 100);
    }

}
