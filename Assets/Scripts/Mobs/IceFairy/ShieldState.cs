using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShieldState : IceFairyState
{
    private Rigidbody _rigidbody;
    private TemporaryStructure _defenceStructure;

    public override void Init()
    {
        base.Init();

        _rigidbody = GetComponent<Rigidbody>();
        _defenceStructure = _view.GetComponent<TemporaryStructure>();

        if (!Object.HasStateAuthority) return;

        if (
            _defenceStructure.TryGetComponent(out Collider collider1)
            && IceFairy.Owner.TryGetComponent(out Collider collider2)
        )
        {
            Physics.IgnoreCollision(collider1, collider2, true);
        }

        _defenceStructure.Died += () => IceFairy.RPC_ChangeState(IceFairyStateID.Fairy);
    }

    protected override void OnEnable()
    {
        _defenceStructure?.FullRecover();
        if (_rigidbody != null)
        _rigidbody.useGravity = true;

        base.OnEnable();
    }

    public override void BecomeShield(GameObject sender, Vector3 to)
    {
    }

    public override void BecomeWall(GameObject sender, Vector3 from, Vector3 to)
    {
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
    }
}
