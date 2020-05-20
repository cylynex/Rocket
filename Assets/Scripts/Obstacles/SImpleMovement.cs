/* Simple Movement v1
 * 
 * What it Does:
 * Lets an object move Side to side, up and down, or diagonal.
 * 
 * Variables:
 * Movement Vector: The X and Y values you want it to traverse.  Just set the amount you want it to move here and it will move along that axis that amount.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SimpleMovement : MonoBehaviour {

    [Header("Setup Values")]
    [SerializeField] Vector2 movementVector;
    public Vector2 startingPosition;
    [Range(0, 1)] [SerializeField] float movementFactor;


    public bool direction = true;
    [SerializeField] float moveAmount = .005f;

    private void Start() {
        startingPosition = transform.position;
        if (direction == false) {
            movementFactor = 1;
        }
    }

    void Update() {

        // Initial Attempt
        if (direction == true) {
            MoveUp();
        }
        else {
            MoveDown();
        }
        Move();
    }

    void MoveUp() {
        if (movementFactor < 1) {
            movementFactor = movementFactor + (moveAmount / 1000);
        }
        else {
            movementFactor = movementFactor - (moveAmount / 1000);
            direction = false;
        }
    }

    void MoveDown() {
        if (movementFactor < 0) {
            movementFactor = movementFactor + (moveAmount / 1000);
            direction = true;
        }
        else {
            movementFactor = movementFactor - (moveAmount / 1000);
        }
    }

    void Move() {
        Vector2 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}