using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IceFairySpawner))]
public class ActivateFairyShield : Skill
{
    private IceFairy _iceFairy;

    private void Start()
    {
        _iceFairy = GetComponent<IceFairySpawner>().IceFairy;
    }

    protected override void Activate()
    {
        _iceFairy?.BecomeShield(gameObject, SurfaceMouse.Position);
        StartCooldown();
    }

    protected override void End() {}
    public override bool Unpress() => true;
}