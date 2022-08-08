using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(IceFairySpawner))]
public class ActivateFairyWall : Skill
{
    private IceFairy _iceFairy;

    private void Start()
    {
        _iceFairy = GetComponent<IceFairySpawner>().IceFairy;
    }

    protected override void Activate()
    {
        _iceFairy?.BecomeWall(gameObject, transform.position, SurfaceMouse.Position);
        StartCooldown();
    }

    protected override void End() { }
    public override bool Unpress() => true;
}
