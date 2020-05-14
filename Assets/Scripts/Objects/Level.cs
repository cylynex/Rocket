using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public string levelName;
    [TextArea(10, 10)]
    public string levelDescription;
    public float startingFuel;
    public bool fuelConsumption = false;
    public float fuelConsumptionRate;
    public float maxLandVelocity;
}
