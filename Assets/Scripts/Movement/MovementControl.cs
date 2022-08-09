using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class MovementControl : MonoBehaviour
{
    protected Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public abstract void Activate();
    public abstract void Stop();
}