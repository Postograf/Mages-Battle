using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(IceFairy))]
public abstract class IceFairyState : MonoBehaviour
{
    [SerializeField] protected GameObject _view;
    public IceFairy IceFairy { get; private set; }

    protected virtual void Awake()
    {
        IceFairy = GetComponent<IceFairy>();
    }

    protected virtual void OnEnable()
    {
        _view.SetActive(true);
    }

    public abstract void BecomeWall(GameObject sender, Vector3 from, Vector3 to);

    public abstract void BecomeShield(GameObject sender, Vector3 to);

    protected virtual void OnDisable()
    {
        _view.SetActive(false);
    }
}