using Fusion;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovementControl : MovementControl
{
    private PlayerInputs _inputs;
    private InputAction _movement;

    private NavMeshAgent _agent;
    private SkillsControl _skills;
    private Animator _animator;

    private bool _isRun;
    private float _sqrAgentStopping;

    private const string _moveProperty = "move";
    private const string _idleProperty = "idle";

    protected override void Awake()
    {
        base.Awake();

        _agent = GetComponent<NavMeshAgent>();
        _skills = GetComponent<SkillsControl>();
        _animator = GetComponent<Animator>();

        _inputs = new PlayerInputs();
        _movement = _inputs.Game.Movement;
        _movement.performed += MoveToCursor;

        _sqrAgentStopping = _agent.stoppingDistance * _agent.stoppingDistance;

        Activate();
    }

    private void OnEnable()
    {
        Activate();
        _movement.Enable();
    }

    private void MoveToCursor(InputAction.CallbackContext callback)
    {
        _isRun = true;
        Activate();
        _skills?.CancelAllSkills();
        _animator?.SetTrigger(_moveProperty);
        _agent.SetDestination(SurfaceMouse.Position);
    }

    private void Update()
    {
        var sqrDistanceToTarget = (transform.position - _agent.destination).sqrMagnitude;
        if (_isRun && sqrDistanceToTarget <= _sqrAgentStopping)
        {
            _isRun = false;
            _animator?.SetTrigger(_idleProperty);
        }
    }

    private void OnDisable()
    {
        Rpc_Stop();
        _movement.Disable();
    }

    public override void Activate()
    {
        _agent.enabled = true;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
    }

    [Rpc]
    public override void Rpc_Stop()
    {
        _isRun = false;
        _agent.enabled = false;
        _rigidbody.isKinematic = false;
    }
}
