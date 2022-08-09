using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShieldState : IceFairyState
{
    private Rigidbody _rigidbody;
    private TemporaryStructure _defenceStructure;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
        _defenceStructure = _view.GetComponent<TemporaryStructure>();
        _defenceStructure.Died += () => IceFairy.ChangeState(IceFairyStateID.Fairy);
    }

    protected override void OnEnable()
    {
        _defenceStructure.FullRecover();

        base.OnEnable();

        _rigidbody.useGravity = true;
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
