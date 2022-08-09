using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyState : IceFairyState
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minMoveHeight;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _distanceFromTargetForWall;
    [SerializeField] private float _heightForWall;
    [SerializeField] private float _heightForShield;

    private Rigidbody _rigidbody;
    private Sequence _sequence;
    private bool _isBecoming = false;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _isBecoming = false;
        if (_rigidbody != null)
            _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (_isBecoming == false)
        {
            var homePosition = IceFairy.Home.position;
            var fairyPosition = transform.position;
            var directionToHome = (homePosition - fairyPosition).normalized;
            transform.position += directionToHome * _speed * Time.deltaTime;
        }
    }

    public override void BecomeWall(GameObject sender, Vector3 from, Vector3 to)
    {
        _isBecoming = true;
        _sequence.Kill();
        var oldY = from.y;
        from += (from - to).normalized * _distanceFromTargetForWall;
        from.y = _minMoveHeight + oldY;
        to.y += _heightForWall;
        var durationToFrom = Vector3.Distance(from, transform.position) / _speed;
        var rotationDuration = Vector3.Distance(to, from) / _rotationSpeed;

        _sequence = DOTween.Sequence()
            .Append(transform.DOMove(from, durationToFrom).SetEase(Ease.Linear))
            .Append(transform.DOLookAt(to, rotationDuration).SetEase(Ease.Linear))
            .OnComplete(() => IceFairy.ChangeState(IceFairyStateID.Wall));
    }

    public override void BecomeShield(GameObject sender, Vector3 to)
    {
        _isBecoming = true;
        _sequence.Kill();
        var targetHeight = to + new Vector3(0, _heightForShield, 0);
        var durationToTo = Vector3.Distance(to, transform.position) / _speed;
        var durationToHeight = Vector3.Distance(to, targetHeight) / _speed;

        _sequence = DOTween.Sequence()
            .Append(transform.DOMove(to, durationToTo).SetEase(Ease.Linear))
            .Append(transform.DOMove(targetHeight, durationToHeight).SetEase(Ease.Linear))
            .OnComplete(() => IceFairy.ChangeState(IceFairyStateID.Shield));
    }

    protected override void OnDisable()
    {
        if (_rigidbody != null)
            _rigidbody.isKinematic = false;

        base.OnDisable();
    }
}
