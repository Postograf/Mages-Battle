using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

public class SkillsControl : MonoBehaviour
{
    [SerializeField] private Skill[] _skills;

    public Skill[] Skills => _skills;
    public HashSet<Skill> ActivatedSkills { get; protected set; }

    public virtual void CancelAllSkills()
    {
        foreach (var skill in ActivatedSkills)
        {
            skill.Cancel();
        }

        ActivatedSkills.RemoveWhere(x => x.Cancellable == false);
    }
}