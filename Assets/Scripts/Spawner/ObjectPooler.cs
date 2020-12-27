using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [Serializable] public class Pool {
        public string     tag;
        public GameObject objectPrefab;
        public int        size;
    }

    public List<Pool>                            pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.objectPrefab, transform, false);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag) {
        if (!poolDictionary.ContainsKey(tag))
            return null;
        GameObject obj = poolDictionary[tag].Dequeue();
        obj.transform.position = transform.position;
        obj.SetActive(true);

        IPooledObject pooledObj = obj.GetComponent<IPooledObject>();
        if(pooledObj!=null)
            pooledObj.OnObjectSpawn();
        poolDictionary[tag].Enqueue(obj);
        return obj;
    }
}