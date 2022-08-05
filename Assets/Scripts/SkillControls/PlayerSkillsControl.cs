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

    private void Awake()
    {
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
            _skillInputs[i].performed += (a) =>
            {
                var skill = Skills[i];
                skill.WaitDelay();
                ActivatedSkills.Add(skill);
                _animator?.SetTrigger($"skill {i} start");

                if (skill.Cancellable)
                {
                    _movement?.Stop();
                }
            };

            _skillInputs[i].canceled += (a) =>
            {
                var skill = Skills[i];
                if (ActivatedSkills.Contains(skill))
                {
                    skill.End();
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
