using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI healthText;
    Monster                          _monster;
    int                              oldHealth;

    void Awake() {
        _monster                =  transform.GetComponent<Monster>();
        _monster.OnHealthChange += UpdateHealthText;
    }

    void UpdateHealthText(int health) {
        if (oldHealth == health)
            return;
        healthText.text = health.ToString();
        oldHealth       = health;
    }
}