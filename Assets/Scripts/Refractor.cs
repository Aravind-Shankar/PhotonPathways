using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RayGenerator))]
public class Refractor : RayReceiver
{
    private RayGenerator _rayGenerator;
    public float refractive_index;

    private void Awake()
    {
        _rayGenerator = GetComponent<RayGenerator>();
    }

    public override void ReceiveRay(RayModel incidentRay, RaycastHit hit, RayGenerator rayGenerator)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        sphere.transform.position = hit.point;

        base.ReceiveRay(incidentRay, hit, rayGenerator);
        _rayGenerator.CopyFrom(rayGenerator);

        Vector3 refractedDir;
        Vector3 I = incidentRay.Direction;
        Vector3 N = hit.normal;

        float cos_theta_i = Mathf.Clamp(-1, 1, Vector3.Dot(I, N));
        float n1 = 1;
        float n2 = refractive_index;

        if (cos_theta_i < 0)
        {
            cos_theta_i = -cos_theta_i;
        }
        else
        {
            float temp = n1;
            n1 = n2;
            n2 = temp;
            N = -N;
        }

        float n = n1 / n2;
        float k = 1 - n * n * (1 - cos_theta_i * cos_theta_i);

        if (k < 0)
        {
            refractedDir = Vector3.Reflect(I, -N).normalized;
        }

        refractedDir = (n * I + (n * cos_theta_i - Mathf.Sqrt(k)) * N).normalized;

        var refractedRay = _rayGenerator.GenerateRay(hit.point, refractedDir);
        incidentRay.fullLengthAction += () => { refractedRay.CanRender = true; };
    }
}
