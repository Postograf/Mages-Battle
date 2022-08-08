using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyState : IceFairyState
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minMoveHeight;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private float _distanceFromTargetForWall;
    [SerializeField] private float _heightForShield;

    private bool _isBecoming = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        _isBecoming = false;
    }

    private void Update()
    {
        if (_isBecoming == false)
        {
            var position = transform.position;
            var homePosition = IceFairy.Home.position;
            transform.position = Vector3.Lerp(position, homePosition, _speed * Time.deltaTime);
        }
    }

    public async override void BecomeWall(GameObject sender, Vector3 from, Vector3 to)
    {
        _isBecoming = true;
        transform.DOKill();
        var oldY = from.y;
        from += (from - to).normalized * _distanceFromTargetForWall;
        from.y = _minMoveHeight + oldY;
        to.y += _minMoveHeight;
        var duration = Vector3.Distance(transform.position, from) / _speed;
        await transform.DOMove(from, duration).AsyncWaitForCompletion();
        await transform.DOLookAt(to, _rotationDuration).AsyncWaitForCompletion();
        IceFairy.ChangeState(IceFairyStateID.Wall);
    }

    public async override void BecomeShield(GameObject sender, Vector3 to)
    {
        _isBecoming = true;
        transform.DOKill();
        var durationToTarget = Vector3.Distance(transform.position, to) / _speed;
        var targetHeight = to + new Vector3(0, _heightForShield, 0);
        var durationToHeight = Vector3.Distance(to, targetHeight) / _speed;
        await transform.DOMove(to, durationToTarget).AsyncWaitForCompletion();
        await transform.DOMove(targetHeight, durationToHeight).AsyncWaitForCompletion();
        IceFairy.ChangeState(IceFairyStateID.Shield);
    }
}
