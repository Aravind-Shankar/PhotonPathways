using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayModel : MonoBehaviour
{
    [System.Serializable]
    public struct Data
    {
        [ColorUsage(false, true)]
        public Color color;

        public float sizeFactor;
        public float speedFactor;

        public float lifetimeSeconds;
    }

    private bool _initialized;

    private Data _rayData;
    private Vector3 _rayOrigin;
    private Vector3 _rayEnd;
    private Vector3 _rayDirection;
    private float _rayLength;

    public Vector3 Origin => _rayOrigin;
    public Vector3 Direction => _rayDirection;

    public MeshRenderer rayEmissiveRenderer;

    public bool CanRender  { get;  set; }

    private float _currentTime;
    private bool _atFullLength;

    [HideInInspector]
    public UnityAction fullLengthAction;
    public UnityAction deathAction;

    private void Awake()
    {
        _initialized = false;
        CanRender = false;
        _atFullLength = false;
    }

    public void Initialize(Data data, Vector3 origin, Vector3 end)
    {
        _rayData = data;
        _rayOrigin = origin;
        _rayEnd = end;

        _rayDirection = end - origin;
        _rayLength = _rayDirection.magnitude;
        _rayDirection.Normalize();

        transform.position = origin;
        transform.localScale = new Vector3(_rayData.sizeFactor, _rayData.sizeFactor, 0.0f);

        Vector3 newForward = _rayDirection;
        Vector3 newRight = Vector3.Cross(newForward, Vector3.up).normalized;
        Vector3 newUp = Vector3.Cross(newRight, newForward).normalized;

        transform.rotation = Quaternion.LookRotation(newForward, newUp);

        rayEmissiveRenderer.material.SetColor("_EmissionColor", _rayData.color);

        _initialized = true;
        _currentTime = 0f;
    }

    private void Update()
    {
        if (!_initialized || !CanRender)
            return;

        _currentTime += Time.deltaTime;
        if (_currentTime >= _rayData.lifetimeSeconds)
        {
            Destroy(gameObject);
            if(deathAction != null) deathAction.Invoke();
        }

        if (_atFullLength)
            return;

        float scale = Mathf.Lerp(0f, _rayLength, _currentTime * _rayData.speedFactor / _rayLength);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scale);

        if (scale >= _rayLength)
        {
            _atFullLength = true;
            fullLengthAction.Invoke();
        }
    }
}
