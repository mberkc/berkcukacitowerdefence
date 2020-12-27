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
    int                        maxHealth = 100, tweenId;
    bool                       isAlive;

    public void OnObjectSpawn() {
        health = maxHealth;
        OnHealthChange?.Invoke(health);
        isAlive = true;
        FollowPath();
    }

    void FollowPath() {
        StopMovement();
        tweenId = transform.DOPath(path, 5).intId;
    }

    void StopMovement() {
        DOTween.Kill(tweenId);
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
        // Recycle
    }

    void Finish() {
        if (!isAlive)
            return;
        isAlive = false;
        StopMovement();
        OnFinish?.Invoke();
    }

    void OnTriggerEnter(Collider other) {
        print(other.tag);
        if (other.CompareTag(Tags.BULLET))
            TakeDamage(other.GetComponent<Bullet>().Damage);
        if (other.CompareTag(Tags.FINISH))
            Finish();
    }
}