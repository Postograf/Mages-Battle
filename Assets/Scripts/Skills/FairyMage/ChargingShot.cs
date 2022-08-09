using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingShot : Skill
{
    [SerializeField] private float _manaCostInHold;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _damageCurve;
    [SerializeField] private float _scaleByDamage;

    private Projectile _spawnedProjectile;
    private float _elapsedTime;

    protected override void Activate()
    {
        _elapsedTime = 0;
        _spawnedProjectile = Instantiate(_projectile, _firePoint.position, Quaternion.identity);

        Phase = SkillPhase.Holding;
    }

    protected override void Hold(float deltaTime)
    {
        if (_unit.SkillCast(_manaCostInHold * deltaTime))
        {
            _elapsedTime += deltaTime;

            _spawnedProjectile.Damage = _damageCurve.Evaluate(_elapsedTime);
            _spawnedProjectile.transform.localScale =
                Vector3.one * _scaleByDamage * _spawnedProjectile.Damage;

            var target = SurfaceMouse.Position;
            target.y = transform.position.y;
            transform.LookAt(target);

            _spawnedProjectile.transform.position = _firePoint.position;
        }
    }

    protected override void End()
    {
        base.End();
        _spawnedProjectile.Speed = _speed;
        var target = SurfaceMouse.Position;
        target.y = _spawnedProjectile.transform.position.y;
        _spawnedProjectile.Launch(target - _spawnedProjectile.transform.position);
    }

    public override void Cancel()
    {
        if (_cancellable)
        {
            _spawnedProjectile.Destroy();
            Phase = SkillPhase.Ready;
            NotifyButtonUnpressed();
        }
    }
}
