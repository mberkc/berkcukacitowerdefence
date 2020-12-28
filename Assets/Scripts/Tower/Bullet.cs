using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] Monster target;
    public           int     Damage { get; set; }
    static           float   speed;
    static           bool    isAlive;

    public void OnBulletFired(Monster targetMonster) {
        target           =  targetMonster;
        target.OnDestroy += DestroyBullet;
        target.OnFinish  += DestroyBullet;
        isAlive          =  true;
    }

    // Fixed vs Late Dif
    void LateUpdate() {
        Vector3.MoveTowards(transform.position, target.transform.position, speed);
        // transform.Translate((target.transform.position - transform.position) * (speed * Time.deltaTime), Space.World);
    }

    void DestroyBullet() {
        if (!isAlive)
            return;
        target.OnDestroy -= DestroyBullet;
        target.OnFinish  -= DestroyBullet;
        // Recycle
    }
}