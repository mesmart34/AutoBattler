using System;
using System.Collections;
using System.Collections.Generic;
using AutoBattler;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterInfoPrefab;

    private void Start()
    {
        var units = Board.Instance.GetUnits(x => !x.IsEnemy);
        foreach (var unit in units)
        {
            var infoInstance = Instantiate(_characterInfoPrefab, transform);
            var texts = infoInstance.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = unit.Name;
            texts[1].text = unit._attackController.Strength.ToString();
        }
    }
}
