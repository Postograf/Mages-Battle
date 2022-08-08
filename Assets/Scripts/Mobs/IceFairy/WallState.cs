using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WallState : IceFairyState
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private List<Collider> _ignoredColliders;
    private float _currentLifeTime;
    private Rigidbody _rigidbody;
    private Collider[] _colliders;

    protected override void Awake()
    {
        base.Awake();

        _colliders = _view.GetComponents<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _ignoredColliders = new List<Collider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _currentLifeTime = 0;
        _rigidbody.useGravity = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (enabled == false) return;

        if (collision.gameObject.TryGetComponent(out MovementControl control))
        {
            control.Stop();
        }
    }

    private void FixedUpdate()
    {
        _currentLifeTime += Time.fixedDeltaTime;
        if (_currentLifeTime >= _lifeTime)
        {
            IceFairy.ChangeState(IceFairyStateID.Fairy);
        }

        _rigidbody.velocity = transform.forward * _speed;
    }

    public override void BecomeWall(GameObject sender, Vector3 from, Vector3 to)
    {
        var senderCollider = sender.GetComponent<Collider>();
        
        _ignoredColliders.Add(senderCollider);
        foreach (var collider in _colliders)
        {
            Physics.IgnoreCollision(collider, senderCollider, true);
        }
    }

    public override void BecomeShield(GameObject sender, Vector3 to) {}

    protected override void OnDisable()
    {
        foreach (var ignoredCollider in _ignoredColliders)
        {
            foreach (var collider in _colliders)
            {
                Physics.IgnoreCollision(collider, ignoredCollider, false);
            }
        }

        _ignoredColliders.Clear();
        _rigidbody.useGravity = false;

        base.OnDisable();
    }
}
