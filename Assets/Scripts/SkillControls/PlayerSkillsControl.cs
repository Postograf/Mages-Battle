using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillsControl : SkillsControl
{
    private PlayerInputs _inputs;
    private InputAction[] _skillInputs;
    private Animator _animator;
    private MovementControl _movement;

    protected override void Awake()
    {
        base.Awake();

        _inputs = new PlayerInputs();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<MovementControl>();

        _skillInputs = new InputAction[3]
        {
            _inputs.Game.FirstSkill,
            _inputs.Game.SecondSkill,
            _inputs.Game.ThirdSkill
        };

        for (int i = 0; i < _skillInputs.Length && i < Skills.Length; i++)
        {
            var skill = Skills[i];
            _skillInputs[i].performed += (a) =>
            {
                if (skill.Press())
                {
                    ActivatedSkills.Add(skill);
                    _animator?.SetTrigger($"skill {i} start");

                    if (skill.Cancellable)
                    {
                        _movement?.Stop();
                    }
                }
            };

            _skillInputs[i].canceled += (a) =>
            {
                if (ActivatedSkills.Contains(skill) && skill.Unpress())
                {
                    ActivatedSkills.Remove(skill);
                    _animator?.SetTrigger($"skill {i} ended");
                }
            };
        }
    }

    private void OnEnable()
    {
        foreach (var input in _skillInputs)
        {
            input.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var input in _skillInputs)
        {
            input.Disable();
        }
    }
}
