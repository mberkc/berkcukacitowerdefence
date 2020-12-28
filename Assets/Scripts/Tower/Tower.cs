using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITowerAction {
    public event ITowerAction.TowerDamageAction OnDamageChange;
    ActiveTargets                               _activeTargets;
    Queue<Monster>                              monsterQueue = new Queue<Monster>();

    [SerializeField] GameObject bulletPrefab;
    Transform                   bullets;

    public int Damage {
        get {
            return damage;
        }
        set {
            damage = value;
        }
    }

    float                 fireCooldown = 1f, fireTimer;
    [SerializeField] int  damage;
    [SerializeField] bool isActive;
    bool                  canFire;

    void Start() {
        _activeTargets               =  Spawner.Instance.GetComponent<ActiveTargets>();
        _activeTargets.OnQueueUpdate += UpdateTargetList;
        bullets                      =  transform.GetChild(1);
    }

    public void GenerateTower(int damage) {
        if (!isActive)
            isActive = true;
        this.damage = damage;
        fireTimer   = 0f;
        OnDamageChange?.Invoke(damage);
    }

    public void DisableTower() {
        isActive = false;
        OnDamageChange?.Invoke(0);
    }

    void FixedUpdate() {
        if (!GameManager.Instance.isPlaying || !isActive)
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
        if (monsterQueue.Count <= 0)
            return;
        Fire();
    }

    void UpdateTargetList(Queue<Monster> monsterQueue) {
        this.monsterQueue = monsterQueue;
    }

    void Fire() {
        canFire = false;
        Bullet bullet = Instantiate(bulletPrefab, bullets, false).GetComponent<Bullet>();
        bullet.OnBulletFired(monsterQueue.Peek(), damage);
        //_objectPooler.SpawnFromPool(Strings.MONSTER);
    }

    public void RecycleBullet(GameObject obj) {
        //_objectPooler.DisableObject(obj);
    }
}