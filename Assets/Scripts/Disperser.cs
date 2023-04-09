using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disperser : RayReceiver
{
    private RayGenerator _rayGenerator;
    //private float[] refractive_indices;
    private Color[] VIBGYOR;

    private void Awake()
    {
        _rayGenerator = GetComponent<RayGenerator>();
        VIBGYOR = new Color[7];

        VIBGYOR[0] = new Color(0.578f, 0f, 0.824f,0.2f);
        VIBGYOR[1] = new Color(0.293f, 0f, 0.508f, 0.2f);
        VIBGYOR[2] = new Color(0f, 0f, 1f, 0.2f);
        VIBGYOR[3] = new Color(0f, 1f, 0f, 0.2f);
        VIBGYOR[4] = new Color(1f, 1f, 0f, 0.2f);
        VIBGYOR[5] = new Color(0.996f, 0.496f, 0f, 0.2f);
        VIBGYOR[6] = new Color(1f, 0f, 0f, 0.2f);

    }

    public override void ReceiveRay(RayModel incidentRay, RaycastHit hit, RayGenerator rayGenerator)
    {
        base.ReceiveRay(incidentRay, hit, rayGenerator);
        _rayGenerator.CopyFrom(rayGenerator);

        float refractive_index = 1.7f;

        for (int i = 0; i < 7; i++)
        {
            Vector3 refractedDir = refractRay(incidentRay.Direction.normalized, hit.normal.normalized, refractive_index);
            _rayGenerator.rayData.color = VIBGYOR[i];
            var refractedRay = _rayGenerator.GenerateRay(hit.point, refractedDir);

            incidentRay.fullLengthAction += () => { refractedRay.CanRender = true; };
            refractive_index -= 0.05f;
        }
    }

    Vector3 refractRay(Vector3 I, Vector3 N, float refractive_index)
    {
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
            return Vector3.Reflect(I, -N).normalized;
        }

        return (n * I + (n * cos_theta_i - Mathf.Sqrt(k)) * N).normalized;
    }
}
