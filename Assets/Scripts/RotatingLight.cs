using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLight : MonoBehaviour {

    [SerializeField] Vector3 rotationVector;

    private void Start() {
        print(transform.rotation);
    }

    private void Update() {
        transform.Rotate(Vector3.up);
    }

}
