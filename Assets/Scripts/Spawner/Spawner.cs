using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public static Spawner Instance;
    GameManager           _gameManager;
    ObjectPooler          _objectPooler;

    [SerializeField] float monsterSpawnCooldown
                         , monsterSpawnCooldownMin     = 0.2f
                         , monsterSpawnCooldownMax     = 5f
                         , monsterCooldownDecreaseRate = 0.1f;

    bool isMinMonsterCooldown;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        _gameManager  = GameManager.Instance;
        _objectPooler = GetComponent<ObjectPooler>();
    }

    void InitializeSpawner() {
        monsterSpawnCooldown = monsterSpawnCooldownMax;
        isMinMonsterCooldown = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnMonster();
    }

    void FixedUpdate() {
        if (!_gameManager.isPlaying)
            return;
        // Cooldown Update
        //SpawnMonster();
    }

    void SpawnMonster() {
        UpdateCooldown();
        _objectPooler.SpawnFromPool(Strings.MONSTER);
    }

    void UpdateCooldown() {
        if (isMinMonsterCooldown)
            return;
        monsterSpawnCooldown -= monsterCooldownDecreaseRate;
        if (monsterSpawnCooldown <= monsterSpawnCooldownMin)
            isMinMonsterCooldown = true;
    }
}