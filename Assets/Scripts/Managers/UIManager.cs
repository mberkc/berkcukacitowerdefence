﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static            UIManager       Instance;
    [SerializeField]         GameObject      initialPanel;
    [SerializeField]         TextMeshProUGUI killAmount;
    [HideInInspector] public GameObject      gameCanvas;

    void Awake() {
        if (Instance == null) {
            gameCanvas = transform.GetChild(0).gameObject;
            Instance   = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void InitPanels() {
        ShowInitialPanel();
    }

    public void ShowInitialPanel() {
        initialPanel.SetActive(true);
    }

    public void OnRestartButtonClicked() {
        GameManager.Instance.RestartLevel();
    }

    public void OnSpawnButtonClicked() {
        // Subscribe - Event Call
    }

    public void SetWaveText(int killAmount) {
        this.killAmount.text = "KILLS " + killAmount;
    }
}