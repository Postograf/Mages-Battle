using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFairySpawner : MonoBehaviour
{
    [SerializeField] private IceFairy _iceFairyPrefab;
    [SerializeField] private Transform _home;

    public IceFairy IceFairy { get; private set; }

    private void Awake()
    {
        IceFairy = Instantiate(_iceFairyPrefab, _home.position, Quaternion.identity);
        IceFairy.Home = _home;
    }
}
