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

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _skills = GetComponent<SkillsControl>();
        _animator = GetComponent<Animator>();

        _inputs = new PlayerInputs();
        _movement = _inputs.Game.Movement;
        _movement.performed += MoveToCursor;
    }

    private void OnEnable()
    {
        _movement.Enable();
    }

    private void MoveToCursor(InputAction.CallbackContext callback)
    {
        _isRun = true;
        _agent.enabled = true;
        _agent.isStopped = false;
        _skills?.CancelAllSkills();
        _animator?.SetTrigger("move");
        _agent.SetDestination(SurfaceMouse.Position);
    }

    private void Update()
    {
        if (
            _isRun 
            && (transform.position - _agent.destination).sqrMagnitude 
                <= Mathf.Pow(_agent.stoppingDistance, 2)
        )
        {
            _isRun = false;
            _animator?.SetTrigger("idle");
        }
    }

    private void OnDisable()
    {
        _movement.Disable();
    }

    public override void Stop()
    {
        _agent.isStopped = true;
        _agent.enabled = false;
    }
}
