using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableObject : MonoBehaviour
{

    private bool _isCarried = false;

    private bool _canBeCarried = true;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private bool _isInit = false;

    private void Init()
    {
        if (_isInit) return;

        _isInit = true;

        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public bool GetCanBeTaken()
    {
        return !_isCarried && _canBeCarried;
    }

    public bool GetTaken()
    {
        return _isCarried;
    }

    public void Take()
    {
        Init();
        _rigidbody.useGravity = false;
        _isCarried = true;
        _collider.isTrigger = true;
    }

    public void Release()
    {
        _rigidbody.useGravity = true;
        _isCarried = false;
        _collider.isTrigger = false;
    }
}
