using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _scaleByDamage;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private float _damage;
    public float Damage
    {
        get => _damage;
        set
        {
            _damage = value;
            transform.localScale = Vector3.one * _scaleByDamage * _damage;
        }
    }

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
