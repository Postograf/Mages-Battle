using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillPhase
{
    Ready,
    WaitingDelay,
    Activated,
    Holding,
    Ended,
    InCooldown
}

[RequireComponent(typeof(Stats))]
public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _description;
    [SerializeField] protected float _manaCost;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected float _delay;
    [SerializeField] protected bool _cancellable;

    protected Stats _stats;
    protected float _pastDelay;
    protected float _currentCooldown;

    public string Name => _name;
    public Sprite Icon => _icon;
    public string Description => _description;
    public float ManaCost => _manaCost;
    public float Cooldown => _cooldown;
    public float Delay => _delay;
    public bool Cancellable => _cancellable;

    public SkillPhase Phase { get; protected set; }

    private void Awake()
    {
        _stats = GetComponent<Stats>();
    }

    public virtual bool Press()
    {
        if (Phase == SkillPhase.Ready && _stats.SkillCast(_manaCost))
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

            return true;
        }

        return false;
    }

    protected virtual void Activate()
    {
        Phase = SkillPhase.Holding;
    }

    protected virtual void Hold(float deltaTime)
    {
        Phase = SkillPhase.Ended;
        End();
    }

    public virtual bool Unpress()
    {
        if (Phase != SkillPhase.Ready && Phase != SkillPhase.InCooldown)
        {
            Phase = SkillPhase.Ended;
            End();
            return true;
        }

        return false;
    }

    protected virtual void End()
    {
        StartCooldown();
    }

    protected void StartCooldown()
    {
        if (_cooldown > 0)
        {
            _currentCooldown = _cooldown;
            Phase = SkillPhase.InCooldown;
        } else
        {
            Phase = SkillPhase.Ready;
        }
    }

    protected virtual void Update()
    {
        if (Phase == SkillPhase.InCooldown)
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0)
            {
                Phase = SkillPhase.Ready;
            }
        }
        else if (Phase == SkillPhase.WaitingDelay)
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
            Phase = SkillPhase.Ready;
        }
    }
}
