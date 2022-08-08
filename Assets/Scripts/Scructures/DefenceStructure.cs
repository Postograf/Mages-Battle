using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStructure : Stats
{
    private void Update()
    {
        Mana -= Time.deltaTime;
    }

    protected override void NotifyManaChanges()
    {
        base.NotifyManaChanges();
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
