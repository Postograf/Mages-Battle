using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillViewList : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private SkillView _skillViewPrefab;

    private List<SkillView> _spawnedSkillViews = new List<SkillView>();

    private void Awake()
    {
        _game.PlayerSpawned += InitSkillViews;
    }

    private void InitSkillViews()
    {
        foreach (var skill in _game.PlayerSkills.Skills)
        {
            var skillView = Instantiate(_skillViewPrefab, transform);
            skillView.Init(skill);
            _spawnedSkillViews.Add(skillView);
        }
    }
}
