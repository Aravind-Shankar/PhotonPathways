using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RayGenerator))]
public class Reflector : RayReceiver
{
    private RayGenerator _rayGenerator;
    public bool isCounted;
   
    private void Awake()
    {
        _rayGenerator = GetComponent<RayGenerator>();
        isCounted = false;
    }

    public override void ReceiveRay(RayModel incidentRay, RaycastHit hit, RayGenerator rayGenerator, bool receiveFromInside = false)
    {
        base.ReceiveRay(incidentRay, hit, rayGenerator);
        _rayGenerator.CopyFrom(rayGenerator);

        Vector3 reflectedDir = Vector3.Reflect(incidentRay.Direction, hit.normal).normalized;
        var reflectedRay = _rayGenerator.GenerateRay(hit.point, reflectedDir, incidentRay.MediumRefractiveIndex);

        incidentRay.fullLengthAction += () => { reflectedRay.CanRender = true; isCounted = true;};
        incidentRay.deathAction += () => { isCounted = false; };
    }
}
