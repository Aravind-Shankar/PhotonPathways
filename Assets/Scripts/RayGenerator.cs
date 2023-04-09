using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGenerator : MonoBehaviour
{
    public RayModel rayModelPrefab;
    public RayModel.Data rayData;
    public bool isCounted;

    const float MAX_RAY_DISTANCE = 100f;

    public void CopyFrom(RayGenerator other)
    {
        rayModelPrefab = other.rayModelPrefab;
        rayData = other.rayData;
    }

    public RayModel GenerateRay(Vector3 origin, Vector3 direction)
    {
        RayModel newRay = Instantiate(rayModelPrefab);
        isCounted = false;
        if (Physics.Raycast(origin, direction, out var hit, MAX_RAY_DISTANCE))
        {
            newRay.Initialize(rayData, origin, hit.point);
            if (hit.collider.gameObject.TryGetComponent<RayReceiver>(out var receiver))
                receiver.ReceiveRay(newRay, hit, this);
        }
        else
            newRay.Initialize(rayData, origin, origin + direction * MAX_RAY_DISTANCE);

        return newRay;
    }
}
