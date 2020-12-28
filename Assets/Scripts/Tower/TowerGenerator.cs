using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour {
    Tower[] towers;
    int[]   towerGenerationQueue            = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
    int     towersGenerated, towerPositions = 13, minDamage = 10, maxDamage = 51;
    bool    allTowersGenerated;

    void Start() {
        towers = transform.GetComponentsInChildren<Tower>();
        RandomizeArray(towerGenerationQueue);
        UIManager.Instance.SpawnButton.onClick.AddListener(GenerateTower);
    }

    void GenerateTower() {
        int damage = Random.Range(minDamage, maxDamage);
        if (allTowersGenerated) {
            towers[Random.Range(0, towerPositions)].GenerateTower(damage);
        } else {
            int towerIndex = towersGenerated++;
            towers[towerGenerationQueue[towerIndex]].GenerateTower(damage);
            if (towerIndex == towerPositions - 1)
                allTowersGenerated = true;
        }
    }

    void RandomizeArray(int[] inputArray) {
        for (int i = 0; i < inputArray.Length; i++) {
            int rnd  = Random.Range(0, inputArray.Length);
            var temp = inputArray[rnd];
            inputArray[rnd] = inputArray[i];
            inputArray[i]   = temp;
        }
    }
}