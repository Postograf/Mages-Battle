using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private CurMaxValue _health;
    [SerializeField] private CurMaxValue _mana;
    [SerializeField, Range(0, 100)] private int _critChance;
    [SerializeField, Range(0, 100)] private int _evadeChance;

    public float Health
    {
        get => _health.Current;
        protected set => _health.Current = value;
    }

    public float MaxHealth
    {
        get => _health.Max;
        private set => _health.Max = value;
    }

    public float Mana
    {
        get => _mana.Current;
        protected set => _mana.Current = value;
    }

    public float MaxMana
    {
        get => _mana.Max;
        private set => _mana.Max = value;
    }

    public int CritChance
    {
        get => _critChance;
        set => _critChance = Mathf.Min(100, value);
    }

    public int EvadeChance
    {
        get => _evadeChance;
        set => _evadeChance = Mathf.Min(100, value);
    }

    public event Action<float, float> HealthChanged;
    public event Action Died;
    public event Action<float, float> ManaChanged;

    private void Awake()
    {
        _health.Changed += NotifyHealthChanges;
        _mana.Changed += NotifyManaChanges;
    }

    public virtual void ApplyDamage(float damage, Vector3 from)
    {
        if (Random.Range(1, 101) > _evadeChance)
        {
            Health -= damage;
        }
    }

    public void AddHealth(float value, Addition addition)
    {
        _health.Add(value, addition);
    }

    public void AddMana(float value, Addition addition)
    {
        _mana.Add(value, addition);
    }

    public bool SkillCast(float cost)
    {
        if (CanCast(cost))
        {
            var spentMana = Mathf.Clamp(cost, 0, Mana);
            var spentHealth = Mathf.Clamp(cost - spentMana, 0, Health);
            Mana -= spentMana;
            Health -= spentHealth;
            return true;
        }

        return false;
    }

    public bool CanCast(float cost)
    {
        return Health + Mana >= cost;
    }

    protected virtual void NotifyHealthChanges(float current, float max)
    {
        HealthChanged?.Invoke(_health.Current, _health.Max);
        if (_health.Current <= 0)
        {
            Died?.Invoke();
        }
    }

    protected virtual void NotifyManaChanges(float current, float max)
    {
        ManaChanged?.Invoke(_mana.Current, _mana.Max);
    }
}
