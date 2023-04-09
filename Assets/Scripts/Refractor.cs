using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RayGenerator))]
public class Refractor : RayReceiver
{
    private RayGenerator _rayGenerator;
    public float refractiveIndex;

    private void Awake()
    {
        _rayGenerator = GetComponent<RayGenerator>();
    }

    public override void ReceiveRay(RayModel incidentRay, RaycastHit hit, RayGenerator rayGenerator, bool receiveFromInside = false)
    {
        base.ReceiveRay(incidentRay, hit, rayGenerator);
        _rayGenerator.CopyFrom(rayGenerator);

        Vector3 outgoingDir;
        float outgoingRefractiveIndex;
        bool castFromInside = !receiveFromInside;
        Vector3 I = incidentRay.Direction;
        Vector3 N = hit.normal;

        float cos_theta_i = Vector3.Dot(-I, N);
        float n1 = incidentRay.MediumRefractiveIndex;
        float n2 = refractiveIndex;
        if (receiveFromInside)
        {
            n1 = refractiveIndex;
            n2 = 1.0f;  // outside == air
        }

        float n = n1 / n2;
        float k = 1f - n * n * (1f - cos_theta_i * cos_theta_i);

        if (k < 0)
        {
            // TIR
            outgoingDir = Vector3.Reflect(I, N).normalized;
            outgoingRefractiveIndex = n1;
            castFromInside = false;
        }
        else
        {
            // refraction
            outgoingDir = (n * I + (n * cos_theta_i - Mathf.Sqrt(k)) * N).normalized;
            outgoingRefractiveIndex = n2;
        }

        var outgoingRay = _rayGenerator.GenerateRay(hit.point, outgoingDir, outgoingRefractiveIndex,
            castFromInside);

        incidentRay.fullLengthAction += () => { outgoingRay.CanRender = true; };
    }
}
