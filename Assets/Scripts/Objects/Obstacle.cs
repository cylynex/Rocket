using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Obstacle",menuName ="Obstacle")]
public class Obstacle : ScriptableObject {

    //public Vector2 startingPosition;
    public GameObject obstacleObject;
    public bool movable;
    public bool startingDirection = false;

}
