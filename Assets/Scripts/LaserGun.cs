using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserGun : RayGenerator
{
    public Transform firingPointTransform;
    public float cooldownSeconds;

    public bool DEBUG_FireTrigger = false;

    public InputAction triggerAction;
    private float _lastFireTime;

    void OnEnable()
    {
        triggerAction.Enable();
        _lastFireTime = Time.timeSinceLevelLoad;
        DEBUG_FireTrigger = false;
    }

    private void Update()
    {
        if (DEBUG_FireTrigger || triggerAction.triggered)
        {
            DEBUG_FireTrigger = false;
            Fire();
        }
    }


    public void Fire()
    {
        Debug.Log("ENTERED FIRE");
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
