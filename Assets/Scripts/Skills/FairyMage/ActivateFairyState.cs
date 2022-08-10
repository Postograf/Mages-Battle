using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IceFairySpawner))]
public class ActivateFairyState : Skill
{
    [SerializeField] private IceFairyStateID _activatingState;
    private IceFairySpawner _iceFairySpawner;
    private bool _isActive;

    private void Start()
    {
        _iceFairySpawner = GetComponent<IceFairySpawner>();
    }

    public override bool Press()
    {
        if (Phase == SkillPhase.Ready && _unit.CanCast(_manaCost))
        {
            Phase = SkillPhase.Activated;
            Activate();
            NotifyButtonPressed();
            return true;
        }

        return false;
    }

    protected override void Activate()
    {
        if (_activatingState == IceFairyStateID.Shield)
        {
            _iceFairySpawner.IceFairy?.BecomeShield(gameObject, SurfaceMouse.Position);
        }
        else if (_activatingState == IceFairyStateID.Wall)
        {
            _iceFairySpawner.IceFairy?.BecomeWall(gameObject, transform.position, SurfaceMouse.Position);
        }

        Phase = SkillPhase.Ready;
        NotifyButtonUnpressed();
    }

    protected override void Update()
    {
        if (!Object.HasInputAuthority) return;

        base.Update();

        if (_iceFairySpawner.IceFairy.State == _activatingState)
        {
            if (_isActive == false)
            {
                _unit.SkillCast(_manaCost);
                _isActive = true;
            }
        }
        else if (_isActive)
        {
            _isActive = false;
            StartCooldown();
        }
    }

    protected override void End() {}
    public override bool Unpress() => true;
}