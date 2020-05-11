using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MovingObstacle : MonoBehaviour {

    [Header("Setup Values")]
    [SerializeField] Vector2 movementVector;
    public Vector2 startingPosition;
    [Range(0, 1)] [SerializeField] float movementFactor;

    
    public bool direction = true;
    [SerializeField] float moveAmount = .005f;

    private void Start() {
        startingPosition = transform.position;
        print("start direction for me: " + gameObject.name + ": " + direction);
        if(direction == false) {
            movementFactor = 1;
        }
    }

    void Update() {

        // Initial Attempt
        if (direction == true) {
            MoveUp();
        } else {
            MoveDown();
        }
        Move();
    }

    void MoveUp() {
        if (movementFactor < 1) {
            movementFactor = movementFactor + moveAmount;
        } else {
            movementFactor = movementFactor - moveAmount;
            direction = false;
        }
    }

    void MoveDown() {
        if (movementFactor < 0) {
            movementFactor = movementFactor + moveAmount;
            direction = true;
        } else {
            movementFactor = movementFactor - moveAmount;
        }
    }

    void Move() {
        Vector2 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}