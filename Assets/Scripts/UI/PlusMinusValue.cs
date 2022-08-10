using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PlusMinusValue : MonoBehaviour
{
    [SerializeField] private int _startValue;
    [SerializeField] private int _step;
    [SerializeField] private int _min;
    [SerializeField] private int _max;
    [SerializeField] private Button _plusButton;
    [SerializeField] private Button _minusButton;
    [SerializeField] private TMP_Text _valueText;

    private int _currentValue;

    public Button PlusButton => _plusButton;
    public Button MinusButton => _minusButton;
    public int CurrentValue 
    { 
        get => _currentValue;
        private set
        {
            _currentValue = Mathf.Clamp(value, _min, _max);
            _valueText.text = _currentValue.ToString();

            _plusButton.interactable = CurrentValue < _max;
            _minusButton.interactable = CurrentValue > _min;
        }
    }

    public event Action<PlusMinusValue> ValueIncreased;
    public event Action<PlusMinusValue> ValueDecreased;

    private void Awake()
    {
        _plusButton.onClick.AddListener(OnPlusButtonClicked);
        _minusButton.onClick.AddListener(OnMinusButtonClicked);

        CurrentValue = _startValue;
    }

    private void OnPlusButtonClicked()
    {
        CurrentValue += _step;
        ValueIncreased?.Invoke(this);
    }

    private void OnMinusButtonClicked()
    {
        CurrentValue -= _step;
        ValueDecreased?.Invoke(this);
    }
}
