using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoBattler;
using Contracts;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterInfoDisplayer : MonoBehaviour
{
    public static CharacterInfoDisplayer Instance;
    [SerializeField]
    private GameObject _skillInfoPrefab;  
    [SerializeField]
    private GameObject _displayer;
    [SerializeField]
    private TextMeshProUGUI _characterName;

    private List<GameObject> _spawnedSkills = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Show(Unit unit)
    {
        _characterName.text = unit.Name;
        foreach (var skill in unit._skillController._skills)
        {
            var skillInstance = Instantiate(_skillInfoPrefab, _displayer.transform);
            _spawnedSkills.Add(skillInstance);
            var skillInfo = skillInstance.GetComponent<SkillInfoDisplayer>();
            skillInfo.Init(skill.Name, skill.Description);
        }
        _displayer.SetActive(true);
    }

    public void Hide()
    {
        foreach (var skill in _spawnedSkills)
        {
            Destroy(skill);
        }
        _displayer.SetActive(false);
    }
}
