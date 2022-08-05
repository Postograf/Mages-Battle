using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillPhase
{
    Deactivated,
    WaitingDelay,
    Activated,
    Holding,
    Ended
}

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected Image _icon;
    [SerializeField] protected string _description;
    [SerializeField] protected int _manaCost;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected float _delay;
    [SerializeField] protected bool _cancellable;

    protected float _pastDelay;

    public string Name => _name;
    public Image Icon => _icon;
    public string Description => _description;
    public int ManaCost => _manaCost;
    public float Cooldown => _cooldown;
    public float Delay => _delay;
    public bool Cancellable => _cancellable;

    public SkillPhase Phase { get; protected set; }

    public virtual void WaitDelay()
    {
        if (_delay <= 0)
        {
            Phase = SkillPhase.Activated;
            Activate();
        }
        else
        {
            Phase = SkillPhase.WaitingDelay;
            _pastDelay = 0f;
        }
    }

    public virtual void Activate()
    {
        Phase = SkillPhase.Holding;
    }

    protected abstract void Hold(float deltaTime);

    public virtual void End()
    {
        Phase = SkillPhase.Deactivated;
    }

    protected virtual void Update()
    {
        if (Phase == SkillPhase.WaitingDelay)
        {
            _pastDelay += Time.deltaTime;
            if (_pastDelay >= _delay)
            {
                Phase = SkillPhase.Activated;
                Activate();
            }
        }
        else if (Phase == SkillPhase.Holding)
        {
            Hold(Time.deltaTime);
        }
    }

    public virtual void Cancel()
    {
        if (_cancellable)
        {
            Phase = SkillPhase.Deactivated;
        }
    }
}
