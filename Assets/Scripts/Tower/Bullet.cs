using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] Monster target;
    public           int     Damage { get; set; }
    [SerializeField] float   speed    = 12;
    float                    lifeTime = 5f;
    bool                     isAlive;

    public void OnBulletFired(Monster targetMonster, int damage) {
        target           =  targetMonster;
        Damage           =  damage;
        target.OnDestroy += DestroyBullet;
        target.OnFinish  += DestroyBullet;
        isAlive          =  true;
        Invoke(nameof(DisableBullet), lifeTime);
    }

    // Fixed vs Late Dif
    void LateUpdate() {
        transform.Translate((target.transform.position - transform.position) * (speed * Time.deltaTime)
                          , Space.World);
    }

    void DisableBullet() {
        Destroy(GetComponent<SpriteRenderer>());
    }

    void DestroyBullet() {
        if (!isAlive)
            return;
        isAlive          =  false;
        target.OnDestroy -= DestroyBullet;
        target.OnFinish  -= DestroyBullet;
        // Recycle
    }
}