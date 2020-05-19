using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [SerializeField] float rotateSpeed;
    [SerializeField] Vector3 rotateVector;

    private void Update() {
        transform.Rotate(rotateVector * Time.deltaTime);
    }

}
