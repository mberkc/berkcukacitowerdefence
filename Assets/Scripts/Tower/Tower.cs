using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITowerAction {
    public event ITowerAction.TowerDamageAction OnDamageChange;

    public int Damage {
        get;
        set;
    }

    float fireCooldown = 1f, fireTimer;
    int   damage;
    bool  isActive, canFire;

    void CreateTower(int damage) {
        if (!isActive)
            isActive = true;
        fireTimer = 0f;
        OnDamageChange?.Invoke(damage);
    }

    void DisableTower() {
        isActive = false;
        OnDamageChange?.Invoke(0);
    }

    void FixedUpdate() {
        if (!isActive)
            return;
        if (canFire)
            LookForTarget();
        else {
            fireTimer += Time.deltaTime;
            if (fireTimer < fireCooldown)
                return;
            fireTimer = 0f;
            canFire   = true;
        }
    }

    void LookForTarget() {
        if (false)
            return;
        // Check if there is a target
        canFire = false;
        Fire();
    }

    void Fire() {
        //_objectPooler.SpawnFromPool(Strings.MONSTER);
    }

    public void RecycleBullet(GameObject obj) {
        //_objectPooler.DisableObject(obj);
    }
}