using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotate : MonoBehaviour
{
    public float rotationSpeed = 10f; // set the rotation speed in degrees per second

    void Update()
    {
        // Rotate the object around its Y-axis at a constant speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
