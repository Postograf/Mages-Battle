using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;

public enum StatType
{
    Health,
    Mana,
    CritChance,
    EvadeChance
}

[Serializable]
public class StatTypeValueDictionary : SerializableDictionary<StatType, PlusMinusValue> { }

public class PointsDistributor : MonoBehaviour
{
    [SerializeField] private StatTypeValueDictionary _plusMinusValues;
    [SerializeField] private int _points;
    [SerializeField] private int _maxPoints;
    [SerializeField] private TMP_Text _pointsText;

    public StatTypeValueDictionary Values => _plusMinusValues;

    public int Points
    {
        get => _points;
        private set
        {
            if (_points == 0 && value > 0)
            {
                SetActivePlusButtons(true);
            }

            _points = Mathf.Clamp(value, 0, _maxPoints);
            _pointsText.text = _points.ToString();

            if (_points == 0)
            {
                SetActivePlusButtons(false);
            }
        }
    }

    private void Awake()
    {
        foreach (var value in _plusMinusValues)
        {
            value.Value.ValueIncreased += OnValueIncreased;
            value.Value.ValueDecreased += OnValueDecreased;
        }
    }

    private void OnValueIncreased(PlusMinusValue value)
    {
        Points--;
    }

    private void OnValueDecreased(PlusMinusValue value)
    {
        Points++;
    }

    private void SetActivePlusButtons(bool interactable)
    {
        foreach (var plusMinusValue in _plusMinusValues)
        {
            plusMinusValue.Value.PlusButton.interactable = interactable;
        }
    }
}
