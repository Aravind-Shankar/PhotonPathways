using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 direction;
    public GameObject laser;
    List<GameObject> beams;
    public int THRESHOLD;
    float currentTime;
    float originalScale;
    bool isCurrBeamRendered;
    int currBeamIndex;

    Vector3 origin_cache;
    Vector3 direction_cache;
    Vector3 end_cache;

    int path_depth;
    bool isPathEnd;
    public float speedFactor;

    void Start()
    {
        beams = new List<GameObject>();
        currentTime = 0.0f;
        originalScale = 0.0f;
        isCurrBeamRendered = true;
        currBeamIndex = -1;
        path_depth = 0;
        origin_cache = origin;
        direction_cache = direction;
        end_cache = origin_cache;
        isPathEnd = false;
    }

    void Update()
    {
        if (isCurrBeamRendered)
        {
            origin_cache = end_cache;
            if (!isPathEnd)
            {
                CastRay(origin_cache, direction_cache, path_depth++);
                generateBeam(origin_cache, end_cache);
            }
        }
        else generateBeam(origin_cache, end_cache);
    }

    void CastRay(Vector3 origin_, Vector3 direction_, int depth)
    {
        if(depth > THRESHOLD)
        {
            return;
        }

        RaycastHit hit;
        origin_cache = origin_;
        direction_cache = direction_;

        if (Physics.Raycast(origin_, direction_, out hit, Mathf.Infinity))
        {
            end_cache = hit.point;
            direction_cache = Vector3.Reflect(direction_, hit.normal).normalized;
        }
        else
        {
            end_cache = origin_ + direction_ * 100f;
            isPathEnd = true;
        }
    }

    void generateBeam(Vector3 origin, Vector3 end)
    {
        GameObject laser_;
        if (isCurrBeamRendered)
        {
            isCurrBeamRendered = false;
            laser_ = Instantiate(laser, origin, Quaternion.identity);
            beams.Add(laser_);
            currBeamIndex++;
            currentTime = 0.0f;
        }
        else
        {
            laser_ = beams[currBeamIndex];
        }

        Vector3 ray_direction = end - origin;
        float destinationScale = ray_direction.magnitude;
        
        float scale = Mathf.Lerp(0f, destinationScale, currentTime* speedFactor / destinationScale);

        if (scale >= destinationScale)
        {
            isCurrBeamRendered = true;
        }

        currentTime += Time.deltaTime;

        laser_.transform.position = origin;
        laser_.transform.localScale = new Vector3(1f, 1f, scale);

        Vector3 Y = new Vector3(0f, 1f, 0f);
        Vector3 newForward = ray_direction.normalized;
        Vector3 newRight = Vector3.Cross(newForward, Y).normalized;
        Vector3 newUp = Vector3.Cross(newRight, newForward).normalized;

        laser_.transform.rotation = Quaternion.LookRotation(newForward, newUp);
        
    }
}

