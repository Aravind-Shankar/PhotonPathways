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

    public RayModel GenerateRay(Vector3 origin, Vector3 direction, float mediumRefractiveIndex, bool castFromInside = false)
    {
        isCounted = false;

        RayModel newRay = Instantiate(rayModelPrefab);

        RaycastHit hit = default;
        bool foundHit;
        bool receiveFromInside = castFromInside;
        if (castFromInside)
        {
            foundHit = false;
            var reversedHits = Physics.RaycastAll(origin + direction * MAX_RAY_DISTANCE, -direction, MAX_RAY_DISTANCE);
            foreach (var reversedHit in reversedHits)
                if (reversedHit.collider.gameObject == gameObject &&
                    !Mathf.Approximately(Vector3.Distance(reversedHit.point, origin), 0f))
                {
                    // hit on the same object, at different point
                    hit = reversedHit;
                    hit.normal *= -1f;
                    foundHit = true;
                    break;
                }

            // forward hit: be robust to self-hits by moving further from the origin
            if (Physics.Raycast(origin + direction * 1e-3f, direction, out var forwardHit, MAX_RAY_DISTANCE))
            {
                if (!foundHit || Vector3.Distance(origin, hit.point) > Vector3.Distance(origin, forwardHit.point))
                {
                    // found another nearer collider instead of the inside-object itself, so ignore the inside-case
                    receiveFromInside = false;
                    hit = forwardHit;
                }
            }
        }
        else
        {
            // forward hit: be robust to self-hits by moving further from the origin
            foundHit = Physics.Raycast(origin + direction * 1e-3f, direction, out hit, MAX_RAY_DISTANCE);
        }
        if (foundHit)
        {
            newRay.Initialize(rayData, origin, hit.point, mediumRefractiveIndex);
            if (hit.collider.gameObject.TryGetComponent<RayReceiver>(out var receiver))
                receiver.ReceiveRay(newRay, hit, this, receiveFromInside);
        }
        else
            newRay.Initialize(rayData, origin, origin + direction * MAX_RAY_DISTANCE, mediumRefractiveIndex);

        return newRay;
    }
}
