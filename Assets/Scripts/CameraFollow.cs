using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float xDistance = 0f;
    [SerializeField] float yDistance = 0f;
    [SerializeField] float zDistance;

    void Update() {
        Vector3 tLoc = new Vector3((target.position.x + xDistance), (target.position.y + yDistance), (target.position.z - zDistance));
        transform.position = tLoc;
    }

}
