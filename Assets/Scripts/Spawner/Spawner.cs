using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public static Spawner Instance;
    GameManager           _gameManager;
    ObjectPooler          _objectPooler;
    ActiveTargets         _activeTargets;

    [SerializeField] float monsterSpawnCooldownMin     = 0.5f
                         , monsterSpawnCooldownMax     = 2f
                         , monsterCooldownDecreaseRate = 0.1f;

    float monsterSpawnTimer, monsterSpawnCooldown = 2f;

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
        _gameManager   = GameManager.Instance;
        _objectPooler  = GetComponent<ObjectPooler>();
        _activeTargets = GetComponent<ActiveTargets>();
    }

    public void InitializeSpawner() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        _activeTargets.ResetQueue();
        monsterSpawnTimer    = 0f;
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
        monsterSpawnTimer += Time.deltaTime;
        if (monsterSpawnTimer < monsterSpawnCooldown)
            return;
        monsterSpawnTimer = 0f;
        UpdateMonsterSpawnCooldown();
        SpawnMonster();
    }

    void SpawnMonster() {
        Monster monster = _objectPooler.SpawnFromPool(Strings.MONSTER);
        _activeTargets.AddTarget(monster);
    }

    void UpdateMonsterSpawnCooldown() {
        if (isMinMonsterCooldown)
            return;
        monsterSpawnCooldown -= monsterCooldownDecreaseRate;
        if (monsterSpawnCooldown <= monsterSpawnCooldownMin)
            isMinMonsterCooldown = true;
    }

    public void RecycleObject(GameObject obj) {
        _objectPooler.DisableObject(obj);
        _gameManager.DestroyMonster();
    }
}