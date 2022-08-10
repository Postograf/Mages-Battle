using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void RPC_ApplyDamage(float damage, Vector3 from);
}