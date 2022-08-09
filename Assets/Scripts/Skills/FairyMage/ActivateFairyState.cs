using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IceFairySpawner))]
public class ActivateFairyState : Skill
{
    [SerializeField] private IceFairyStateID _activatingState;
    private IceFairy _iceFairy;
    private bool _isActive;

    private void Start()
    {
        _iceFairy = GetComponent<IceFairySpawner>().IceFairy;
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
            _iceFairy?.BecomeShield(gameObject, SurfaceMouse.Position);
        }
        else if (_activatingState == IceFairyStateID.Wall)
        {
            _iceFairy?.BecomeWall(gameObject, transform.position, SurfaceMouse.Position);
        }

        Phase = SkillPhase.Ready;
    }

    protected override void Update()
    {
        base.Update();

        if (_iceFairy.State == _activatingState)
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
    public override bool Unpress()
    {
        NotifyButtonUnpressed();
        return true;
    }
}