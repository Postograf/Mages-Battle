using Fusion;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFairySpawner : NetworkBehaviour
{
    [SerializeField] private IceFairy _iceFairyPrefab;
    [SerializeField] private Transform _home;

    public IceFairy IceFairy { get; private set; }

    public override void Spawned()
    {
        if (!Object.HasStateAuthority) return;

        IceFairy = Runner.Spawn(_iceFairyPrefab, _home.position, Quaternion.identity);
        IceFairy.Home = _home;
        IceFairy.Owner = gameObject;
        IceFairy.RPC_Initialize();
    }
}