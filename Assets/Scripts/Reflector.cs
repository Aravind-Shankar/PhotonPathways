using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RayGenerator))]
public class Reflector : RayReceiver
{
    private RayGenerator _rayGenerator;
    
    private void Awake()
    {
        _rayGenerator = GetComponent<RayGenerator>();
    }

    public override void ReceiveRay(RayModel incidentRay, RaycastHit hit, RayGenerator rayGenerator)
    {
        base.ReceiveRay(incidentRay, hit, rayGenerator);
        _rayGenerator.CopyFrom(rayGenerator);

        Vector3 reflectedDir = Vector3.Reflect(incidentRay.Direction, hit.normal).normalized;
        var reflectedRay = _rayGenerator.GenerateRay(hit.point, reflectedDir);
        incidentRay.fullLengthAction += () => { reflectedRay.CanRender = true; };
    }
}
