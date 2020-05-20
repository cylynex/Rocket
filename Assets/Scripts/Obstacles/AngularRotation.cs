using UnityEngine;
using System.Collections;

public class AngularRotation : MonoBehaviour
{
    [SerializeField]
    private float tumble;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
}