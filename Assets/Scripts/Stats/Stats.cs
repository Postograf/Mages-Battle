using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Stats : MonoBehaviour, IDamageable
{
    [SerializeField] private CurMaxValue _health;
    [SerializeField] private CurMaxValue _mana;
    [SerializeField, Range(0, 100)] private int _critChance;
    [SerializeField, Range(0, 100)] private int _evadeChance;

    public float Health
    {
        get => _health.Current;
        protected set
        {
            _health.Current = value;
            NotifyHealthChanges();
        }
    }

    public float MaxHealth
    {
        get => _health.Max;
        private set
        {
            _health.Max = value;
            NotifyHealthChanges();
        }
    }

    public float Mana
    {
        get => _mana.Current;
        protected set
        {
            _mana.Current = value;
            NotifyManaChanges();
        }
    }

    public float MaxMana
    {
        get => _mana.Max;
        private set
        {
            _mana.Max = value;
            NotifyManaChanges();
        }
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
        NotifyHealthChanges();
    }

    public void AddMana(float value, Addition addition)
    {
        _mana.Add(value, addition);
        NotifyManaChanges();
    }

    public bool SkillCast(float cost)
    {
        if (Health + Mana >= cost)
        {
            var spentMana = Mathf.Clamp(cost, 0, Mana);
            var spentHealth = Mathf.Clamp(cost - spentMana, 0, Health);
            Mana -= spentMana;
            Health -= spentHealth;
            return true;
        }

        return false;
    }

    protected virtual void NotifyHealthChanges()
    {
        HealthChanged?.Invoke(_health.Current, _health.Max);
        if (_health.Current <= 0)
        {
            Died?.Invoke();
        }
    }

    protected virtual void NotifyManaChanges()
    {
        ManaChanged?.Invoke(_mana.Current, _mana.Max);
    }
}
