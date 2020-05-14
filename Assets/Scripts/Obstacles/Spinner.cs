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
