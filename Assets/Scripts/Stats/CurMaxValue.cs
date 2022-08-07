using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Addition
{
    OnlyCurrent,
    OnlyMax,
    MaxAndCurrent
}

[Serializable]
public class CurMaxValue
{
    [SerializeField] private float _current;
    public float Current
    {
        get => _current;
        set => _current = Mathf.Clamp(value, 0, _max);
    }

    [SerializeField] private float _max;
    public float Max
    {
        get => _max;
        set
        {
            _max = Mathf.Max(0, value);
            if (_current > _max)
            {
                Current = _max;
            }
        }
    }

    public void Add(float value, Addition addition)
    {
        switch (addition)
        {
            case Addition.OnlyCurrent:
                Current += value;
                break;
            case Addition.OnlyMax:
                Max += value;
                break;
            case Addition.MaxAndCurrent:
                Max += value;
                Current += value;
                break;
        }
    }
}
