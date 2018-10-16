using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingRotation : MonoBehaviour 
{

    private Vector2 _lastVelocity = Vector2.zero;

    private Transform _transform;

    private float _lastAngle = 0f;

    private float _speedRotation = 20.0f;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

	public void SetRotation(Vector3 velocity)
    {
        float angle = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, _lastAngle, 0), Time.deltaTime * _speedRotation);
        
        if (velocity.x == 0 && velocity.z == 0) return;
        
        _lastVelocity = new Vector2(velocity.x, velocity.z);

        angle = Mathf.Atan2(_lastVelocity.y, -_lastVelocity.x) * 180 / Mathf.PI - 90.0f;

        _lastAngle = angle;
    }
}
