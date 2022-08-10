using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _cooldown;
    [SerializeField] private TMP_Text _cooldownTimer;

    public void Init(Skill skill)
    {
        _image.sprite = skill.Icon;
        _cooldown.SetActive(false);
        skill.CooldownChanged += OnCooldownChanged;
    }

    private void OnCooldownChanged(float cooldown)
    {
        if (cooldown > 0)
        {
            _cooldown.SetActive(true);
            _cooldownTimer.text = Mathf.RoundToInt(cooldown).ToString();
        }
        else
        {
            _cooldown.SetActive(false);
        }
    }
}
