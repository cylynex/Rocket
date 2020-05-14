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
