using Fusion;

using System;

using UnityEngine;

public enum IceFairyStateID
{
    Fairy,
    Wall,
    Shield
}

[Serializable]
public class StateByIdDictionary : SerializableDictionary<IceFairyStateID, IceFairyState>
{ }

public class IceFairy : NetworkBehaviour
{
    [SerializeField] private StateByIdDictionary _statesById;

    public IceFairyStateID State { get; private set; }
    public Transform Home { get; set; }
    public GameObject Owner { get; set; }

    public void BecomeWall(GameObject sender, Vector3 from, Vector3 to)
    {
        _statesById[State].BecomeWall(sender, from, to);
    }

    public void BecomeShield(GameObject sender, Vector3 to)
    {
        _statesById[State].BecomeShield(sender, to);
    }

    [Rpc]
    public void RPC_ChangeState(IceFairyStateID state)
    {
        State = state;
        foreach (var stateById in _statesById)
        {
            stateById.Value.enabled = stateById.Key == state;
        }
    }

    [Rpc]
    public void RPC_Initialize()
    {
        foreach (var stateById in _statesById)
        {
            stateById.Value.Init();
        }

        RPC_ChangeState(IceFairyStateID.Fairy);
    }
}
