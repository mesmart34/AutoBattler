using System;
using System.Collections;
using System.Collections.Generic;
using AutoBattler;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class CharacterInfoTrigger : MonoBehaviour
{
    private Unit _unit;
    
    private void Awake()
    {
        _unit = GetComponent<Unit>(); 
    }

    private void OnMouseEnter()
    {
        CharacterInfoDisplayer.Instance.Show(_unit);        
    }

    private void OnMouseExit()
    {
        CharacterInfoDisplayer.Instance.Hide();
    }
}
