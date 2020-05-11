using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarController : MonoBehaviour {

    [SerializeField] Slider fuelBar;
    
    public void AdjustFuel(float currentFuel) {
        fuelBar.value = currentFuel;
    }

    
}
