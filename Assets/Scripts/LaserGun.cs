using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : RayGenerator
{
    public Transform firingPointTransform;
    public float cooldownSeconds;

    public bool DEBUG_FireTrigger = false;

    private float _lastFireTime;
    
    void Start()
    {
        _lastFireTime = Time.timeSinceLevelLoad;
        DEBUG_FireTrigger = false;
    }

    private void Update()
    {
        if (DEBUG_FireTrigger)
        {
            DEBUG_FireTrigger = false;
            Fire();
        }
    }


    public void Fire()
    {
        if (Time.timeSinceLevelLoad - _lastFireTime < cooldownSeconds)
        {
            Debug.Log($"Cooling down; can't fire.");
            return;
        }

        _lastFireTime = Time.timeSinceLevelLoad;

        var ray = GenerateRay(firingPointTransform.position, firingPointTransform.forward);
        ray.CanRender = true;
    }
}
