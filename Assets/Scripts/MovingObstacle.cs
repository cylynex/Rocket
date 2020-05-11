using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MovingObstacle : MonoBehaviour {

    [SerializeField] Vector2 movementVector;
    [Range(0, 1)] [SerializeField] float movementFactor;

    Vector2 startingPosition;
    bool direction = true;
    [SerializeField] float moveAmount = .005f;

    private void Start() {
        startingPosition = transform.position;
    }

    void Update() {

        // Initial Attempt
        if (direction == true) {
            MoveRight();
        } else {
            MoveLeft();
        }

        Move();
    }

    void MoveRight() {
        if (movementFactor < 1) {
            movementFactor = movementFactor + moveAmount;
        } else {
            movementFactor = movementFactor - moveAmount;
            direction = false;
        }
    }

    void MoveLeft() {
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