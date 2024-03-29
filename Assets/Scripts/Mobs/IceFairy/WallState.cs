using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WallState : IceFairyState
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private List<MovementControl> _controledUnits;
    private List<Collider> _ignoredColliders;
    private float _currentLifeTime;
    private Rigidbody _rigidbody;
    private Collider[] _colliders;

    public override void Init()
    {
        base.Init();

        _colliders = _view.GetComponents<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _ignoredColliders = new List<Collider>();
        _controledUnits = new List<MovementControl>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _currentLifeTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled == false || !Object.HasStateAuthority) return;

        if (collision.gameObject.TryGetComponent(out MovementControl control))
        {
            _controledUnits.Add(control);
            control.Rpc_Stop();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (enabled == false || !Object.HasStateAuthority) return;

        if (collision.gameObject.TryGetComponent(out MovementControl control))
        {
            _controledUnits.Remove(control);
            control.Activate();
        }
    }

    private void FixedUpdate()
    {
        if (!Object.HasStateAuthority) return;

        _currentLifeTime += Time.fixedDeltaTime;
        if (_currentLifeTime >= _lifeTime)
        {
            IceFairy.RPC_ChangeState(IceFairyStateID.Fairy);
        }

        _rigidbody.velocity = transform.forward * _speed;

        foreach (var unit in _controledUnits)
        {
            unit.Rpc_Stop();
        }
    }

    public override void BecomeWall(GameObject sender, Vector3 from, Vector3 to)
    {
        var senderCollider = sender.GetComponent<Collider>();
        
        _ignoredColliders.Add(senderCollider);
        foreach (var collider in _colliders)
        {
            Physics.IgnoreCollision(collider, senderCollider, true);
        }

        if (sender.TryGetComponent(out MovementControl control))
        {
            control.Activate();
        }
    }

    public override void BecomeShield(GameObject sender, Vector3 to) {}

    protected override void OnDisable()
    {
        if (Object.HasStateAuthority)
        {
            foreach (var ignoredCollider in _ignoredColliders)
            {
                foreach (var collider in _colliders)
                {
                    Physics.IgnoreCollision(collider, ignoredCollider, false);
                }
            }

            foreach (var unit in _controledUnits)
            {
                unit.Rpc_Stop();
            }

            _controledUnits.Clear();
            _ignoredColliders.Clear();
            _rigidbody.velocity = Vector3.zero;
        }

        base.OnDisable();
    }
}
