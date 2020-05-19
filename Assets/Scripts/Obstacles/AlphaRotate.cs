using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaRotate : MonoBehaviour {

    [SerializeField] float adjustAmount = .05f;
    Color objectColor;

    private void FixedUpdate() {
        objectColor = GetComponent<MeshRenderer>().material.color;
        objectColor.a -= adjustAmount;
        GetComponent<MeshRenderer>().material.color = objectColor;
    }   
}