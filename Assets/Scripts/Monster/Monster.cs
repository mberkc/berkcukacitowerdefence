using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Monster : MonoBehaviour, IPooledObject, IMonsterAction {
    public event IMonsterAction.MonsterHealthAction OnHealthChange;
    public event IMonsterAction.MonsterAction       OnDestroy;
    public event IMonsterAction.MonsterAction       OnFinish;

    [SerializeField] Vector3[] path = new Vector3[4];
    [SerializeField] int       health;

    public string NameTag {
        get => nameTag;
    }

    string nameTag   = Strings.MONSTER;
    int    maxHealth = 100;
    bool   isAlive;

    public void OnObjectSpawn() {
        CancelInvoke(nameof(Disable));
        health = maxHealth;
        OnHealthChange?.Invoke(health);
        isAlive = true;
        FollowPath();
    }

    void FollowPath() {
        StopMovement();
        transform.DOPath(path, 5);
    }

    void StopMovement() {
        DOTween.Kill(transform);
    }

    void TakeDamage(int damageTaken) {
        if (!isAlive)
            return;
        health -= damageTaken;
        if (health <= 0) {
            health = 0;
            Destroy();
        }
        OnHealthChange?.Invoke(health);
    }

    void Destroy() {
        if (!isAlive)
            return;
        isAlive = false;
        StopMovement();
        OnDestroy?.Invoke();
        Invoke(nameof(Disable), 0.1f);
    }

    void Finish() {
        if (!isAlive)
            return;
        isAlive = false;
        StopMovement();
        OnFinish?.Invoke();
        //Invoke(nameof(Disable), 0.1f);
    }

    void Disable() {
        Spawner.Instance.RecycleObject(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tags.BULLET)) {
            TakeDamage(other.GetComponent<Bullet>().Damage);
            Destroy(other.gameObject);
        }
        if (other.CompareTag(Tags.FINISH)) {
            Finish();
            GameManager.Instance.Finish(false);
        }
    }
}