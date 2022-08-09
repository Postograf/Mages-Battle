using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour
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
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(Damage, transform.position);
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
        Destroy(gameObject);
    }
}
