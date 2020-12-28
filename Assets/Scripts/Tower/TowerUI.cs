using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI damageText;
    Tower                            _tower;
    int                              oldDamage;

    void Awake() {
        _tower                =  transform.GetComponent<Tower>();
        _tower.OnDamageChange += UpdateDamageText;
    }

    void UpdateDamageText(int damage) {
        if (oldDamage == damage)
            return;
        damageText.text = damage != 0 ? damage.ToString() : "";
        oldDamage       = damage;
    }
}