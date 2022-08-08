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

public class IceFairy : MonoBehaviour
{
    [SerializeField] private StateByIdDictionary _statesById;

    public IceFairyStateID CurrentState { get; private set; }
    public Transform Home { get; set; }

    private void Start()
    {
        ChangeState(IceFairyStateID.Fairy);
    }

    public void BecomeWall(GameObject sender, Vector3 from, Vector3 to)
    {
        _statesById[CurrentState].BecomeWall(sender, from, to);
    }

    public void BecomeShield(GameObject sender, Vector3 to)
    {
        _statesById[CurrentState].BecomeShield(sender, to);
    }

    public void ChangeState(IceFairyStateID state)
    {
        CurrentState = state;
        foreach (var stateById in _statesById)
        {
            stateById.Value.enabled = stateById.Key == state;
        }
    }
}
