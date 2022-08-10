using Fusion;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : NetworkBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    public float Damage { get; set; }

    public float Speed { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Object.HasStateAuthority) return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.RPC_ApplyDamage(Damage, transform.position);
        }

        Destroy();
    }

    public void Launch(Vector3 direction)
    {
        _rigidbody.velocity = direction * Speed;
        _collider.isTrigger = false;
    }

    public void Destroy()
    {
        Runner.Despawn(Object);
    }
}
