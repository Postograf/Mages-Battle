using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryStructure : Unit
{
    private void Update()
    {
        Mana -= Time.deltaTime;
    }

    protected override void NotifyManaChanges(float current, float max)
    {
        base.NotifyManaChanges(current, max);
        if (Mana <= 0)
        {
            Health = 0;
        }
    }

    public void FullRecover()
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }
}
