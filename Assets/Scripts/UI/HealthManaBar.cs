using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManaBar : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _manaBar;

    public Unit Unit { get => _unit; set => _unit = value; }

    private void Awake()
    {
        _unit.HealthChanged += ChangeHealthBar;
        _unit.ManaChanged += ChangeManaBar;
        _unit.Died += () => gameObject.SetActive(false);
    }

    private void ChangeHealthBar(float current, float max)
    {
        _healthBar.fillAmount = current / max;
    }

    private void ChangeManaBar(float current, float max)
    {
        _manaBar.fillAmount = current / max;
    }
}
