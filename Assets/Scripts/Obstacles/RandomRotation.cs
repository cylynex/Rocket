/* Random Rotation v1
 * 
 * What It Does:
 * Allows an object to rotate randomly on the X and Y axis
 * 
 * Variables:
 * Rotation Min and Max set the range you want the object to rotate on each axis.  It will then select numbers for the
 * X and Y and rotate the object on those values
 * 
 * Rotation Speed - The Speed the object will rotate
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] Vector3 rotationVectorMin = new Vector3(-3, 3, 3);
    [SerializeField] Vector3 rotationVectorMax = new Vector3(-3, 3, 3);
    [SerializeField] Vector2 rotationSpeedRange = new Vector2(.05f, 1f);

    [Header("Derived Values")]
    [SerializeField] Vector3 rotationVector;
    [SerializeField] float rotationSpeed;

    private void Start() {
        rotationVector.x = Random.Range(rotationVectorMin.x, rotationVectorMax.x);
        rotationVector.y = Random.Range(rotationVectorMin.x, rotationVectorMax.y);
        rotationVector.z = Random.Range(rotationVectorMin.x, rotationVectorMax.z);
        rotationSpeed = Random.Range(rotationSpeedRange.x, rotationSpeedRange.y);
    }


    private void Update() {
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }

}
